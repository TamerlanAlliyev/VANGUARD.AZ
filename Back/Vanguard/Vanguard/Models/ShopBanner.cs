using System.ComponentModel.DataAnnotations.Schema;
using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class ShopBanner: BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ImageId { get; set; }
    public Image Image { get; set; } = null!;
    [NotMapped]
    public IFormFile ImageFile { get; set; } = null!;
}
