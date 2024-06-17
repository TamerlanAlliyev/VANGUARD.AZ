using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.Services.Interfaces;
using Vanguard.Areas.Admin.ViewModels.ProductViewModels;
using Vanguard.Data;
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
    public ProductController(VanguardContext context, IProductService productService)
    {
        _context = context;
        _productService = productService;
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
        catch (Exception ex)
        {
            return View("Error500", new ServiceResult(false, ex.Message, 500));
        }
    }



    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateVM vm)
    {
        try
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

            await _productService.CreateAsync(vm);
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