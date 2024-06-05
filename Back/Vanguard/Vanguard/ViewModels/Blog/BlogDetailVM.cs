using Vanguard.Models;

namespace Vanguard.ViewModels.Blog;

public class BlogDetailVM
{
    public Vanguard.Models.Blog Blog = new Vanguard.Models.Blog();
    public List<Category> Categories { get; set; } = new List<Category>();
    public int SelectedCategory { get; set; }
}
