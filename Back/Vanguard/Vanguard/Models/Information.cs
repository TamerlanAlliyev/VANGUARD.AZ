using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class Information : BaseAuditable
{
    public string? Weight { get; set; }
    public string? Dimensions { get; set; }
    public int ProductId { get; set; }
    public int SizeId { get; set; }
    public Product Product { get; set; } = null!;
    public Size Size { get; set; } = null!;
}
