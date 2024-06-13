using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Data;
using Vanguard.Models;

namespace Vanguard.ViewComponents;

public class SearchViewComponent : ViewComponent
{
    readonly VanguardContext _context;

    public SearchViewComponent(VanguardContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(string? text)
    {
        if (text == null)
        {
            return View(new List<Product>());
        }
        text = text.ToLower().Trim();



        var products = await _context.Products
                                     .Where(p => !p.IsDeleted && p.Name.Contains(text))
                                     .Include(p=>p.Images)
                                     .Include(p=>p.ProductCategory)
                                     .ThenInclude(p=>p.Category)
                                     .ToListAsync();
        return View(products);
    }
}
