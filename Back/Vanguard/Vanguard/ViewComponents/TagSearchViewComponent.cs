
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Data;
using Vanguard.Models;

namespace Vanguard.ViewComponents;

[ViewComponent]
public class TagSearchViewComponent : ViewComponent
{
    private readonly VanguardContext _context;

    public TagSearchViewComponent(VanguardContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            ViewData["TagsSearch"] = null;
            return View(new List<Tag>());
        }

        text = text.ToLower().Trim();

        List<Tag> tags = await _context.Tags
                                       .Where(p => !p.IsDeleted && p.Name.Contains(text))
                                       .ToListAsync();

        ViewData["TagsSearch"] = tags;

        return View();
    }
}
