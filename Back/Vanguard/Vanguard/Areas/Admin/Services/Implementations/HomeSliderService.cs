using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.Services.Interfaces;
using Vanguard.Data;
using Vanguard.Extensions;
using Vanguard.Helpers;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Services.Implementations;

public class HomeSliderService: IHomeSliderService
{
    readonly VanguardContext _context;
    readonly IWebHostEnvironment _environment;

    public HomeSliderService(VanguardContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<ServiceResult> SliderImageCreateAsync(HomeSlider homeSlider)
    {
        try
        {
            if (homeSlider.ImageFile != null)
            {
                var path = Path.Combine(_environment.WebRootPath, "client", "assets", "settings", "slider");

                if (homeSlider.Image != null)
                    await ImageDeleteAsync(homeSlider.Image.Url);

                var filename = await homeSlider.ImageFile.SaveToAsync(path);

                Image image = new Image
                {
                    Url = filename,
                    HomeSlider = homeSlider
                };

                homeSlider.Image = image;

                await _context.Images.AddAsync(image);
            }

            await _context.SaveChangesAsync();
            return ServiceResult.Ok("Slider image created successfully.");
        }
        catch (Exception ex)
        {
            return ServiceResult.InternalServerError($"Internal server error: {ex.Message}");
        }
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
}
