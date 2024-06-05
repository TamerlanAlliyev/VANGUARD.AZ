namespace Vanguard.ViewModels.Blog;

public class BlogGetVM
{
    public int Id { get; set; }
    public int Clicked { get; set; }
    public string Title { get; set; } = null!;
    public string MainDescription { get; set; } = null!;
    public string? AddinationDescription { get; set; }
    public string Image { get; set; } = null!;
    public List<string>? AddunationImages { get; set; }
    public bool IsVideo { get; set; }
    public string Author { get; set; } = null!;
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; } = null!;

}
