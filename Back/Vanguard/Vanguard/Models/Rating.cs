namespace Vanguard.Models;

public class Rating
{
    public int Id { get; set; }
    public string? Comment { get; set; }
    public int UserRating {  get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public string AppUserId { get; set; }
    public AppUser AppUser {  get; set; } = null!;
}
