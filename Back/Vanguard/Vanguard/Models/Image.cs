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
    public int? HomeSliderId { get; set; }
    public int? HomeBannerId { get; set; }
    public int? ShopBannerId { get; set; }
    public int? AboutId { get; set; }
    public int? AboutEmploeesId { get; set; }

    public Product? Product { get; set; }
    public Color? Color { get; set; }
    public AppUser? AppUser { get; set; }
    public Blog? Blog { get; set; }
    public HomeSlider? HomeSlider { get; set; }
    public HomeBanner? HomeBanner { get; set; }
    public ShopBanner? ShopBanner { get; set; }
    public About? About { get; set; }
    public AboutEmploees? AboutEmploees { get; set; }
}
