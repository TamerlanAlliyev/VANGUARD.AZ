using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class BlogComment: BaseAuditable
{
    public string Comment { get; set; } = null!;
    public string AppUserId { get; set; } = null!;
    public int BlogId { get; set; }
    public BlogComment? Reply { get; set; }
    public int? ReplyId { get; set; }
    public List<BlogComment>? AnswerComments { get; set; }
    public AppUser AppUser { get; set; } = null!;
    public Blog Blog { get; set; } = null!;
}
