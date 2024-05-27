using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
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

    public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IEmailService emailService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _emailService = emailService;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        //var username = _userManager.FindByNameAsync(vm.Username);
        //if (username!=null)
        //{
        //    ModelState.AddModelError("", "Username already exists");
        //    return View(vm);
        //}

        //var userEmail = _userManager.FindByEmailAsync(vm.Email);
        //if (userEmail!=null)
        //{
        //    ModelState.AddModelError("", "Email already exists");
        //    return View(vm);
        //}

        AppUser user = new AppUser
        {
            Name = vm.Name,
            Surname = vm.Surname,
            UserName = vm.Username,
            FullName = $"{vm.Name} {vm.Surname}",
            Email = vm.Email,
        };

        var result = await _userManager.CreateAsync(user,vm.Password);


        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", $"{error.Code} - {error.Description}");
            }
            return View(vm);
        }

        var roleResult = await _userManager.AddToRoleAsync(user, "Admin");

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

        return View();
    }

    //public async Task CreateRoles()
    //{
    //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
    //    await _roleManager.CreateAsync(new IdentityRole { Name = "Customer" });
    //}
}
