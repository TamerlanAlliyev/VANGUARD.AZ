using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class Image : BaseAuditable
{
    public string Url { get; set; } = null!;
    public bool IsMain { get; set; }
    public bool IsHover { get; set; }
    public bool IsVideo { get; set; }
    public int? ProductId { get; set; }
    public int? ColorId { get; set; }
    public int? BlogId { get; set; }
    public string? AppUserId { get; set; }
    public Product? Product { get; set; }
    public Color? Color { get; set; }
    public AppUser? AppUser { get; set; }
    public Blog? Blog { get; set; }
}
