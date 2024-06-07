using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.Services.Interfaces;
using Vanguard.Data;
using Vanguard.Extensions;
using Vanguard.Helpers;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Services.Implementations;

public class HomeSliderService : IHomeSliderService
{
    readonly VanguardContext _context;
    readonly IWebHostEnvironment _environment;

    public HomeSliderService(VanguardContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<HomeSlider> SliderEditAsync(HomeSlider slider)
    {
        var homeSlider = await _context.HomeSliders.FirstOrDefaultAsync(s => s.Id == slider.Id);
        if (homeSlider == null)
          ServiceResult.NotFound("Slider not found.");
        homeSlider.Title = slider.Title;
        homeSlider.Description = slider.Description;
        homeSlider.TagId = slider.TagId;
         ServiceResult.Ok("Slider edit successfully.");
        return homeSlider;
    }

    public async Task<ServiceResult> SliderImageCreateAsync(HomeSlider slider, string url)
    {
        Image Image = new Image();
        var path = Path.Combine(_environment.WebRootPath, "cilent", "assets", "settings", "slider");
        if(url!=null)
        await ImageDeleteAsync(url);

        var filename = await slider.ImageFile.SaveToAsync(path);

        Image = new Image
        {
            Url = filename,
            HomeSlider = slider,
        };

        slider.Image = Image;
        await _context.Images.AddAsync(Image);
        return ServiceResult.Ok("Image created successfully.");
    }



    public async Task<ServiceResult> ImageDeleteAsync(string url)
    {
        var path = Path.Combine(_environment.WebRootPath, "client", "assets", "settings", "slider");
        Image image = await _context.Images.FirstOrDefaultAsync(i => i.Url == url);
        if (image == null)
            return ServiceResult.NotFound("Image not found.");

        string fileName = image.Url;
        if (string.IsNullOrEmpty(fileName))
            return ServiceResult.BadRequest("File name is invalid.");

        ImageFileExtension.DeleteImagesService(path, fileName);
        _context.Images.Remove(image);
        await _context.SaveChangesAsync();

        return ServiceResult.Ok("Image deleted successfully.");
    }
}
