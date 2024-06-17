using System.ComponentModel.DataAnnotations.Schema;

namespace Vanguard.Models;

public class About
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    [ForeignKey(nameof(Image))]
    public int ImageId { get; set; }
    public Image? Image { get; set; }  
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
}
