namespace Vanguard.Areas.Admin.ViewModels.Blog;

public class ImageVM
{
    public int Id { get; set; }
    public string Url { get; set; } = null!;
    public bool IsVideo { get; set; }
    public bool IsMain { get; set; }
}
