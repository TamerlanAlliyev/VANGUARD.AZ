using Vanguard.Areas.Admin.ViewModels.CategoryViewModels;
using Vanguard.Areas.Admin.ViewModels.ProductViewModels;
using Vanguard.Areas.Admin.ViewModels.ProductViewModels.Edit;
using Vanguard.Areas.Admin.ViewModels.TagViewModels;
using Vanguard.Models;
using Vanguard.Helpers;

namespace Vanguard.Areas.Admin.Services.Interfaces;

public interface IProductService
{
    public Task<string> RootImageCreate(IFormFile imageFile);
    public Image ImageCreate(string ImageName, Product Product, bool IsMain, bool IsHover, Color color);
    public Task<List<TagSelectionVM>> TagSelectionsAsync();
    public  Task<List<CategorySelectionVM>> CategorySelectionsAsync();
    public  Task<List<CategorySelectionVM>> SelectedCategoryAsync(int id);
    public  Task CategoryDeleteAsync(int productId, int catId);
    public  Task<ServiceResult> ImageDeleteAsync(string url);


    public Task<List<TagSelectionVM>> SelectedTagAsync(int id);
    public  Task<List<SizeVM>> SelectedSizesAsync(int id);
    public  Task<ImageVM> SelectedSizesAsync(int product, int color);
    public  Task TagDeleteAsync(int productId, int tagId);

    public  Task<ServiceResult> CreateAsync(ProductCreateVM vm);

    public Task<ProductEditVM> EditViewModelAsync(int? id);
    public Task<ServiceResult> EditAsync(ProductEditVM vm);

    public Task<ProductDetailVM> DetailAsync(int? id);




    public Task<List<ProductCategory>> ProductCategoriesCreateAsync(int productId, int[] catIds);
    public  Task<List<ProductTag>> ProductTagCreateAsync(int productId, int[] tagIds);
    public  Task<ServiceResult> HardDeleteAsync(int id);
    public  Task<ServiceResult> InformationDeleteAsync(int? id);

}
