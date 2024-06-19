using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Vanguard.Areas.Admin.Services.Interfaces;
using Vanguard.Areas.Admin.ViewModels.ProductViewModels;
using Vanguard.Data;
using Vanguard.Extensions;
using Vanguard.Helpers;
using Vanguard.Models;
using YourNamespace.Filters;


namespace Vanguard.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin, Editor")]
[ServiceFilter(typeof(AdminAuthorizationFilter))]

public class ProductController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly VanguardContext _context;
    readonly IProductService _productService;
    readonly IWebHostEnvironment _environment;
    public ProductController(VanguardContext context, IProductService productService, IWebHostEnvironment environment)
    {
        _context = context;
        _productService = productService;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            List<Product> products = await _context.Products
                                                    .Where(p => !p.IsDeleted)
                                                    .OrderByDescending(p => p.Id)
                                                    .Include(p => p.ProductColors)
                                                    .ThenInclude(p => p.Color)
                                                    .Include(p => p.Images)
                                                    .ToListAsync();
            return View(products);
        }
        catch (KeyNotFoundException ex)
        {
            return View("Error404", new ServiceResult(false, ex.Message, 404));
        }
        catch (ArgumentException ex)
        {
            return View("Error400", new ServiceResult(false, ex.Message, 400));
        }
        catch (UnauthorizedAccessException ex)
        {
            return View("Error401", new ServiceResult(false, ex.Message, 401));
        }
        catch (Exception ex)
        {
            return View("Error500", new ServiceResult(false, ex.Message, 500));
        }
    }

    public async Task<IActionResult> DeletedList()
    {
        try
        {
            List<Product> products = await _context.Products.Where(p => p.IsDeleted)
                                                            .OrderByDescending(p => p.Id)
                                                            .Include(p => p.ProductColors)
                                                            .ThenInclude(p => p.Color)
                                                            .Include(p => p.Images)
                                                            .ToListAsync();
            return View(products);
        }
        catch (KeyNotFoundException ex)
        {
            return View("Error404", new ServiceResult(false, ex.Message, 404));
        }
        catch (ArgumentException ex)
        {
            return View("Error400", new ServiceResult(false, ex.Message, 400));
        }
        catch (UnauthorizedAccessException ex)
        {
            return View("Error401", new ServiceResult(false, ex.Message, 401));
        }
        catch (Exception ex)
        {
            return View("Error500", new ServiceResult(false, ex.Message, 500));
        }
    }





    public async Task<IActionResult> Create()
    {
        try
        {
            ProductCreateVM vm = new ProductCreateVM
            {
                CategorySelection = await _productService.CategorySelectionsAsync(),
                TagSelections = await _productService.TagSelectionsAsync(),
                Colors = await _context.Colors.Where(c => !c.IsDeleted).ToListAsync(),
                Gender = await _context.Genders.ToListAsync(),
            };
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
        catch (UnauthorizedAccessException ex)
        {
            return View("Error401", new ServiceResult(false, ex.Message, 401));
        }
        catch (Exception ex)
        {
            return View("Error500", new ServiceResult(false, ex.Message, 500));
        }
    }



    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateVM vm)
    {
        ModelState.Clear();
        ValidationHelper.ValidateProductCreate(vm, ModelState);

        if (!ModelState.IsValid)
        {
            vm.CategorySelection = await _productService.CategorySelectionsAsync();
            vm.TagSelections = await _productService.TagSelectionsAsync();
            vm.Colors = await _context.Colors.Where(c => !c.IsDeleted).ToListAsync();
            vm.Gender = await _context.Genders.ToListAsync();
            return View(vm);
        }

        try
        {
            List<Color> colors = new List<Color>();
            List<Product> products = new List<Product>();
            List<Image> images = new List<Image>();
            List<Size> sizes = new List<Size>();
            List<Information> informations = new List<Information>();
            List<ProductCategory> productCategories = new List<ProductCategory>();
            List<ProductTag> productTags = new List<ProductTag>();
            List<ProductColor> productColors = new List<ProductColor>();

            var exsistsSize = await _context.Sizes.Where(s => !s.IsDeleted).ToListAsync();

            foreach (var item in vm.ColorSizeVM)
            {
                //Color Create
                Color color = new Color();
                if (_context.Colors.Where(c => !c.IsDeleted).Any(c => c.HexCode == item.HexCode))
                {
                    var col = await _context.Colors.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.HexCode == item.HexCode);
                    if (col != null)
                    {
                        color = col;
                    }
                }
                else
                {
                    color = new Color
                    {
                        Name = item.ColorName,
                        HexCode = item.HexCode,
                    };
                    colors.Add(color);
                }

                //Product Create
                Product product = new Product
                {
                    Name = vm.Name,
                    Model = vm.Model,
                    ShortDescription = vm.ShortDescription,
                    Description = vm.Description,
                    SellPrice = vm.SellPrice,
                    DiscountPrice = vm.DiscountPrice,
                    GenderId = vm.SelectedGender
                };
                products.Add(product);

                //ProductColor Create
                ProductColor productColor = new ProductColor
                {
                    Product = product,
                    Color = color
                };
                productColors.Add(productColor);

                //Category Selects
                if (vm.SelectedCategoryIds != null)
                {
                    foreach (var cat in vm.SelectedCategoryIds)
                    {
                        ProductCategory productCategory = new ProductCategory
                        {
                            CategoryId = cat,
                            Product = product
                        };
                        productCategories.Add(productCategory);
                    }
                }

                //Tag Selects
                if (vm.SelectedTagIds != null)
                {
                    foreach (var tag in vm.SelectedTagIds)
                    {
                        ProductTag productTag = new ProductTag
                        {
                            TagId = tag,
                            Product = product
                        };
                        productTags.Add(productTag);
                    }
                }


                //Images Create
                if (item.Images.MainImage != null)
                {
                    if (color != null)
                    {

                        if (!item.Images.MainImage.FileSize(5))
                        {
                            ModelState.AddModelError("Images.MainImage", "Invalid file type or size.");
                            return View(vm);

                        }
                        if (!item.Images.MainImage.FileTypeAsync("image"))
                        {
                            ModelState.AddModelError("Images.MainImage", "Files must be 'Image' type!");
                            return View(vm);

                        }
                        var path = Path.Combine(_environment.WebRootPath, "cilent", "assets", "images", "products");
                        var fileName = await item.Images.MainImage.SaveToAsync(path);

                        Image Image = new Image
                        {
                            Url = fileName,
                            IsMain = true,
                            IsHover = false,
                            Product = product,
                            Color = color
                        };
                        images.Add(Image);


                    }
                }

                if (item.Images.HoverImage != null)
                {
                    if (color != null)
                    {

                        if (!item.Images.HoverImage.FileSize(5))
                        {
                            ModelState.AddModelError("Images.MainImage", "Invalid file type or size.");
                            return View(vm);
                        }
                        if (!item.Images.HoverImage.FileTypeAsync("image"))
                        {
                            ModelState.AddModelError("Images.MainImage", "Files must be 'Image' type!");
                            return View(vm);

                        }
                        var path = Path.Combine(_environment.WebRootPath, "cilent", "assets", "images", "products");
                        var fileName = await item.Images.HoverImage.SaveToAsync(path);

                        Image Image = new Image
                        {
                            Url = fileName,
                            IsMain = false,
                            IsHover = true,
                            Product = product,
                            Color = color
                        };
                        images.Add(Image);


                    }
                }

                if (item.Images.AdditionalImages != null)
                {
                    foreach (var additionalImage in item.Images.AdditionalImages)
                    {
                        if (color != null)
                        {

                            if (!additionalImage.FileSize(5))
                            {
                                ModelState.AddModelError("Images.MainImage", "Invalid file type or size.");
                                return View(vm);

                            }
                            if (!additionalImage.FileTypeAsync("image"))
                            {
                                ModelState.AddModelError("Images.MainImage", "Files must be 'Image' type!");
                                return View(vm);

                            }
                            var path = Path.Combine(_environment.WebRootPath, "cilent", "assets", "images", "products");
                            var fileName = await additionalImage.SaveToAsync(path);

                            Image Image = new Image
                            {
                                Url = fileName,
                                IsMain = false,
                                IsHover = false,
                                Product = product,
                                Color = color
                            };
                            images.Add(Image);

                        }
                    }
                }






                if (item.Sizes != null)
                {
                    foreach (var siz in item.Sizes)
                    {
                        Size size = new Size();
                        Information info = new Information();

                        if (exsistsSize.Any(s => s.Name == siz.Size))
                        {
                            size = exsistsSize.FirstOrDefault(s => s.Name == siz.Size);
                            info = new Information
                            {
                                Product = product,
                                Color = color,
                                SizeId = size.Id,
                                Count = siz.Count,
                                Dimensions = siz.Dimensions,
                                Weight = siz.Weight
                            };
                            informations.Add(info);
                        }
                        else if (sizes.Any(s => s.Name == siz.Size))
                        {
                            size = sizes.FirstOrDefault(s => s.Name == siz.Size);
                            info = new Information
                            {
                                Product = product,
                                Color = color,
                                Size = size,
                                Count = siz.Count,
                                Dimensions = siz.Dimensions,
                                Weight = siz.Weight
                            };
                            informations.Add(info);
                        }
                        else
                        {
                            size = new Size
                            {
                                Name = siz.Size,
                            };
                            sizes.Add(size);

                            info = new Information
                            {
                                Product = product,
                                Color = color,
                                Size = size,
                                Count = siz.Count,
                                Dimensions = siz.Dimensions,
                                Weight = siz.Weight
                            };
                            informations.Add(info);
                        }
                    }
                }
            }

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                _context.Colors.AddRange(colors);
                _context.Products.AddRange(products);
                _context.Images.AddRange(images);
                _context.Sizes.AddRange(sizes);
                _context.Informations.AddRange(informations);
                _context.ProductCategory.AddRange(productCategories);
                _context.ProductTag.AddRange(productTags);
                _context.ProductColor.AddRange(productColors);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
        }
        catch (DbUpdateException ex)
        {
           
            var errorMessage = $"DB Update Exception: {ex.Message} InnerException: {ex.InnerException?.Message}";
      
            return View("Error500", new ServiceResult(false, errorMessage, 500));
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

        return RedirectToAction("Index");
    }




    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || id < 1)
        {
            return View("Error404");
        }

        try
        {
            ProductEditVM vm = await _productService.EditViewModelAsync(id);

            if (vm == null)
            {
                return View("Error400");
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
        catch (UnauthorizedAccessException ex)
        {
            return View("Error401", new ServiceResult(false, ex.Message, 401));
        }
        catch (Exception ex)
        {
            return View("Error500", new ServiceResult(false, ex.Message, 500));
        }
    }



    [HttpPost]
    public async Task<IActionResult> Edit(ProductEditVM vm)
    {
        try
        {
            ModelState.Clear();

            ValidationHelper.ValidateProduct(vm, ModelState);

            if (!ModelState.IsValid)
            {
                var edVM = await _productService.EditViewModelAsync(vm.Id);
                vm.ColorSizeVM = edVM.ColorSizeVM;
                vm.SelectedCategories = edVM.SelectedCategories;
                vm.SelectedTags = edVM.SelectedTags;
                vm.CategorySelection = await _productService.CategorySelectionsAsync();
                vm.TagSelections = await _productService.TagSelectionsAsync();
                //vm.Colors = await _context.Colors.Where(c => !c.IsDeleted).ToListAsync();
                return View(vm);
            }

            await _productService.EditAsync(vm);
            return RedirectToAction("Index");
        }
        catch (KeyNotFoundException ex)
        {
            return View("Error404", new ServiceResult(false, ex.Message, 404));
        }
        catch (ArgumentException ex)
        {
            return View("Error400", new ServiceResult(false, ex.Message, 400));
        }
        catch (UnauthorizedAccessException ex)
        {
            return View("Error401", new ServiceResult(false, ex.Message, 401));
        }
        catch (Exception ex)
        {
            return View("Error500", new ServiceResult(false, ex.Message, 500));
        }
    }





    public async Task<IActionResult> Detail(int? id)
    {
        try
        {
            var vm = await _productService.DetailAsync(id);
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
        catch (UnauthorizedAccessException ex)
        {
            return View("Error401", new ServiceResult(false, ex.Message, 401));
        }
        catch (Exception ex)
        {
            return View("Error500", new ServiceResult(false, ex.Message, 500));
        }

    }







    [HttpPost("Admin/Product/ImageDelete/{url}")]
    public async Task<IActionResult> ImageDelete(string url)
    {
        await _productService.ImageDeleteAsync(url);
        return Ok();
    }

    [HttpPost("Admin/Product/CategoryDelete/{productId}/{catId}")]
    public async Task<IActionResult> CategoryDelete(int productId, int catId)
    {
        await _productService.CategoryDeleteAsync(productId, catId);
        return Ok();
    }


    [HttpPost("Admin/Product/TagDelete/{productId}/{tagId}")]
    public async Task<IActionResult> TagDelete(int productId, int tagId)
    {
        await _productService.TagDeleteAsync(productId, tagId);
        return Ok();
    }

    [HttpPost("Admin/Product/SoftDelete/{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        Product? product = await _context.Products.FirstOrDefaultAsync(p => !p.IsDeleted && p.Id == id);
        product.IsDeleted = true;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("Admin/Product/HardDelete/{id}")]
    public async Task<IActionResult> HardDelete(int id)
    {
        await _productService.HardDeleteAsync(id);
        return Ok();
    }

    [HttpPost("Admin/Product/Recover/{id}")]
    public async Task<IActionResult> Recover(int id)
    {
        Product? product = await _context.Products.FirstOrDefaultAsync(p => p.IsDeleted && p.Id == id);
        product.IsDeleted = false;
        await _context.SaveChangesAsync();
        return Ok();
    }






    [HttpPost("Admin/Product/InformationDelete/{id}")]
    public async Task<IActionResult> InformationDelete(int? id)
    {
        try
        {
            await _productService.InformationDeleteAsync(id);
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
}