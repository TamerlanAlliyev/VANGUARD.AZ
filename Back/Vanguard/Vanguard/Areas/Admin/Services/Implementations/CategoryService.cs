using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Vanguard.Areas.Admin.ViewModels.Category;
using Vanguard.Data;
using Vanguard.Exceptions;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Services;

public class CategoryService : ICategoryService
{
    readonly VanguardContext _context;
    readonly IHttpContextAccessor _httpContextAccessor;

    public CategoryService(VanguardContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        var categories = await _context.Categories.Where(c => !c.IsDeleted).OrderByDescending(t => t.Id).Include(c => c.ProductCategory).ToListAsync();
        return categories;
    }

    public async Task<Category?> GetByIdAsync(int? id)
    {
        if (id < 1 || id == null) return null;
        var category = await _context.Categories
                            .Where(c => c.Id == id && !c.IsDeleted)
                            .Include(c => c.ProductCategory)
                            .Include(c => c.ParentCategory)
                            .FirstOrDefaultAsync();
        return category;
    }

    public async Task<Category?> GetByIdDeletedAsync(int? id)
    {
        if (id < 1 || id == null) return null;
        var category = await _context.Categories.Where(c => c.Id == id && c.IsDeleted).FirstOrDefaultAsync();
        return category;
    }

    public async Task<Category?> GetByNameAsync(string? name)
    {
        if (name == null) return null;
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        if (category == null) return null;

        return category;
    }

    public async Task<SelectList> SelectCategoriesAsync()
    {
        var categories = await _context.Categories.Where(c => !c.IsDeleted && c.ParentCategoryId == null).ToListAsync();
        return new SelectList(categories, "Id", "Name");
    }

    public async Task<SelectList> SelectCategoriesAsync(int? id)
    {
        var categories = await _context.Categories.Where(c => !c.IsDeleted && c.ParentCategoryId == null && c.Id != id).ToListAsync();
        return new SelectList(categories, "Id", "Name");
    }




    public async Task<Category?> CreateAsync(CategoryCreatVM? vm)
    {
        if (vm == null) return null;

        bool created = await CategoryCheckAsync(vm);

        if (created)
        {
            if (_httpContextAccessor?.HttpContext != null)
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = 422;
            }

            throw new UnprocessableEntityException("Category already exists.");
        }



        var category = new Category
        {
            Name = vm.Name,
        };

        if (vm.ParentCategoryId != null) category.ParentCategoryId = vm.ParentCategoryId;

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<bool> CategoryCheckAsync(CategoryCreatVM vm)
    {
        var category = await GetByNameAsync(vm.Name);
        if (category !=null) return true;
        return false;
    }



    public async Task<CategoryCreatVM?> EditViewAsync(int? id)
    {
        var category = await GetByIdAsync(id);
        if (category == null) return null;



        var vm = new CategoryCreatVM
        {
            Name = category.Name,
            Id = category.Id,
            ParentCategoryId = category.ParentCategoryId,
        };

        return vm;
    }


    public async Task<IActionResult?> EditAsync(int? id, CategoryCreatVM vm)
    {

        var category = await GetByIdAsync(id);
        if (category == null) return null;

        if (category.Name != vm.Name)
        {
            bool created = await CategoryCheckAsync(vm);

            if (created)
            {
                if (_httpContextAccessor?.HttpContext != null)
                {
                    _httpContextAccessor.HttpContext.Response.StatusCode = 422;
                }

                throw new UnprocessableEntityException("Category already exists.");
            }
        }

        category.Name = vm.Name;
        category.ParentCategoryId = vm.ParentCategoryId;

        await _context.SaveChangesAsync();

        return new OkResult();
    }

    public async Task<List<Category>> DeletedListAllAsync()
    {
        var category = await _context.Categories.Where(c => c.IsDeleted).ToListAsync();
        return category;
    }

    public async Task<IActionResult?> SoftDeleteAsync(int? id)
    {
        var category = await GetByIdAsync(id);
        if (category == null) return null;

        category.IsDeleted = true;
        await _context.SaveChangesAsync();

        return new OkResult();
    }


    public async Task<IActionResult?> HardDeleteAsync(int? id)
    {
        var category = await GetByIdDeletedAsync(id);
        if (category == null) return null;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return new OkResult();
    }


    public async Task<IActionResult?> RecoverAsync(int? id)
    {
        if (id == null || id < 0) return null;
        var category = await _context.Categories.FirstOrDefaultAsync(t => t.Id == id && t.IsDeleted);
        if (category == null) return null;
        category.IsDeleted = false;
        await _context.SaveChangesAsync();

        return new OkResult();
    }

}
