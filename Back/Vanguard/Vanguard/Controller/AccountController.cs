﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using Vanguard.Data;
using Vanguard.Extensions;
using Vanguard.Helpers;
using Vanguard.Models;
using Vanguard.Services.Interfaces;
using Vanguard.ViewModels.Account;
using Vanguard.ViewModels.Basket;

namespace Vanguard.Controller;

public class AccountController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly UserManager<AppUser> _userManager;
    readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    readonly IEmailService _emailService;
    readonly IWebHostEnvironment _environment;
    readonly VanguardContext _context;
    public AccountController(UserManager<AppUser> userManager,
                                  SignInManager<AppUser> signInManager,
                                  IEmailService emailService,
                                  RoleManager<IdentityRole> roleManager,
                                  IWebHostEnvironment environment,
                                  VanguardContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _roleManager = roleManager;
        _environment = environment;
        _context = context;
    }



    public IActionResult Login()
    {
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




        var basket = HttpContext.Request.Cookies["basket"];
        if (basket != null)
        {

            List<BasketVM> basketItems = basket == null ? new List<BasketVM>() : JsonConvert.DeserializeObject<List<BasketVM>>(basket);

            List<Basket> bskList = new List<Basket>();

            foreach (var item in basketItems)
            {
                Basket bsk = new Basket
                {
                    InformationId = item.Id,
                    Quantity = item.Count,
                    AppUserId = user.Id,
                    AppUser = user,
                };
                bskList.Add(bsk);

            }
            HttpContext.Response.Cookies.Delete("basket");

            await _context.Baskets.AddRangeAsync(bskList);
            await _context.SaveChangesAsync();
        }


        return RedirectToAction("Index", "Home");
    }


    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM vm)
    {
        ModelState.Clear();

        ValidationHelper.ValidateRegister(vm, ModelState);

        if (!ModelState.IsValid)
        {
            return View(vm);
        }

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
        };



        var result = await _userManager.CreateAsync(user, vm.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", $"{error.Code} - {error.Description}");
            }
            return View(vm);
        }

        var roleResult = await _userManager.AddToRoleAsync(user, "Customer");

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


        Image image = new Image();

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
            image = new Image
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
        return RedirectToAction("Login");
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
    //    await _roleManager.CreateAsync(new IdentityRole { Name = "Customer" });
    //}
}
