using Vanguard.Models;

namespace Vanguard.ViewModels.Shop;

public class ShopProductVM
{
    public Product Product { get; set; } = null!;
    public string MainImageURL { get; set; } = null!;
    public string HoverImageURL { get; set; } = null!;
    public List<string>? AdditionalImagesURL { get; set; }
    public string Color { get; set; } = null!;
    public List<string> Categories { get; set; } = null!;
    public List<string> Tags { get; set; } = null!;
    public List<Information> Informations { get; set; } = null!;
    public bool IsNew { get; set; }
    public bool IsDiscounted { get; set; }
    public bool IsBest { get; set; }
    public int? Offer { get; set; }
}
