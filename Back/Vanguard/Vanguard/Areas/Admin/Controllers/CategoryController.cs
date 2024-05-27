using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Vanguard.Areas.Admin.Services;
using Vanguard.Areas.Admin.ViewModels.Category;
using Vanguard.Areas.Admin.ViewModels.ErrorsViewModel;
using Vanguard.Data;
using Vanguard.Exceptions;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly VanguardContext _context;
    readonly ICategoryService _categoryService;

    public CategoryController(VanguardContext context, ICategoryService categoryService)
    {
        _context = context;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _categoryService.GetAllAsync());
    }

    public async Task<IActionResult> Detail(int? id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category == null) return View("Error");

        return View(category);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _categoryService.SelectCategoriesAsync();
        return View();
    }



    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreatVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        try
        {
            var category = await _categoryService.CreateAsync(vm);
            return RedirectToAction("Index");
        }
        catch (UnprocessableEntityException ex)
        {
            ViewBag.Categories = await _categoryService.SelectCategoriesAsync();
            ModelState.AddModelError("", ex.Message);
            return View(vm);
        }

    }




    public async Task<IActionResult> Edit(int? id)
    {
        ViewBag.Categories = await _categoryService.SelectCategoriesAsync(id);

        var category = await _categoryService.EditViewAsync(id);

        if (category == null) return View("Error");

        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int? id, CategoryCreatVM vm)
    {
        if (id == null || id < 1) return View("Error");

        try
        {
            await _categoryService.EditAsync(id, vm);
            return RedirectToAction("Index");
        }
        catch (UnprocessableEntityException ex)
        {
            ViewBag.Categories = await _categoryService.SelectCategoriesAsync(id);
            ModelState.AddModelError("", ex.Message);
            return View(vm);
        }

    }


    public async Task<IActionResult> SoftDelete(int? id)
    {
        var category =await _categoryService.SoftDeleteAsync(id);
        if (category == null) return View("Error");
        return Ok();
    }

    public async Task<IActionResult> HardDeleted(int? id)
    {
        var category = await _categoryService.HardDeleteAsync(id);
        if (category == null) return View("Error");
        return Ok();
    }
    public async Task<IActionResult> RecoverAsync(int? id)
    {
        var category = await _categoryService.RecoverAsync(id);
        if (category == null) return View("Error");
        return Ok();
    }

    public async Task<IActionResult> DeletedList()
    {
        var category = await _context.Categories.Where(c=>c.IsDeleted).ToListAsync();
        return View(category);
    }

}
