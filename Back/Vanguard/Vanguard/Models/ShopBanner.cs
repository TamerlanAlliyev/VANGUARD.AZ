using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class ShopBanner: BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ImageId { get; set; }
    public Image Image { get; set; } = null!;
}
