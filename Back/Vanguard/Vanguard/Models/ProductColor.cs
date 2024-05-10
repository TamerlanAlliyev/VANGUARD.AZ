using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class ProductColor:BaseEntity
{
    public int ProductId { get; set; }
    public int ColorId { get; set; }
    public Product Product { get; set; } = null!;
    public Color Color { get; set; } = null!;
}
