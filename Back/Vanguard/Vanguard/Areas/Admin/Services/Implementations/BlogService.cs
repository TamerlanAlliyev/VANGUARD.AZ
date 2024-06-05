using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Vanguard.Areas.Admin.Services.Interfaces;
using Vanguard.Data;
using Vanguard.Extensions;
using Vanguard.Helpers;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Services.Implementations;

public class BlogService : IBlogService
{
    readonly IWebHostEnvironment _environment;
    readonly VanguardContext _context;
    public BlogService(IWebHostEnvironment environment, VanguardContext context)
    {
        _environment = environment;
        _context = context;
    }

    public async Task<ServiceResult> ImageDeleteAsync(int id)
    {
        var path = Path.Combine(_environment.WebRootPath, "cilent", "assets", "blogs");
        Image image = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);
        if (image == null) ServiceResult.NotFound("Image not found.");

        string fileName = image.Url;

        if (string.IsNullOrEmpty(fileName)) ServiceResult.BadRequest("File name is invalid.");

        try
        {
            ImageFileExtension.DeleteImagesService(path, fileName);
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return ServiceResult.Ok("Image deleted successfully.");
        }
        catch (Exception ex)
        {
            return ServiceResult.InternalServerError($"Internal server error: {ex.Message}");
        }
    }


    public async Task CategoryDeleteAsync(int blogId, int catId)
    {
        var blogCategoryDel = await _context.BlogCategory
            .FirstOrDefaultAsync(pc => pc.BlogId == blogId && pc.CategoryId == catId);

        if (blogCategoryDel != null)
        {
            _context.BlogCategory.Remove(blogCategoryDel);
            await _context.SaveChangesAsync();
        }
    }

    public async Task TagDeleteAsync(int blogId, int tagId)
    {
        var blogTagDel = await _context.BlogTag
              .FirstOrDefaultAsync(pc => pc.BlogId == blogId && pc.TagId == tagId);

        if (blogTagDel != null)
        {
            _context.BlogTag.Remove(blogTagDel);
            await _context.SaveChangesAsync();
        }
    }



    public async Task<List<BlogCategory>> BlogCategoriesCreateAsync(int[] SelectedCategoryIds, Blog blog)
    {
        List<BlogCategory> categories = new List<BlogCategory>();

        List<BlogCategory> exsistCategory = await _context.BlogCategory.ToListAsync();
        if (SelectedCategoryIds != null)
        {
            foreach (var item in SelectedCategoryIds)
            {
                if (exsistCategory.FirstOrDefault(bc => bc.CategoryId == item && bc.BlogId == blog.Id) == null)
                {
                    BlogCategory cat = new BlogCategory
                    {
                        Blog = blog,
                        CategoryId = item,
                    };
                    categories.Add(cat);
                }

            }
        }
        return categories;
    }




    public async Task<List<BlogTag>> BlogTagsCreateAsync(int[] SelectedTagIds, Blog blog)
    {
        List<BlogTag> tags = new List<BlogTag>();
        List < BlogTag > exsistTags = await _context.BlogTag.ToListAsync();

        if (SelectedTagIds != null)
        {
            foreach (var item in SelectedTagIds)
            {
                if (exsistTags.FirstOrDefault(bc => bc.TagId == item&& bc.BlogId==blog.Id) == null)
                {
                    BlogTag tag = new BlogTag
                    {
                        TagId = item,
                        Blog = blog,
                    };
                    tags.Add(tag);
                }
            }
        }
        return tags;
    }




    public async Task<Image> ImageCreateAsync(IFormFile image, bool IsMain, bool IsVideo, Blog blog)
    {
        var path = Path.Combine(_environment.WebRootPath, "cilent", "assets", "blogs");
        var filename = await image.SaveToAsync(path);


        Image newImage = new Image
        {
            Url = filename,
            Blog = blog,
            IsMain = IsMain,
            IsVideo = IsVideo,
        };
        return newImage;
    }

}
