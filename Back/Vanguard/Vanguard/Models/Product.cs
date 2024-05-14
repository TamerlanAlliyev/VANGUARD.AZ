using System.ComponentModel.DataAnnotations.Schema;
using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class Product : BaseAuditable
{
    public string Name { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string? Description { get; set; }
    public decimal SellPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int? ClicketCount { get; set; }
    public List<ProductSize> ProductSizes { get; set; } = null!;
    public List<Image> Images { get; set; } = null!;
    public List<ProductColor> ProductColors { get; set; } = null!;
    public List<ProductCategory> ProductCategory { get; set; } = null!;
    public List<ProductTag> ProductTag { get; set; } = null!;


    [NotMapped]
    public IFormFile MainImage { get; set; } = null!;
    [NotMapped]
    public IFormFile HoverImage { get; set; } = null!;
    [NotMapped]
    public List<IFormFile>? AdditionalImages { get; set; } 
}
