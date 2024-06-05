using Vanguard.Models;
using Vanguard.ViewModels.Shop;

namespace Vanguard.ViewModels.Blog;

public class BlogViewVM
{
    public List<BlogAllVM> Blogs { get; set; } = new List<BlogAllVM>();
    public List<Category> Categories { get; set; } = new List<Category>();
    public int SelectedCategory { get; set; }
    public PageInfo? PageInfo { get; set; }
}
