namespace Vanguard.Areas.Admin.ViewModels.Blog;

public class BlogVM
{
    public int Id { get; set; }
    public int Clicked { get; set; }
    public string Title { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string User { get; set; } = null!;
    public DateTime CreateDate { get; set; } 
    public string UserPosition { get; set; } = null!;
    public bool IsViedo { get; set; }
}
