using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class Image : BaseAuditable
{
    public string Url { get; set; } = null!;
    public bool IsMain { get; set; }
    public bool IsHover { get; set; }
    public int? ProductId { get; set; }
    public int? ColorId { get; set; }
    public int? AppUserId { get; set; }
    public Product? Product { get; set; }
    public Color? Color { get; set; }
    public AppUser? AppUser { get; set; }
}
