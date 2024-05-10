using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class Product : BaseAuditable
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? Storage { get; set; }
    public decimal SellPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int? ClicketCount { get; set; }
    public List<Information>? Informations { get; set; }
    public List<ProductSize> ProductSizes { get; set; } = null!;
    public List<Image> Images { get; set; } = null!;
    public List<ProductColor> ProductColors { get; set; } = null!;
    public List<ProductCategory> ProductCategory { get; set; } = null!;
    public List<ProductTag> ProductTag { get; set; } = null!;
}
