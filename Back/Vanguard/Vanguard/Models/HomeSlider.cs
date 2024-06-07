using System.ComponentModel.DataAnnotations.Schema;
using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class HomeSlider:BaseAuditable
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int? ImageId { get; set; }
    public Image Image { get; set; } = null!;
    public int TagId { get; set; }
    public Tag Tag { get; set; } = null!;

    [NotMapped]
    public IFormFile ImageFile { get; set; } = null!;
}
