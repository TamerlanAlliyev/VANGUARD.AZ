namespace Vanguard.Areas.Admin.ViewModels.Category;

public class CategoryCreatVM
{
    public int Id { get; set; } 
    public string Name { get; set; } = null!;
    public int? ParentCategoryId { get; set; }
}
