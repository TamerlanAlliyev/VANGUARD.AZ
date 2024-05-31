using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vanguard.Data;
using Vanguard.Models;
using Vanguard.Areas.Admin.ViewModels.Admin;
using Vanguard.Services.Interfaces;
using YourNamespace.Filters; 

namespace YourNamespace.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]
	[ServiceFilter(typeof(AdminAuthorizationFilter))]

	public class AdminController : Controller
    {
        readonly VanguardContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        readonly IEmailService _emailService;

        public AdminController(VanguardContext context, IEmailService emailService, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _emailService = emailService;
            _roleManager = roleManager;
        }

        public IActionResult EmployeeList()
        {
            return View();
        }

        public IActionResult EmployeeCreat()
        {
            EmployeeCreatVM vm = new EmployeeCreatVM
            {
                Roles = _roleManager.Roles.ToList(),
            };
            return View(vm); 
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeCreat(EmployeeCreatVM model)
        {
            if (ModelState.IsValid)
            {
                var accessCode = Guid.NewGuid().ToString("N").Substring(0, 20);
                _context.AllowedEmployees.Add(new AllowedEmployee { Email = model.Email, AccessCode = accessCode,RoleId=model.RoleId });
                await _context.SaveChangesAsync();

                _emailService.Send(model.Email, "Employee Registration", $"Your access code: {accessCode}");

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("EmployeeList");
        }
    }
}
