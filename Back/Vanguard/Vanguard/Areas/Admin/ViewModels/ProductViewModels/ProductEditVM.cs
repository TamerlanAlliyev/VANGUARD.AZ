using System.ComponentModel.DataAnnotations.Schema;
using Vanguard.Areas.Admin.ViewModels.CategoryViewModels;
using Vanguard.Areas.Admin.ViewModels.ProductViewModels.Edit;
using Vanguard.Areas.Admin.ViewModels.TagViewModels;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.ViewModels.ProductViewModels;

public class ProductEditVM
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string? Description { get; set; }
    public decimal SellPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
    public List<Color>? Colors { get; set; }

    public List<TagSelectionVM>? TagSelections { get; set; }
    public List<ProductTag>? SelectedTags { get; set; }
    public int[]? SelectedTagIds { get; set; } = null!;

    public List<CategorySelectionVM>? CategorySelection { get; set; }
    public List<ProductCategory>? SelectedCategories { get; set; }
    public int[]? SelectedCategoryIds { get; set; } 

    public ColorSizeVM ColorSizeVM { get; set; } = new ColorSizeVM();



    public List<SizeVM>? SizeVMs { get; set; }




    //[NotMapped]
    //public IFormFile MainImage { get; set; } = null!;
    //[NotMapped]
    //public IFormFile HoverImage { get; set; } = null!;
    //[NotMapped]
    //public List<IFormFile>? AdditionalImages { get; set; }

    //public string? MainImageUrl { get; set; }
    //public string? HoverImageUrl { get; set; }
    //public List<string>? AdditionalImageUrls { get; set; }
}
