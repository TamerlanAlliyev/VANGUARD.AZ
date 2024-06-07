﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using System.IO;
using System.Security.Policy;
using Vanguard.Areas.Admin.Services.Interfaces;
using Vanguard.Data;
using Vanguard.Extensions;
using Vanguard.Helpers;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Controllers;

[Area("Admin")]
public class SettingHomeController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly VanguardContext _context;
    readonly IWebHostEnvironment _environment;
    readonly IHomeSliderService _sliderService;
    public SettingHomeController(VanguardContext context, IWebHostEnvironment environment, IHomeSliderService sliderService)
    {
        _context = context;
        _environment = environment;
        _sliderService = sliderService;
    }

    public async Task<IActionResult> SliderList()
    {
        var slider = await _context.HomeSliders
                        .Include(hs => hs.Tag)
                        .Include(hs => hs.Image)
                        .ToListAsync();

        return View(slider);
    }

    public async Task<IActionResult> SliderCreate()
    {

        var tags = await _context.Tags.Where(t => !t.IsDeleted).ToListAsync();
        ViewBag.Tags = tags;
        return View();
    }



    [HttpPost]
    public async Task<IActionResult> SliderCreate(HomeSlider slider)
    {

        ModelState.Clear();

        ValidationHelper.ValidateHomeSlider(slider, ModelState);

        if (!ModelState.IsValid)
        {
            var tags = await _context.Tags.Where(t => !t.IsDeleted).ToListAsync();
            ViewBag.Tags = tags;
            return View(slider);
        }

        HomeSlider homeSlider = new HomeSlider
        {
            TagId = slider.TagId,
            Title = slider.Title,
            Description = slider.Description,
        };

        Models.Image Image = new Models.Image();
        if (!slider.ImageFile.FileSize(5) || !slider.ImageFile.FileTypeAsync("image/"))
        {
            ModelState.AddModelError("ProfilImage", "Invalid file type or size.");
            return View(slider);
        }
        if (!slider.ImageFile.FileTypeAsync("image"))
        {
            ModelState.AddModelError("ProfilImage", "Files must be 'image' type!.");
            return View(slider);
        }

        var path = Path.Combine(_environment.WebRootPath, "cilent", "assets", "settings", "slider");
        var filename = await slider.ImageFile.SaveToAsync(path);

        Image = new Models.Image
        {
            Url = filename,
        };

        homeSlider.Image = Image;
        Image.HomeSlider = homeSlider;

        await _context.Images.AddAsync(Image);
        await _context.HomeSliders.AddAsync(homeSlider);
        await _context.SaveChangesAsync();
        return RedirectToAction("SliderList");
    }



    public async Task<IActionResult> SliderEdit(int? id)
    {
        var slider = await _context.HomeSliders.Include(s => s.Image).Include(s => s.Tag).FirstOrDefaultAsync(s => s.Id == id);

        var tags = await _context.Tags.Where(t => !t.IsDeleted).ToListAsync();
        ViewBag.Tags = tags;
        return View(slider);
    }


    [HttpPost]
    public async Task<IActionResult> SliderEdit(HomeSlider slider)
    {
        ModelState.Clear();

        ValidationHelper.ValidateHomeSlider(slider, ModelState);

        if (!ModelState.IsValid)
        {
            var tags = await _context.Tags.Where(t => !t.IsDeleted).ToListAsync();
            ViewBag.Tags = tags;
            return View(slider);
        }


        var homeSlider = await _context.HomeSliders.Include(s => s.Image).Include(s => s.Tag).FirstOrDefaultAsync(s => s.Id == slider.Id);
        homeSlider.Title = slider.Title;
        homeSlider.Description = slider.Description;
        homeSlider.TagId = slider.TagId;

        if (slider.ImageFile != null)
            await _sliderService.SliderImageCreateAsync(slider);

        await _context.SaveChangesAsync();
        return RedirectToAction("SliderList");
    }





    public async Task<ServiceResult> ImageDeleteAsync(string url)
    {
        var path = Path.Combine(_environment.WebRootPath, "cilent", "assets", "settings", "slider");
        Image image = await _context.Images.FirstOrDefaultAsync(i => i.Url == url);
        if (image == null) ServiceResult.NotFound("Image not found.");

        string fileName = image.Url;
        if (string.IsNullOrEmpty(fileName)) ServiceResult.BadRequest("File name is invalid.");

        try
        {
            ImageFileExtension.DeleteImagesService(path, fileName);
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return ServiceResult.Ok("Image deleted successfully.");
        }
        catch (Exception ex)
        {
            return ServiceResult.InternalServerError($"Internal server error: {ex.Message}");
        }
    }


    public async Task<ServiceResult> SliderDelete(int id)
    {
        try
        {
            var exsistBlog = await _context.HomeSliders.Include(b => b.Image).FirstOrDefaultAsync(i => i.Id == id);
            if (exsistBlog.Image != null)
                await ImageDeleteAsync(exsistBlog.Image.Url);

            _context.HomeSliders.Remove(exsistBlog);
            await _context.SaveChangesAsync();
            return ServiceResult.Ok("Slider deleted successfully.");
        }
        catch (Exception ex)
        {
            return ServiceResult.InternalServerError($"Internal server error: {ex.Message}");
        }
    }
}