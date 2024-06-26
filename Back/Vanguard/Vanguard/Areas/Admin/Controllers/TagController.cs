﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.Services;
using Vanguard.Areas.Admin.ViewModels.TagViewModels;
using Vanguard.Data;
using Vanguard.Exceptions;
using YourNamespace.Filters;

namespace Vanguard.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin, Editor")]
[ServiceFilter(typeof(AdminAuthorizationFilter))]
public class TagController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly VanguardContext _context;
    readonly ITagService _tagService;

    public TagController(VanguardContext context, ITagService tagService)
    {
        _context = context;
        _tagService = tagService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _tagService.GetAllAsync());
    }

    public async Task<IActionResult> Detail(int? id)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);
        if (tag == null) return View("Error");
        return View(tag);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TagCreateVM vm)
    {
        if (!ModelState.IsValid) return View(vm);

        try
        {
            var Create = await _tagService.CreateAsync(vm);
            return RedirectToAction("Index");
        }
        catch (UnprocessableEntityException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(vm);
        }
    }


    public async Task<IActionResult> Edit(int? id)
    {
        var TagView = await _tagService.EditViewAsync(id);
        if (TagView == null) return View("Error");
        return View(TagView);
    }



    [HttpPost]
    public async Task<IActionResult> Edit(int? id, TagCreateVM vm)
    {
        if (!ModelState.IsValid) return View(vm);

        try
        {
            var Edit = await _tagService.EditAsync(id, vm);
            if (Edit == null) return View("Error");
            return RedirectToAction(nameof(Index));

        }
        catch (UnprocessableEntityException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(vm);
        }
    }

    public async Task<IActionResult> SoftDelete(int? id)
    {
        await _tagService.SoftDeleteAsync(id);

        return Ok();
    }

    public async Task<IActionResult> DeletedList()
    {
        return View(await _tagService.GetAllDeletedAsync());
    }



    public async Task<IActionResult> HardDeleted(int? id)
    {
        await _tagService.HardDeletedAsync(id);
        return Ok();
    }


    public async Task<IActionResult> Recover(int? id)
    {
        await _tagService.RecoverAsync(id);

        return Ok();
    }

}
