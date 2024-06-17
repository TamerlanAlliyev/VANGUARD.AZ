namespace Vanguard.ViewModels.Blog;

public class BlogAllVM
{
    public int Id { get; set; }
    public int Clicked { get; set; }
    public int Comment { get; set; }
    public string Title { get; set; } = null!;
    public string? Descripton { get; set; } 
    public string Image { get; set; } = null!;
    public bool IsVideo { get; set; }
    public string Author { get; set; } = null!;
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; } = null!;

}
