using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Linq;
using Vanguard.Data;
using Vanguard.Models;
using Vanguard.ViewModels.Shop;
using Vanguard.ViewModels.Shop.AdditionVMs;

namespace Vanguard.Controller;

public class ShopController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly VanguardContext _context;

    public ShopController(VanguardContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(ShopVM shopVM)
    {
        var query = _context.Products.Where(p => !p.IsDeleted)
                                     .Include(p => p.Images.Where(i => !i.IsDeleted))
                                     .Include(p => p.ProductColors)
                                       .ThenInclude(pc => pc.Color)
                                     .Include(p => p.ProductCategory)
                                       .ThenInclude(pc => pc.Category)
                                     .Include(p => p.ProductTag)
                                       .ThenInclude(pt => pt.Tag)
                                     .Include(p => p.Information)
                                       .ThenInclude(i => i.Size)
                                     .AsQueryable();

        var totalItems = await query.CountAsync();

        List<SelectedCats> cats = new List<SelectedCats>();

        if (shopVM.SelectedCategories != null && shopVM.SelectedCategories.Any())
        {
            foreach (var cat in shopVM.SelectedCategories)
            {
                SelectedCats ca = new SelectedCats
                {
                    catId = cat,
                    select = true,
                };
                cats.Add(ca);
            }
            var selectedCategoryIds = cats.Select(c => c.catId).ToList();

            query = query.Where(p => p.ProductCategory.Any(pc => selectedCategoryIds.Contains(pc.CategoryId)));
        }



        var products = await query
                            .OrderByDescending(p => p.Id)
                            .Skip((shopVM.PageInfo.CurrentPage - 1) * shopVM.PageInfo.ItemsPerPage)
                            .Take(shopVM.PageInfo.ItemsPerPage)
                            .ToListAsync();



        var shopProductVMs = products.Select(p => new ShopProductVM
        {
            Product = p,
            MainImageURL = p.Images.FirstOrDefault(i => i.IsMain)!.Url,
            HoverImageURL = p.Images.FirstOrDefault(i => i.IsHover)!.Url,
            AdditionalImagesURL = p.Images
                .Where(i => !i.IsMain && !i.IsHover)
                .Select(i => i.Url)
                .ToList(),
            Color = p.ProductColors.Color.Name,
            Categories = p.ProductCategory.Select(pc => pc.Category.Name).ToList(),
            Tags = p.ProductTag.Select(pc => pc.Tag.Name).ToList(),
            Informations = p.Information.ToList()!,
        }).ToList();


        var categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();

        var categorySelectings = categories.Select(category => new CategorySelecting
        {
            Category = category,
        }).ToList();

        ShopVM vm = new ShopVM
        {
            Product = shopProductVMs,
            CategorySelecting = categorySelectings,
            SelectedCats = cats,
            Grid = shopVM.Grid,
            PageInfo = new PageInfo
            {
                CurrentPage = shopVM.PageInfo.CurrentPage,
                TotalItems = totalItems,
                ItemsPerPage = shopVM.PageInfo.ItemsPerPage,
            }
        };


        return View(vm);
    }


}





