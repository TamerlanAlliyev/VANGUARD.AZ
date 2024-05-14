using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.Services;
using Vanguard.Areas.Admin.ViewModels.TagViewModels;
using Vanguard.Data;
using Vanguard.Exceptions;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Services;

public class TagService : ITagService
{
    readonly VanguardContext _context;
    readonly IHttpContextAccessor _httpContextAccessor;

    public TagService(VanguardContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<Tag>> GetAllAsync()
    {
        var Tags = await _context.Tags.Where(t => !t.IsDeleted).OrderByDescending(t => t.Id).ToListAsync();
        return Tags;
    }

    public async Task<IActionResult?> CreateAsync(TagCreateVM? vm)
    {
        if (vm == null) return null;

        bool created = await TagNameCheckAsync(vm);

        if (created)
        {
            if (_httpContextAccessor?.HttpContext != null)
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = 422;
            }

            throw new UnprocessableEntityException("Tag already exists.");
        }

        Tag tag = new Tag
        {
            Name = vm.Name,
        };

        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();

        return new OkResult();
    }







    public async Task<bool> TagNameCheckAsync(TagCreateVM vm)
    {
        var category = await GetByNameAsync(vm.Name);
        if (category != null) return true;
        return false;
    }


    public async Task<Tag?> GetByNameAsync(string? name)
    {
        if (name == null) return null;
        var tag = await _context.Tags.FirstOrDefaultAsync(c => c.Name == name);
        if (tag == null) return null;

        return tag;
    }



    public async Task<Tag?> GetByIdAsync(int? id)
    {
        if (id == null || id < 1) return null;

        var Tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

        if (Tag == null) return null;
        return Tag;
    }




    public async Task<TagCreateVM?> EditViewAsync(int? id)
    {
        var oldTag = await GetByIdAsync(id);

        if (oldTag == null) return null;

        TagCreateVM tag = new TagCreateVM
        {
            Name = oldTag.Name,
        };

        return tag;
    }



    public async Task<IActionResult?> EditAsync(int? id, TagCreateVM vm)
    {
        if (id != vm.Id) return new BadRequestObjectResult("Invalid request. Ids do not match.");
        var oldTag = await GetByIdAsync(id);

        if (oldTag == null) return null;

        if(oldTag.Name != vm.Name){
            bool created = await TagNameCheckAsync(vm);

            if (created)
            {
                if (_httpContextAccessor?.HttpContext != null)
                {
                    _httpContextAccessor.HttpContext.Response.StatusCode = 422;
                }

                throw new UnprocessableEntityException("Tag already exists.");
            }
        }

        oldTag.Name = vm.Name;

        await _context.SaveChangesAsync();

        return new OkResult();
    }


    public async Task<List<Tag>> GetAllDeletedAsync()
    {
        var Tags = await _context.Tags.Where(t => t.IsDeleted).OrderByDescending(t => t.Id).ToListAsync();
        return Tags;
    }

    public async Task<IActionResult?> SoftDeleteAsync(int? id)
    {
        var tag = await GetByIdAsync(id);
        if (tag == null) return null;

        tag.IsDeleted = true;
        await _context.SaveChangesAsync();

        return new OkResult();
    }


    public async Task<IActionResult?> HardDeletedAsync(int? id)
    {
        if (id == null || id < 0) return null;

        var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id && t.IsDeleted);
        if (tag == null) return null;

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();

        return new OkResult();
    }


    public async Task<IActionResult?> RecoverAsync(int? id)
    {
        if (id == null || id < 0) return null;
        var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id && t.IsDeleted);
        if (tag == null) return null;
        tag.IsDeleted = false;
        await _context.SaveChangesAsync();


        return new OkResult();
    }




 



}
