using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class Color:BaseAuditable
{
    public string Name { get; set; } = null!;
    public string HexCode { get; set; } = null!;
    public int ProductId { get; set; }
    public List<ProductColor> ProductColors { get; set; } = null!;
}
