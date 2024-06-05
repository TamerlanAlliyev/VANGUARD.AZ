namespace Vanguard.Models;

public class BlogTag
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public int TagId { get; set; }
    public Blog Blog { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}
