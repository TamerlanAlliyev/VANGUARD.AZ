using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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

    readonly UserManager<AppUser> _userManager;
    public BlogController(VanguardContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(BlogViewVM postMV, int[]? tagId)
    {
        var query = _context.Blogs
            .Where(b => !b.IsDeleted)
            .Include(b => b.Images)
            .Include(b => b.AppUser)
            .ThenInclude(b => b.AllowedEmployee!.Role)
            .Include(b=>b.BlogComments)
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
            Comment = b.BlogComments!.Count()
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
        if (id == null || id < 1) return BadRequest();


        var query = _context.Blogs
                            .Where(b => !b.IsDeleted)
                            .Include(b => b.Images)
                            .Include(b => b.AppUser)
                            .ThenInclude(b => b.AllowedEmployee!.Role)
                            .Include(b => b.BlogComments)
                            .Include(b => b.Categories)
                            .ThenInclude(bc => bc.Category)
                            .Include(b => b.BlogTags).AsQueryable();


        var blog = await query
         .Where(b => !b.IsDeleted)
         .Include(b => b.Images)
         .Include(b => b.AppUser)
         .ThenInclude(b => b.AllowedEmployee!.Role)
         .Include(b => b.Categories)
         .ThenInclude(bc => bc.Category)
         .Select(b => new BlogGetVM
         {
             Title = b.Title,
             MainDescription = b.MainDescription,
             AddinationDescription = b.AddinationDescription,
             Author = b.AppUser.FullName,
             CreatedBy = b.AppUser.FullName,
             Created = b.CreatedDate,
             Clicked = b.Clickeds,
             Id = b.Id,
             Image = b.Images!.Where(b => b.IsMain && !b.IsDeleted).Select(i => new BlogImageVM
             {
                 Url = i.Url,
                 IsVideo = i.IsVideo,
             })!.FirstOrDefault()!,
             AddunationImages = b.Images.Where(b => !b.IsMain && !b.IsDeleted).Select(i => new BlogImageVM
             {
                 Url = i.Url,
                 IsVideo = i.IsVideo,
             }).ToList(),
             Categories = b.Categories.Select(c => c.Category.Name).ToList(),
             Comments = b.BlogComments!.Count()

         })
         .FirstOrDefaultAsync(b => b.Id == id);


        var popularBlogs = query
         .Where(b => !b.IsDeleted)
         .OrderByDescending(b => b.Clickeds)
         .Take(5)
         .Select(b => new BlogAllVM
         {
             Id = b.Id,
             Title = b.Title,
             Clicked = b.Clickeds,
             Image = b.Images!.FirstOrDefault(i => !i.IsDeleted && i.IsMain)!.Url,
             IsVideo = b.Images!.FirstOrDefault(i => !i.IsDeleted && i.IsMain)!.IsVideo,
             Created = b.CreatedDate,
             CreatedBy = b.AppUser.FullName!,
             Author = b.AppUser.FullName!,
         }).ToList();




        var categories = query
                        .SelectMany(b => b.Categories)
                        .Select(bc => bc.Category)
                        .Distinct()
                        .ToList();


        var tags = query.Where(b => b.Id == id).SelectMany(b => b.BlogTags)
                        .Select(bc => bc.Tag)
                        .Distinct()
                        .ToList();
        
        var comments = await _context.BlogComments.Where(b => b.BlogId == id).Include(c=>c.AppUser).ThenInclude(u=>u.Image).ToListAsync();

        BlogDetailVM vm = new BlogDetailVM
        {
            Blog = blog,
            Categories = categories,
            PopularBlogs = popularBlogs,
            Tags = tags,
            Comments = comments

        };
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

    [HttpPost]
    public async Task<IActionResult> AddComment(BlogDetailVM comment)
    {
        var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
        if (user == null)
        {
            return Unauthorized();
        }
        BlogComment com = new BlogComment
        {
            BlogId=comment.BlogComment.BlogId,
            Comment = comment.BlogComment.Comment,
            AppUser = user,
        };
        await _context.BlogComments.AddAsync(com);
        await _context.SaveChangesAsync();
        return RedirectToAction("Detail", new {Id= comment.BlogComment.BlogId });
    }
}
