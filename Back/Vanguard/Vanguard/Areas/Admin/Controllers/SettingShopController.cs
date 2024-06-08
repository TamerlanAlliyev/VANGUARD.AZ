using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.Services.Interfaces;
using Vanguard.Data;
using Vanguard.Extensions;
using Vanguard.Helpers;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Controllers;

[Area("Admin")]
public class SettingShopController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly VanguardContext _context;
    readonly IWebHostEnvironment _environment;
    readonly ISettingProductService _settingProductService;
    public SettingShopController(VanguardContext context, IWebHostEnvironment environment, ISettingProductService settingProductService)
    {
        _context = context;
        _environment = environment;
        _settingProductService = settingProductService;
    }

    public async Task<IActionResult> Banner()
    {
        var banner = await _context.ShopBanner.Include(b=>b.Image).FirstOrDefaultAsync();
        return View(banner);
    }

    [HttpPost]
    public async Task<IActionResult> Banner(ShopBanner banner)
    {
        var exsisBanner = await _context.ShopBanner.Include(b => b.Image).FirstOrDefaultAsync();

        exsisBanner.Title = banner.Title;
        exsisBanner.Description = banner.Description
            ;

        if (banner.ImageFile != null)
        {
            Image Image = new Image();
            var path = Path.Combine(_environment.WebRootPath, "cilent", "assets", "settings", "banner");

            if (exsisBanner.Image.Url!=null)
                await ImageDeleteAsync(exsisBanner.Image.Url);

            if (!banner.ImageFile.FileSize(5) || !banner.ImageFile.FileTypeAsync("image/"))
            {
                ModelState.AddModelError("ProfilImage", "Invalid file type or size.");
                return View(banner);
            }
            if (!banner.ImageFile.FileTypeAsync("image"))
            {
                ModelState.AddModelError("ProfilImage", "Files must be 'image' type!.");
                return View(banner);
            }


            var filename = await banner.ImageFile.SaveToAsync(path);

            Image = new Image
            {
                Url = filename,
                ShopBanner = exsisBanner,
            };

            exsisBanner.Image = Image;

            await _context.Images.AddAsync(Image);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction("Banner");
    }



    public async Task<ServiceResult> ImageDeleteAsync(string url)
    {
        var path = Path.Combine(_environment.WebRootPath, "cilent", "assets", "settings", "banner");
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


    public IActionResult Product()
    {
        var model = _settingProductService.GetOrCreateSettingProduct();
        return View(model);
    }

    [HttpPost]
    public IActionResult Product(SettingProduct model)
    {
        if (ModelState.IsValid)
        {
            _settingProductService.UpdateSettingProduct(model);
            return RedirectToAction("Product");
        }

        return View(model);
    }
}
