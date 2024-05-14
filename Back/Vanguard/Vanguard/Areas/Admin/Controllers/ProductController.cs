using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.Services.Interfaces;
using Vanguard.Areas.Admin.ViewModels.CategoryViewModels;
using Vanguard.Areas.Admin.ViewModels.ProductViewModels;
using Vanguard.Areas.Admin.ViewModels.TagViewModels;
using Vanguard.Data;
using Vanguard.Extensions;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    readonly VanguardContext _context;
    readonly IWebHostEnvironment _environment;
    readonly IProductService _productService;

    public ProductController(VanguardContext context, IWebHostEnvironment environment, IProductService productService)
    {
        _context = context;
        _environment = environment;
        _productService = productService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Create()
    {
        ProductCreateVM vm = new ProductCreateVM
        {
            CategorySelection = await _productService.CategorySelectionsAsync(),
            TagSelections = await _productService.TagSelectionsAsync()
        };

        return View(vm);
    }




    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateVM vm)
    {

        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        Product product = new Product
        {
            Name = vm.Name,
            ShortDescription = vm.ShortDescription,
            Description = vm.Description,
            SellPrice = vm.SellPrice,
            DiscountPrice = vm.DiscountPrice,
        };



        List<Image> images = new List<Image>();

        if (vm.MainImage != null)
        {
            var fileName = await _productService.RootImageCreate(vm.MainImage);

            Image MainImage = _productService.ImageCreate(fileName, product, true, false);

            images.Add(MainImage);
        }


        if (vm.HoverImage != null)
        {
            var fileName = await _productService.RootImageCreate(vm.HoverImage);

            Image HoverImage = _productService.ImageCreate(fileName, product, false, true);

            images.Add(HoverImage);
        }


        if (vm.AdditionalImages != null)
        {
            foreach (var image in vm.AdditionalImages)
            {
                var fileName = await _productService.RootImageCreate(image);

                Image AdditionalImages = _productService.ImageCreate(fileName, product, false, false);

                images.Add(AdditionalImages);
            }
        }



        List<ProductTag> ProductTags = new List<ProductTag>();

        if (vm.SelectedTagIds != null)
        {

            foreach (var tagId in vm.SelectedTagIds)
            {
                ProductTag productTag = new ProductTag
                {
                    TagId = tagId,
                    Product = product
                };
                ProductTags.Add(productTag);
            }

        }



        List<ProductCategory> ProductCategories = new List<ProductCategory>();

        if (vm.SelectedCategoryIds !=null)
        {
            foreach (var catId in vm.SelectedCategoryIds)
            {
                ProductCategory productCategory = new ProductCategory
                {
                    CategoryId = catId,
                    Product = product
                };
                ProductCategories.Add(productCategory);
            }
        }


        await _context.Products.AddAsync(product);
        await _context.Images.AddRangeAsync(images);
        await _context.ProductTag.AddRangeAsync(ProductTags);
        await _context.ProductCategory.AddRangeAsync(ProductCategories);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
