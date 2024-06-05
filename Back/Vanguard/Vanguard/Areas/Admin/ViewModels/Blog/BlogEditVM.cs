using System.ComponentModel.DataAnnotations.Schema;
using Vanguard.Areas.Admin.ViewModels.CategoryViewModels;
using Vanguard.Areas.Admin.ViewModels.TagViewModels;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.ViewModels.Blog;

public class BlogEditVM
{
    public int BlogId { get; set; }
    public string Title { get; set; } = null!;
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;

    public string MainDescription { get; set; } = null!;
    public string? AddinationDescription { get; set; }


    public List<CategorySelectionVM>? CategorySelection { get; set; }
    public List<Vanguard.Models.Category>? BlogCategories { get; set; }
    public int[]? SelectedCategoryIds { get; set; }

    public List<TagSelectionVM>? TagSelections { get; set; }
    public List<Vanguard.Models.Tag>? BlogTags { get; set; }
    public int[]? SelectedTagIds { get; set; } = null!;


    public List<ImageVM> Images { get; set; } = null!;

    [NotMapped]
    public IFormFile? MainFile { get; set; }
    [NotMapped]

    public List<IFormFile>? AddinationFile { get; set; }
}
