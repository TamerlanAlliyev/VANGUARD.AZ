using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Vanguard.Areas.Admin.ViewModels.CategoryViewModels;
using Vanguard.Areas.Admin.ViewModels.ProductViewModels.Additional;
using Vanguard.Areas.Admin.ViewModels.TagViewModels;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.ViewModels.ProductViewModels;

public class ProductCreateVM
{
    public string Name { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string? Description { get; set; }
    public decimal SellPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
    public List<Color>? Colors { get; set; }

    public List<TagSelectionVM>? TagSelections { get; set; }
    public int[]? SelectedTagIds { get; set; } = null!;

    public List<CategorySelectionVM>? CategorySelection { get; set; }
    public int[] SelectedCategoryIds { get; set; } = null!;

    public List<ColorSizeVM> ColorSizeVM { get; set; } = null!;


}
