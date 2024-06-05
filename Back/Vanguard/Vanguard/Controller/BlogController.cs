using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.ViewModels.Blog;
using Vanguard.Data;
using Vanguard.Models;
using Vanguard.ViewModels.Blog;
using Vanguard.ViewModels.Shop;

namespace Vanguard.Controller;

public class BlogController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly VanguardContext _context;

    public BlogController(VanguardContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(BlogViewVM postMV)
    {
        var blogs = await _context.Blogs

            .Where(b => !b.IsDeleted)
            .Include(b => b.Images)
            .Include(b => b.AppUser)
            .ThenInclude(b => b.AllowedEmployee!.Role)
            .Include(b => b.Categories)
            .ThenInclude(bc => bc.Category)
            .ToListAsync();

        var categories = blogs
            .SelectMany(b => b.Categories)
            .Select(bc => bc.Category)
            .Distinct()
            .ToList();

        if (postMV.SelectedCategory > 0)
        {
            blogs = blogs.Where(b => b.Categories.Any(c => c.CategoryId == postMV.SelectedCategory)).ToList();
        }

        int totalItems = blogs.Count;

        var pageSize = 9;
        var pageNumber = postMV.PageInfo?.CurrentPage ?? 1;

        var pagedBlogs = blogs.Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .ToList();

        var blogVMs = pagedBlogs.Select(b => new BlogAllVM
        {
            Id = b.Id,
            Title = b.Title,
            Clicked = b.Clickeds,
            Image = b.Images.FirstOrDefault(i => !i.IsDeleted && i.IsMain).Url,
            IsVideo = b.Images.FirstOrDefault(i => !i.IsDeleted && i.IsMain).IsVideo,
            Created = b.CreatedDate,
            CreatedBy = b.AppUser.FullName!,
        }).ToList();

        BlogViewVM vm = new BlogViewVM
        {
            Blogs = blogVMs,
            Categories = categories,
            SelectedCategory = postMV.SelectedCategory,
            PageInfo = new PageInfo
            {
                CurrentPage = pageNumber,
                ItemsPerPage = pageSize,
                TotalItems = totalItems
            }
        };

        return View(vm);
    }



    public async Task<IActionResult> Detail(int? id)
    {
        if(id == null||id<1) return BadRequest();

        var blog = await _context.Blogs
         .Where(b => !b.IsDeleted)
         .Include(b => b.Images)
         .Include(b => b.AppUser)
         .ThenInclude(b => b.AllowedEmployee!.Role)
         .Include(b => b.Categories)
         .ThenInclude(bc => bc.Category)
         .FirstOrDefaultAsync(b=>b.Id==id);


        var Categorblogs = await _context.Blogs
                                .Include(b => b.Categories)
                                .ThenInclude(bc => bc.Category)
                                .ToListAsync();

        var categories = Categorblogs
            .SelectMany(b => b.Categories)
            .Select(bc => bc.Category)
            .Distinct()
            .ToList();



        BlogDetailVM vm = new BlogDetailVM
        {
            Blog = blog,
            Categories = categories,
        };
        return View(vm);
    }
}
