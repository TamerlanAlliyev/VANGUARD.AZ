using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class Category : BaseAuditable
{
    public string Name { get; set; } = null!;
    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public ICollection<Category>? ChildCategories { get; set; }
    public List<ProductCategory>? ProductCategory { get; set; }
}
