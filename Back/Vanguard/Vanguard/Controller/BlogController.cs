using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.ViewModels.Blog;
using Vanguard.Data;
using Vanguard.Models;
using Vanguard.ViewModels.Blog;
using Vanguard.ViewModels.Shop;
using static System.Net.Mime.MediaTypeNames;

namespace Vanguard.Controller;

public class BlogController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly VanguardContext _context;

    public BlogController(VanguardContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(BlogViewVM postMV, int[]? tagId)
    {
        var query = _context.Blogs
            .Where(b => !b.IsDeleted)
            .Include(b => b.Images)
            .Include(b => b.AppUser)
            .ThenInclude(b => b.AllowedEmployee!.Role)
            .Include(b => b.Categories)
            .ThenInclude(bc => bc.Category)
            .Include(b => b.BlogTags).AsQueryable(); 

        if (tagId != null && tagId.Length > 0)
        {
            query = query.Where(b => b.BlogTags.Any(bt => tagId.Contains(bt.TagId)));
        }

        var blogs = await query.ToListAsync();

        var categories = blogs
            .SelectMany(b => b.Categories)
            .Select(bc => bc.Category)
            .Distinct()
            .ToList();

        var popularBlogs = blogs
            .Where(b => !b.IsDeleted)
            .OrderByDescending(b => b.Clickeds)
            .Take(5)
            .Select(b => new BlogAllVM
            {
                Id = b.Id,
                Title = b.Title,
                Clicked = b.Clickeds,
                Image = b.Images.FirstOrDefault(i => !i.IsDeleted && i.IsMain).Url,
                IsVideo = b.Images.FirstOrDefault(i => !i.IsDeleted && i.IsMain).IsVideo,
                Created = b.CreatedDate,
                CreatedBy = b.AppUser.FullName!,
                Author = b.AppUser.FullName!,
            }).ToList();

        if (postMV.SelectedCategory > 0)
        {
            blogs = blogs
                .Where(b => b.Categories.Any(c => c.CategoryId == postMV.SelectedCategory))
                .ToList();
        }

        int totalItems = blogs.Count;

        var pageSize = 9;
        var pageNumber = postMV.PageInfo?.CurrentPage ?? 1;

        var pagedBlogs = blogs
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
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
            PopularBlogs = popularBlogs,
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
        blog.Clickeds = blog.Clickeds == 0 || blog.Clickeds == null ? 1 : blog.Clickeds + 1;
        await _context.SaveChangesAsync();

        return View(vm);
    }


    public async Task<IActionResult> TagsSearch(string? text)
    {
        if (text == null)
        {
            ViewData["TagsSearch"] = null;
            return PartialView("_TagsSearchPartialView");
        }
        text = text.ToLower().Trim();

        List<Tag> tags = await _context.Tags
                                       .Where(p => !p.IsDeleted && p.Name.Contains(text))
                                       .ToListAsync();


        ViewData["TagsSearch"] = tags != null ? tags : null;

        return PartialView("_BlogTagsSearchPartialView");
    }


    //public async Task<IActionResult> TagSearch(string? text)
    //{
    //    List<Tag> tags = new List<Tag>();
    //    if (text == null)
    //    {
    //        return View(null);
    //    }

    //    text = text.ToLower().Trim();

    //    tags = await _context.Tags
    //        .Where(p => !p.IsDeleted && p.Name.Contains(text))
    //        .Include(p => p.BlogTags)
    //        .Where(p => p.BlogTags.Any(bt => bt.TagId == p.Id))
    //        .ToListAsync();


    //    return View(tags != null ? tags : null);
    //}
}
