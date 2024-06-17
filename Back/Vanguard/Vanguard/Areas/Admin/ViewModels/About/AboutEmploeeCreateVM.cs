using System.ComponentModel.DataAnnotations.Schema;

namespace Vanguard.Areas.Admin.ViewModels.About;

public class AboutEmploeeCreateVM
{
    public int Id { get; set; }
    public string Fullname { get; set; } = null!;
    public string Position { get; set; }= null!;
    public string? Image { get; set; } 
    [NotMapped]
    public IFormFile ImageFile { get; set; } = null!;
}
