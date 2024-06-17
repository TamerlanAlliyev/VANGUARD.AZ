using Vanguard.Models;

namespace Vanguard.ViewModels.Blog;

public class BlogDetailVM
{
    public BlogGetVM Blog = new BlogGetVM();
    public List<Category> Categories { get; set; } = new List<Category>();
    public List<BlogAllVM> PopularBlogs { get; set; } = new List<BlogAllVM>();

    public int SelectedCategory { get; set; }
    public BlogComment BlogComment { get; set; } = new BlogComment();
    public List<Tag>? Tags { get; set; }
    public List<BlogComment> Comments { get; set; } = new List<BlogComment>();
}
