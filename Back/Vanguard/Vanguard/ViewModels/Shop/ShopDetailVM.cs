using Vanguard.Models;
using Vanguard.ViewModels.Shop.AdditionVMs;

namespace Vanguard.ViewModels.Shop;

public class ShopDetailVM
{
    public Product Product { get; set; }=null!;
    public string MainImageURL { get; set; } = null!;
    public string HoverImageURL { get; set; } = null!;
    public List<string>? AdditionalImagesURL { get; set; }
    public List<ProductColorVM>? Colors { get; set; }
    public List<Information> Information { get; set; } = null!;
    public List<Product>? RelatedProducts { get; set; }
}
