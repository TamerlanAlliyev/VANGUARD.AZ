namespace Vanguard.ViewModels.Rating;

public class RatingVM
{
    public int ProductId { get; set; } 
    public int rating { get; set; } 
    public string? Comment { get; set; } 
    public bool Commented {  get; set; }
}
