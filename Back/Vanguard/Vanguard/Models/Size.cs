using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class Size : BaseAuditable
{
    public string Name { get; set; } = null!;
    public string? Weight { get; set; }
    public string? Dimensions { get; set; }
    public List<ProductSize> ProductSizes { get; set; } = null!;
}
