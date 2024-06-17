namespace Vanguard.Models;

public class AboutEmploees
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Position { get; set; } = null!;
    public int? ImageId { get; set; } 
    public Image? Image { get; set; }

}
