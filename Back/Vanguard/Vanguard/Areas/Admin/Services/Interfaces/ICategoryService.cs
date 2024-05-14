using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vanguard.Areas.Admin.ViewModels.Category;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Services;

public interface ICategoryService
{
    public Task<List<Category>> GetAllAsync();
    public Task<SelectList> SelectCategoriesAsync();
    public Task<SelectList> SelectCategoriesAsync(int? id);
    public Task<Category?> CreateAsync(CategoryCreatVM? vm);
    public Task<Category?> GetByIdAsync(int? id);
    public Task<CategoryCreatVM?> EditViewAsync(int? id);
    public Task<IActionResult?> EditAsync(int? id, CategoryCreatVM vm);
    public Task<IActionResult?> SoftDeleteAsync(int? id);
    public Task<IActionResult?> HardDeleteAsync(int? id);
    public Task<List<Category>> DeletedListAllAsync();
    public Task<IActionResult?> RecoverAsync(int? id);

}
