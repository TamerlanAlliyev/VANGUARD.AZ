using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.ViewModels.Account;
using Vanguard.Data;
using Vanguard.Extensions;
using Vanguard.Helpers;
using Vanguard.Models;
using Vanguard.Services.Interfaces;
using Vanguard.ViewModels.Account;

namespace Vanguard.Areas.Admin.Controllers;

[Area("Admin")]

public class AccountController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    readonly IEmailService _emailService;
    readonly VanguardContext _context;
    readonly IWebHostEnvironment _environment;
    readonly SignInManager<AppUser> _signInManager;
    public AccountController(UserManager<AppUser> userManager,
                             RoleManager<IdentityRole> roleManager,
                             IEmailService emailService,
                             VanguardContext context,
                             IWebHostEnvironment environment,
                             SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _emailService = emailService;
        _context = context;
        _environment = environment;
        _signInManager = signInManager;
    }



    public async Task<IActionResult> Login()
    {
        if (TempData["accCode"] == null)
        {
            return View("Error404");
        }
        var checkEmail = await _context.AllowedEmployees.FirstOrDefaultAsync(a => a.AccessCode == TempData["accCode"]!.ToString());

        TempData["accCode"] = TempData["accCode"];
        ViewData["Email"] = checkEmail!.Email;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string ReturnUrl, LoginVM vm)
    {

        var user = await _userManager.FindByEmailAsync(vm.Email);
        if (user == null)
        {
            ModelState.AddModelError("", "Email not found");
            return View(vm);
        }



        var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);
        if (!result.IsLockedOut && user.LockoutEnd.HasValue)
        {
            ModelState.AddModelError("", "Wait until " + user.LockoutEnd.Value.AddHours(4).ToString("HH:mm:ss"));
        }
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Email or Password is wrong");
            return View();

        }
        if (ReturnUrl != null)
        {
            return Redirect(ReturnUrl);
        }

        var allowedEmp = await _context.AllowedEmployees.FirstOrDefaultAsync(a => a.Email == vm.Email);
        user.AllowedEmployee = allowedEmp;
        user.AllowedEmployeeId = allowedEmp!.Id;

        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Home");
    }


    public IActionResult AccessCode()
    {
        _signInManager.SignOutAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AccessCode(AccessCodeVM vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var checkUser = await _context.AllowedEmployees.FirstOrDefaultAsync(a => a.AccessCode == vm.Code);

        TempData["accCode"] = vm.Code;

        if (checkUser == null)
        {
            ModelState.AddModelError("", "Invalid access code.");
            return View(vm);
        }
        if (checkUser.AppUserId != null)
        {
            return RedirectToAction(nameof(Login));
        }
        else
        {
            return RedirectToAction(nameof(Register));
        }
    }

    public async Task<IActionResult> Register()
    {
        if (TempData["accCode"] == null)
        {
            return View("Error404");
        }
        var checkEmail = await _context.AllowedEmployees.FirstOrDefaultAsync(a => a.AccessCode == TempData["accCode"]!.ToString());

        TempData["accCode"] = TempData["accCode"];
        ViewData["Email"] = checkEmail.Email;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM vm)
    {


        ModelState.Clear();

        ValidationHelper.ValidateRegister(vm, ModelState);

        var allowedEmp = await _context.AllowedEmployees.FirstOrDefaultAsync(a => a.Email == vm.Email);

        if (allowedEmp!.Email != vm.Email)
        {
            ModelState.AddModelError("Email", "You cannot register with an authorized email...!");
            return View(vm);
        }

        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        if (TempData["accCode"] == null)
        {
            return View("Error404");
        }

        var acc = TempData["accCode"]!.ToString()!;



        var selectedRole = await _roleManager.FindByIdAsync(allowedEmp!.RoleId);
        TempData["Email"] = allowedEmp.Email;

        string userName;
        AppUser exsistUserName;
        do
        {
            Guid guid = Guid.NewGuid();
            string guidString = guid.ToString();
            string lastPart = guidString.Substring(guidString.Length - 12);
            userName = $"{vm.Name}-{vm.Surname}_" + lastPart;
            exsistUserName = await _userManager.FindByNameAsync(userName);

        } while (exsistUserName != null);



        var userEmail = await _userManager.FindByEmailAsync(vm.Email);
        if (userEmail != null)
        {
            ModelState.AddModelError("", "Email already exists");
            return View(vm);
        }

        AppUser user = new AppUser
        {
            Name = vm.Name,
            Surname = vm.Surname,
            FullName = $"{vm.Name} {vm.Surname}",
            Email = vm.Email,
            UserName = userName,
            PhoneNumber = vm.PhoneNumber,
            AllowedEmployee = allowedEmp
        };

        allowedEmp.AppUser = user;
        _context.AllowedEmployees.Update(allowedEmp);


        var result = await _userManager.CreateAsync(user, vm.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", $"{error.Code} - {error.Description}");
            }
            return View(vm);
        }

        var roleResult = await _userManager.AddToRoleAsync(user, selectedRole!.Name!);

        if (!roleResult.Succeeded)
        {
            foreach (var error in roleResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(vm);
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var link = Url.Action("EmailConfirmation", "Account", new { token = token, user = user }, Request.Scheme);
        _emailService.Send(user.Email, "Email Confirmation", $"<a href='{link}'>Confirm</a>", true);



        if (vm.ImageFile != null)
        {
            if (!vm.ImageFile.FileSize(5) || !vm.ImageFile.FileTypeAsync("image/"))
            {
                throw new ArgumentException("Invalid file type or size.");
            }
            if (!vm.ImageFile.FileTypeAsync("image"))
            {
                throw new ArgumentException("MainImage", "Files must be 'Image' type!");
            }
            var path = Path.Combine(_environment.WebRootPath, "cilent", "assets", "images", "account");
            var fileName = await vm.ImageFile.SaveToAsync(path);

            Image image = new Image
            {
                Url = fileName,
                AppUser = user
            };

            await _context.Images.AddAsync(image);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction("Login");
    }

    public async Task<IActionResult> EmailConfirmation(string token, string user)
    {
        if (String.IsNullOrWhiteSpace(token) || String.IsNullOrWhiteSpace(user))
            return BadRequest(ModelState);

        var us = await _userManager.FindByNameAsync(user);
        await _userManager.ConfirmEmailAsync(us, token);
        return RedirectToAction("Login");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("AccessCode", "Account");
    }


    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordVM passwordVM)
    {
        if (!ModelState.IsValid)
        {
            return View(passwordVM);
        }
        var userEmail = await _userManager.FindByEmailAsync(passwordVM.Email);
        if (userEmail == null)
        {
            ModelState.AddModelError("", "Email not found");
            return View(userEmail);
        }


        var user = await _userManager.FindByIdAsync(userEmail.Id);

        if (user == null)
        {
            ModelState.AddModelError("", "User not found");
            return View(passwordVM);
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var link = Url.Action("ResetPassword", "Account", new { token = token, user = user }, Request.Scheme);
        _emailService.Send(user.Email, "Reset Password", $"<a href='{link}'>Password Reset</a>", true);

        return RedirectToAction(nameof(Login));
    }


    public IActionResult ResetPassword(string token, string user)
    {
        if (String.IsNullOrWhiteSpace(token) || String.IsNullOrWhiteSpace(user))
            return BadRequest(ModelState);


        ResetPasswordVM reset = new ResetPasswordVM
        {
            user = user
        };
        return View(reset);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPassword)
    {
        var user = await _userManager.FindByNameAsync(resetPassword.user);

        await _userManager.ResetPasswordAsync(user, resetPassword.token, resetPassword.Password);

        return RedirectToAction("Login");
    }


    //public async Task CreateRoles()
    //{
    //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
    //    await _roleManager.CreateAsync(new IdentityRole { Name = "Editor" });
    //    await _roleManager.CreateAsync(new IdentityRole { Name = "Moderator" });
    //    await _roleManager.CreateAsync(new IdentityRole { Name = "Author" });
    //    await _roleManager.CreateAsync(new IdentityRole { Name = "Customer" });
    //}
}
