using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Vanguard.Areas.Admin.ViewModels.CategoryViewModels;
using Vanguard.Areas.Admin.ViewModels.TagViewModels;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.ViewModels.Blog;

public class BlogCreateVM
{
    public string Title { get; set; } = null!;
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
    [AllowHtml]
    public string MainDescription { get; set; } = null!;
    [AllowHtml]
    public string? AddinationDescription { get; set; }


    public List<CategorySelectionVM>? CategorySelection { get; set; }
    public int[]? SelectedCategoryIds { get; set; }

    public List<TagSelectionVM>? TagSelections { get; set; }
    public int[]? SelectedTagIds { get; set; } = null!;


    [NotMapped]
    public IFormFile MainFile { get; set; } = null!;
    [NotMapped]

    public List<IFormFile>? AddinationFile { get; set; }
}
