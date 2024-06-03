using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vanguard.Data;
using Vanguard.Models;
using Vanguard.Areas.Admin.ViewModels.Admin;
using Vanguard.Services.Interfaces;
using YourNamespace.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Vanguard.Helpers;

namespace Vanguard.Areas.Admin.Controllers;
//[Authorize(Roles = "Admin")]
[Area("Admin")]
[ServiceFilter(typeof(AdminAuthorizationFilter))]

public class AdminController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly VanguardContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    readonly UserManager<AppUser> _userManager;

    readonly IEmailService _emailService;

    public AdminController(VanguardContext context, IEmailService emailService, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
    {
        _context = context;
        _emailService = emailService;
        _roleManager = roleManager;
        _userManager = userManager;
    }






    public async Task<IActionResult> EmployeeList()
    {
        try
        {
            var employees = await _context.AllowedEmployees
                                          .Include(u => u.AppUser)
                                          .ThenInclude(u => u.Image)
                                          .Include(u => u.Role)
                                          .ToListAsync();


            List<EmployeeListVM> vm = new List<EmployeeListVM>();

            foreach (var emp in employees)
            {
                EmployeeListVM employee = new EmployeeListVM
                {
                    Id = emp.Id,
                    Image = emp.AppUser?.Image?.Url,
                    FullName = emp.AppUser?.FullName,
                    Position = emp.Role.Name!,
                    Email = emp.Email!,
                    PhoneNumber = emp.AppUser?.PhoneNumber,
                };

                if (emp.AppUser != null) employee.IsRegister = true;

                vm.Add(employee);
            }

            return View(vm);

        }
        catch (KeyNotFoundException ex)
        {
            return View("Error404", new ServiceResult(false, ex.Message, 404));
        }
        catch (ArgumentException ex)
        {
            return View("Error400", new ServiceResult(false, ex.Message, 400));
        }
        catch (Exception ex)
        {
            return View("Error500", new ServiceResult(false, ex.Message, 500));
        }
    }




    public async Task<IActionResult> EmployeeRemove(int id)
    {
        try
        {
            var employee = await _context.AllowedEmployees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return NotFound();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.AllowedEmployeeId == employee.Id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            _context.AllowedEmployees.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return View("Error404", new ServiceResult(false, ex.Message, 404));
        }
        catch (ArgumentException ex)
        {
            return View("Error400", new ServiceResult(false, ex.Message, 400));
        }
        catch (Exception ex)
        {
            return View("Error500", new ServiceResult(false, ex.Message, 500));
        }

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
        try
        {
            if (ModelState.IsValid)
            {
                var accessCode = Guid.NewGuid().ToString("N").Substring(0, 20);
                _context.AllowedEmployees.Add(new AllowedEmployee { Email = model.Email, AccessCode = accessCode, RoleId = model.RoleId });
                await _context.SaveChangesAsync();

                _emailService.Send(model.Email, "Employee Registration", $"Your access code: {accessCode}");

                return RedirectToAction("EmployeeList", "Admin");
            }
            return RedirectToAction("EmployeeList");
        }
        catch (KeyNotFoundException ex)
        {
            return View("Error404", new ServiceResult(false, ex.Message, 404));
        }
        catch (ArgumentException ex)
        {
            return View("Error400", new ServiceResult(false, ex.Message, 400));
        }
        catch (Exception ex)
        {
            return View("Error500", new ServiceResult(false, ex.Message, 500));
        }
    }
}
