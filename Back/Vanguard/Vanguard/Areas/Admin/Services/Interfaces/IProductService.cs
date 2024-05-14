using Vanguard.Areas.Admin.ViewModels.CategoryViewModels;
using Vanguard.Areas.Admin.ViewModels.TagViewModels;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Services.Interfaces;

public interface IProductService
{
    public Task<string> RootImageCreate(IFormFile imageFile);
    public Image ImageCreate(string ImageName, Product Product, bool IsMain, bool IsHover);
    public Task<List<TagSelectionVM>> TagSelectionsAsync();
    public  Task<List<CategorySelectionVM>> CategorySelectionsAsync();
}
