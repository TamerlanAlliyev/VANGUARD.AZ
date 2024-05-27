using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Data;
using Vanguard.ViewModels.Shop;

namespace Vanguard.ViewComponents;
[ViewComponent]

public class ShopProductViewComponents : ViewComponent
{
    readonly VanguardContext _context;

    public ShopProductViewComponents(VanguardContext context)
    {
        _context = context;
    }
    public async Task<IViewComponentResult> InvokeAsync(int page=1, int pageSize = 2)
    {
        var query = _context.Products
                                    .Where(p => !p.IsDeleted)
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
        var products = await query
                            .OrderByDescending(p => p.Id)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
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


        ShopVM vm = new ShopVM
        {
            Product = shopProductVMs,
            script = Url.Content("<script src=\"~/cilent/assets/js/shop.js\"></script>"),
            PageInfo = new PageInfo
            {
                CurrentPage = page,
                TotalItems = totalItems,
                ItemsPerPage = pageSize,
            }
        };

        return View(vm);
    }
}











//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Vanguard.Data;
//using Vanguard.ViewModels.Shop;

//namespace Vanguard.ViewComponents;
//[ViewComponent]

//public class ShopProductViewComponents:ViewComponent
//{
//    readonly VanguardContext _context;

//    public ShopProductViewComponents(VanguardContext context)
//    {
//        _context = context;
//    }
//    public async Task<IViewComponentResult> InvokeAsync()
//    {
//        var products = await _context.Products
//                                         .Where(p => !p.IsDeleted)
//                                         .OrderByDescending(p => p.Id)
//                                         .Take(12)
//                                         .Include(p => p.Images.Where(i => !i.IsDeleted))
//                                         .Include(p => p.ProductColors)
//                                           .ThenInclude(p => p.Color)
//                                         .Include(p => p.ProductCategory)
//                                           .ThenInclude(p => p.Category)
//                                         .Include(p => p.ProductTag)
//                                           .ThenInclude(p => p.Tag)
//                                         .Include(p => p.Information)
//                                           .ThenInclude(p => p.Size)
//                                         .ToListAsync();

//        var shopProductVMs = products.Select(p => new ShopProductVM
//        {
//            Product = p,
//            MainImageURL = p.Images.FirstOrDefault(i => i.IsMain)!.Url,
//            HoverImageURL = p.Images.FirstOrDefault(i => i.IsHover)!.Url,
//            AdditionalImagesURL = p.Images
//                .Where(i => !i.IsMain && !i.IsHover)
//                .Select(i => i.Url)
//                .ToList(),
//            Color = p.ProductColors.Color.Name,
//            Categories = p.ProductCategory.Select(pc => pc.Category.Name).ToList(),
//            Tags = p.ProductTag.Select(pc => pc.Tag.Name).ToList(),
//            Informations = p.Information.ToList()!,
//        }).ToList();


//        ShopVM vm = new ShopVM
//        {
//            Product = shopProductVMs
//        };
//        return View(vm);
//    }
//}
