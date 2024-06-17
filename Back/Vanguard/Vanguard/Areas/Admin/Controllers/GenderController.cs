using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.ViewModels.Gender;
using Vanguard.Data;
using Vanguard.Models;
using YourNamespace.Filters;

namespace Vanguard.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin, Editor")]

[ServiceFilter(typeof(AdminAuthorizationFilter))]

public class GenderController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly VanguardContext _context;

    public GenderController(VanguardContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Gender> genders = await _context.Genders.Where(g => !g.IsDeleted).ToListAsync();
        return View(genders);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(GenderVM vm)
    {
        Gender gender = new Gender
        {
            Name = vm.Name,
            IsDeleted = false
        };

        await _context.Genders.AddAsync(gender);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Delete(int id)
    {
        var gender = await _context.Genders.FirstOrDefaultAsync(g => g.Id == id);
        if (gender == null) return View("Error404");
        _context.Genders.Remove(gender);
        await _context.SaveChangesAsync();
        return Ok();
    }

}
