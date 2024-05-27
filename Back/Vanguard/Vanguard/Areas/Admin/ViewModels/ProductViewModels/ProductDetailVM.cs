using Vanguard.Models;

namespace Vanguard.Areas.Admin.ViewModels.ProductViewModels;

public class ProductDetailVM
{
    public Product? Product { get; set; } = null!;
    public int? Offer { get; set; }
    public string Color { get; set; } = null!;
    public string HexCode { get; set; } = null!;
    public string MainImageURL { get; set; } = null!;
    public string HoverImageURL { get; set; } = null!;
    public List<string> Categories { get; set; } = null!;
    public List<string>? Tags { get; set; }
    public List<string>? AdditionalImagesURL { get; set; }
    public List<Information> Informations { get; set; } = null!;
    public bool InStock { get; set; }
}
