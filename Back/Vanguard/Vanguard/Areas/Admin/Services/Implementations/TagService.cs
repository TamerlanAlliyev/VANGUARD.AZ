using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.Services;
using Vanguard.Areas.Admin.ViewModels.TagViewModels;
using Vanguard.Data;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Services;

public class TagService:ITagService
{
    readonly VanguardContext _context;

    public TagService(VanguardContext context)
    {
        _context = context;
    }

    public async Task<List<Tag>> GetAllAsync()
    {
        var Tags = await _context.Tags.Where(t => !t.IsDeleted).OrderByDescending(t => t.Id).ToListAsync();
        return Tags;
    }

    public async Task<IActionResult?> CreateAsync(TagCreateVM? vm)
    {
        if (vm==null)  return null;

        Tag tag = new Tag
        {
            Name = vm.Name,
        };

        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();

        return new OkResult();
    }





    public async Task<Tag?> GetByIdAsync(int? id)
    {
        if (id == null || id < 1) return null;
        
        var Tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

        if(Tag == null) return null;
        return Tag;
    }




    public async Task<TagCreateVM?> EditViewAsync(int? id)
    {
        var oldTag = await GetByIdAsync(id);

        if (oldTag==null) return null;

        TagCreateVM tag = new TagCreateVM
        {
            Name = oldTag.Name,
        };

        return tag;
    }


    public async Task<IActionResult?> EditAsync(int? id, TagCreateVM vm)
    {
        if (id!=vm.Id) return new BadRequestObjectResult("Invalid request. Ids do not match.");
        var oldTag = await GetByIdAsync(id);

        if (oldTag == null) return null;

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
        if (tag == null)  return null;

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
