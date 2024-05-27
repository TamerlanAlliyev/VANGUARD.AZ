using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class Information:BaseAuditable
{

    public decimal? Weight { get; set; }
    public string? Dimensions { get; set; }
    public int Count { get; set; } = default;


    public int ProductId { get; set; }
    public int SizeId { get; set; }
    public int ColorId { get; set; }
    public Product Product { get; set; } = null!;
    public Size Size { get; set; } = null!;
    public Color Color { get; set; } = null!;
}
