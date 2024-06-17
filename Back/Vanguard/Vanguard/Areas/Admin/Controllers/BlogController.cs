using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Vanguard.Areas.Admin.Services.Interfaces;
using Vanguard.Areas.Admin.ViewModels.Blog;
using Vanguard.Areas.Admin.ViewModels.CategoryViewModels;
using Vanguard.Areas.Admin.ViewModels.TagViewModels;
using Vanguard.Data;
using Vanguard.Extensions;
using Vanguard.Helpers;
using Vanguard.Models;
using YourNamespace.Filters;

namespace Vanguard.Areas.Admin.Controllers;
[Authorize(Roles = "Admin, Author")]
[Area("Admin")]
[ServiceFilter(typeof(AdminAuthorizationFilter))]
public class BlogController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly IProductService _productService;
    readonly UserManager<AppUser> _userManager;
    readonly VanguardContext _context;
    readonly IWebHostEnvironment _environment;
    readonly IBlogService _blogService;
    public BlogController(IProductService productService, UserManager<AppUser> userManager, VanguardContext context, IWebHostEnvironment environment, IBlogService blogService)
    {
        _productService = productService;
        _userManager = userManager;
        _context = context;
        _environment = environment;
        _blogService = blogService;
    }

    public async Task<IActionResult> Index()
    {
        var query = _context.Blogs.Where(b => !b.IsDeleted)
                                  .OrderByDescending(b => b.Id)
                                  .Include(b => b.AppUser)
                                  .Include(b => b.Images)
                                  .AsQueryable();


        var blogs = await query.Select(b => new BlogVM
        {
            Id = b.Id,
            Title = b.Title,
            Image = b.Images.FirstOrDefault(i => i.IsMain)!.Url,
            User = b.AppUser.FullName,
            Clicked = b.Clickeds,
            CreateDate = b.CreatedDate,
            IsViedo = b.Images.Any(i => i.IsMain && i.IsVideo),
        })
            .ToListAsync();
        return View(blogs);
    }

    public async Task<IActionResult> SoftDelete(int id)
    {
        var blog = await _context.Blogs.FirstOrDefaultAsync(b => !b.IsDeleted && b.Id == id);

        if (blog != null)
        {
            blog.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        else
        {
            return NotFound();
        }
    }

    public async Task<IActionResult> Create()
    {
        BlogCreateVM vm = new BlogCreateVM
        {
            CategorySelection = await _productService.CategorySelectionsAsync(),
            TagSelections = await _productService.TagSelectionsAsync(),
        };
        return View(vm);
    }




    [HttpPost]
    public async Task<IActionResult> Create(BlogCreateVM vm)
    {
        try
        {
            ModelState.Clear();

        ValidationHelper.ValidateBlogCreate(vm, ModelState);
        if (!ModelState.IsValid)
        {
            vm.CategorySelection = await _productService.CategorySelectionsAsync();
            vm.TagSelections = await _productService.TagSelectionsAsync();
            return View(vm);
        }
        var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
        var author = await _context.Users.FirstOrDefaultAsync(u => u.Id == user!.Id);

        Blog blog =  _blogService.BlogModelCreateAsync(vm,author!);



        List<BlogCategory> categories = _blogService.BlogCategoriesCreateAsync(vm,blog);

        List<BlogTag> tags = _blogService.BlogTagsCreateAsync(vm,blog);

        List<Image> images = new List<Image>();

        if (vm.MainFile != null)
        {
            if (!vm.MainFile.FileSize(5) || !vm.MainFile.FileTypeAsync("image/", "video/")) 
            {
                ModelState.AddModelError("ProfilImage", "Invalid file type or size. Only image and video files are allowed.");
                return View(vm);
            }

            var path = Path.Combine(_environment.WebRootPath, "cilent", "assets", "blogs"); 
            var filename = await vm.MainFile.SaveToAsync(path);

            Image newImage = new Image
            {
                Url = filename,
                Blog = blog,
                IsMain = true,
                IsVideo = vm.MainFile.ContentType.StartsWith("video/") 
            };
            images.Add(newImage);
        }

        if (vm.AddinationFile != null)
        {
            foreach (var item in vm.AddinationFile)
            {
                if (!item.FileSize(5) || !item.FileTypeAsync("image/", "video/"))
                {
                    ModelState.AddModelError("ProfilImage", "Invalid file type or size. Only image and video files are allowed.");
                    return View(vm);
                }

                var path = Path.Combine(_environment.WebRootPath, "cilent", "assets", "blogs");
                var filename = await item.SaveToAsync(path); 

                Image newImage = new Image
                {
                    Url = filename,
                    Blog = blog,
                    IsMain = false,
                    IsVideo = item.ContentType.StartsWith("video/") 
                };
                images.Add(newImage);
            }
        }


        await _context.Blogs.AddAsync(blog);
        await _context.BlogCategory.AddRangeAsync(categories);
        await _context.BlogTag.AddRangeAsync(tags);
        await _context.Images.AddRangeAsync(images);
        await _context.SaveChangesAsync();


     
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
            return NotFound();
        }

        var blog = await _context.Blogs.Where(b => !b.IsDeleted)
                                .Include(b => b.AppUser)
                                .Include(b => b.Images)
                                .Include(b => b.Categories)
                                .ThenInclude(b => b.Category)
                                .Include(b => b.BlogTags)!
                                .ThenInclude(b => b.Tag)
                                .FirstOrDefaultAsync(b => b.Id == id);

        if (blog == null)
        {
            return NotFound();
        }


        BlogEditVM vm = new BlogEditVM
        {
            BlogId = blog.Id,
            Title = blog.Title,
            AddinationDescription = blog.AddinationDescription,
            MainDescription = blog.MainDescription,
            Images = blog.Images.Where(i => !i.IsDeleted).Select(i => new ImageVM
            {
                Id = i.Id,
                Url = i.Url,
                IsMain = i.IsMain,
                IsVideo = i.IsVideo,
            }).ToList(),
            CategorySelection = await _productService.CategorySelectionsAsync(),
            TagSelections = await _productService.TagSelectionsAsync(),
            BlogCategories = blog.Categories.Select(c => c.Category).ToList(),
            BlogTags = blog.BlogTags?.Select(t => t.Tag).ToList(),
        };
        return View(vm);
    }







    [HttpPost]
    public async Task<IActionResult> Edit(BlogEditVM vm)
    {
        var existingBlog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == vm.BlogId);
        if (existingBlog == null) 
        {
            return NotFound();
        }

        existingBlog.Title = vm.Title;
        existingBlog.MainDescription = vm.MainDescription;
        existingBlog.AddinationDescription = vm.AddinationDescription;

        // Kategorileri güncelle
        if (vm.SelectedCategoryIds != null)
        {
            var newCategories = await _blogService.BlogCategoriesCreateAsync(vm.SelectedCategoryIds, existingBlog);
            await _context.BlogCategory.AddRangeAsync(newCategories);
        }

        // Etiketleri güncelle
        if (vm.SelectedTagIds != null)
        {
            var newTags = await _blogService.BlogTagsCreateAsync(vm.SelectedTagIds, existingBlog);
            await _context.BlogTag.AddRangeAsync(newTags);
        }

        List<Image> images = new List<Image>();

        if (vm.MainFile != null)
        {
            if (!vm.MainFile.FileSize(5) || !vm.MainFile.FileTypeAsync("image/", "video/"))
            {
                ModelState.AddModelError("ProfilImage", "Invalid file type or size. Only image and video files are allowed.");
                return View(vm);
            }

            var existingMainImage = await _context.Images.FirstOrDefaultAsync(i => i.IsMain && i.BlogId == existingBlog.Id);
            if (existingMainImage != null)
            {
                await DeleteMedia(existingMainImage.Id);
                _context.Images.Remove(existingMainImage);
            }

            images.Add(await _blogService.ImageCreateAsync(vm.MainFile, true, vm.MainFile.ContentType.StartsWith("video/"), existingBlog));
        }

        if (vm.AddinationFile != null)
        {
            foreach (var item in vm.AddinationFile)
            {
                if (!item.FileSize(5) || !item.FileTypeAsync("image/", "video/"))
                {
                    ModelState.AddModelError("ProfilImage", "Invalid file type or size. Only image and video files are allowed.");
                    return View(vm);
                }

                images.Add(await _blogService.ImageCreateAsync(item, false, item.ContentType.StartsWith("video/"), existingBlog));
            }
        }

        await _context.Images.AddRangeAsync(images);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }






















    [HttpPost("Admin/Blog/CategoryDelete/{productId}/{catId}")]
    public async Task<IActionResult> CategoryDelete(int productId, int catId)
    {
        await _blogService.CategoryDeleteAsync(productId, catId);
        return Ok();
    }


    [HttpPost("Admin/Blog/TagDelete/{blogId}/{tagId}")]
    public async Task<IActionResult> TagDelete(int blogId, int tagId)
    {
        await _blogService.TagDeleteAsync(blogId, tagId);
        return Ok();
    }

    public async Task<IActionResult> DeleteMedia(int id)
    {
        await _blogService.ImageDeleteAsync(id);
        return Ok();
    }


}