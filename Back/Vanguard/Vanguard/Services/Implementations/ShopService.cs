using Microsoft.EntityFrameworkCore;
using Vanguard.Data;
using Vanguard.Services.Interfaces;
using Vanguard.Models;
using Vanguard.ViewModels.Shop.AdditionVMs;
using Vanguard.ViewModels.Shop;
using Vanguard.Helpers;

namespace Vanguard.Services.Implementations;

public class ShopService : IShopService
{
    readonly VanguardContext _context;

    public ShopService(VanguardContext context)
    {
        _context = context;
    }

    public IQueryable<Product> ProductgetQuery()
    {
        return _context.Products.Where(p => !p.IsDeleted)
            .Include(p => p.Images.Where(i => !i.IsDeleted))
            .Include(p => p.ProductColors).ThenInclude(pc => pc.Color)
            .Include(p => p.ProductCategory).ThenInclude(pc => pc.Category)
            .Include(p => p.ProductTag).ThenInclude(pt => pt.Tag)
            .Include(p => p.Information).ThenInclude(i => i.Size).AsQueryable();
    }









    //SHOP Detail
    public async Task<Product?> ProductGetAsync(int id)
    {

        Product? product = await _context.Products.Where(p => !p.IsDeleted && p.Id == id)
                                                 .Include(p => p.Images)
                                                 .Include(p => p.Information)
                                                 .ThenInclude(p => p.Size)
                                                 .Include(p => p.ProductCategory)
                                                 .ThenInclude(p => p.Category)
                                                 .Include(p => p.ProductTag)
                                                 .ThenInclude(p => p.Tag)
                                                 .FirstOrDefaultAsync();
        return product;
    }

    public async Task<List<Product>> ProductModelAsync(string model)
    {
        List<Product> models = await _context.Products.Where(p => !p.IsDeleted && p.Model == model)
                                            .Include(i => i.Images)
                                            .Include(i => i.Information)
                                            .Include(c => c.ProductColors)
                                            .ThenInclude(c => c.Color)
                                            .ToListAsync();
        return models;
    }

    public async Task<ShopDetailVM?> DetailAsync(int id)
    {

        Product? product = await ProductGetAsync(id);

        if (product == null)
        {
            ServiceResult.NotFound("Product not found.");
            return null;
        }
        else
        {
            var models = await ProductModelAsync(product.Model);

            var productTagIds = product.ProductTag.Select(pt => pt.TagId).ToList();
            var productCategoryIds = product.ProductCategory.Select(pc => pc.CategoryId).ToList();

            var relatedProductsQuery = _context.Products
                                               .Where(p => !p.IsDeleted &&
                                                          p.ProductTag.Any(pt => productTagIds.Contains(pt.TagId)) &&
                                                          p.ProductCategory.Any(pc => productCategoryIds.Contains(pc.CategoryId)))
                                               .OrderByDescending(p => p.Id)
                                               .Take(10)
                                               .Include(p => p.Images)
                                               .Include(p => p.ProductCategory)
                                                  .ThenInclude(pc => pc.Category)
                                               .Include(p => p.ProductTag)
                                                  .ThenInclude(pt => pt.Tag);

            var relatedProducts = await relatedProductsQuery.Include(p => p.Information)
                                                   .ThenInclude(p => p.Color).ToListAsync();

            ShopDetailVM vm = new ShopDetailVM
            {
                Product = product,
                MainImageURL = product.Images!.Where(i => !i.IsDeleted && i.IsMain)!.FirstOrDefault()!.Url,
                HoverImageURL = product.Images!.Where(i => !i.IsDeleted && i.IsHover)!.FirstOrDefault()!.Url,
                AdditionalImagesURL = product.Images!.Where(i => !i.IsDeleted && !i.IsMain && !i.IsHover)!.Select(i => i.Url).ToList(),
                Colors = models.Select(c => new ProductColorVM
                {
                    Color = c.ProductColors.Color.Name,
                    Id = c.Id,
                    Url = c.Images.Where(i => !i.IsDeleted && i.IsMain).FirstOrDefault()!.Url,
                    totalCount = c.Information.Sum(i => i.Count),
                }).ToList(),
                Information = product.Information.ToList(),
                RelatedProducts = relatedProducts
            };

            product.ClicketCount = (product.ClicketCount == null ? 0 : product.ClicketCount) + 1;
            await _context.SaveChangesAsync();

            return vm;
        }

    }
}
