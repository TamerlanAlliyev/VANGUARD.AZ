using System.ComponentModel.DataAnnotations.Schema;
using Vanguard.Areas.Admin.ViewModels.CategoryViewModels;
using Vanguard.Areas.Admin.ViewModels.TagViewModels;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.ViewModels.ProductViewModels;

public class ProductCreateVM
{
    public string Name { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string? Description { get; set; }
    public decimal SellPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
    public List<Tag>? Tags { get; set; }

    [NotMapped]
    public IFormFile MainImage { get; set; } = null!;
    [NotMapped]
    public IFormFile HoverImage { get; set; } = null!;
    [NotMapped]
    public List<IFormFile>? AdditionalImages { get; set; }


    public List<TagSelectionVM>? TagSelections { get; set; }
    public int[]? SelectedTagIds { get; set; }

    public List<CategorySelectionVM>? CategorySelection {  get; set; }
    public int[]? SelectedCategoryIds { get; set; }

}
