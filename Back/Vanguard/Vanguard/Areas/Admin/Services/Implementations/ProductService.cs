using Vanguard.Areas.Admin.Services.Interfaces;
using Vanguard.Models;
using Vanguard.Extensions;
using Vanguard.Areas.Admin.ViewModels.TagViewModels;
using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.ViewModels.CategoryViewModels;
namespace Vanguard.Areas.Admin.Services.Implementations;

public class ProductService : IProductService
{
    readonly IWebHostEnvironment _environment;
    readonly ITagService _tagService;
    readonly ICategoryService _categoryService;
    public ProductService(IWebHostEnvironment environment, ITagService tagService, ICategoryService categoryService)
    {
        _environment = environment;
        _tagService = tagService;
        _categoryService = categoryService;
    }

    public async Task<string> RootImageCreate(IFormFile imageFile)
    {

        if (!imageFile.FileSize(5) || !imageFile.FileTypeAsync("image/"))
        {
            throw new ArgumentException("Invalid file type or size.");
        }
        if (!imageFile.FileTypeAsync("image"))
        {
            throw new ArgumentException("MainImage", "Files must be 'Image' type!");
        }
        var path = Path.Combine(_environment.WebRootPath, "cilent", "assets", "images", "products");
        var fileName = await imageFile.SaveToAsync(path);

        return fileName;
    }

    public Image ImageCreate(string ImageName, Product Product, bool IsMain, bool IsHover)
    {
        Image Image = new Image
        {
            Url = ImageName,
            IsMain = IsMain,
            IsHover = IsHover,
            Product = Product,
        };
        return Image;
    }



    public async Task<List<TagSelectionVM>> TagSelectionsAsync()
    {
        List<TagSelectionVM> tagSelections = new List<TagSelectionVM>();

        List<Tag> tags = await _tagService.GetAllAsync();

        foreach (var item in tags)
        {
            TagSelectionVM select = new TagSelectionVM
            {
                Id = item.Id,
                Name = item.Name
            };

            tagSelections.Add(select);
        }

        return tagSelections;
    }



    public async Task<List<CategorySelectionVM>> CategorySelectionsAsync()
    {
        List<CategorySelectionVM> categorySelections = new List<CategorySelectionVM>();

        List<Category> categories = await _categoryService.GetAllAsync();

        foreach (var cat in categories)
        {
            CategorySelectionVM select = new CategorySelectionVM
            {
                Id = cat.Id,
                Name = cat.Name,
                Parent = cat.ParentCategory?.Name
            };
            categorySelections.Add(select);
        }

        return categorySelections;
    }

}
