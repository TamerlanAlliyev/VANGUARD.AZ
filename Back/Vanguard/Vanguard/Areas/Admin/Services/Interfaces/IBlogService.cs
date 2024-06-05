using Vanguard.Helpers;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Services.Interfaces;

public interface IBlogService
{
    public Task<ServiceResult> ImageDeleteAsync(int id);
    public  Task TagDeleteAsync(int blogId, int tagId);
    public  Task CategoryDeleteAsync(int blogId, int catId);
    public Task<List<BlogCategory>> BlogCategoriesCreateAsync(int[] SelectedCategoryIds, Blog blog);
    public Task<List<BlogTag>> BlogTagsCreateAsync(int[] SelectedTagIds, Blog blog);
    public  Task<Image> ImageCreateAsync(IFormFile image, bool IsMain, bool IsVideo, Blog blog);

}
