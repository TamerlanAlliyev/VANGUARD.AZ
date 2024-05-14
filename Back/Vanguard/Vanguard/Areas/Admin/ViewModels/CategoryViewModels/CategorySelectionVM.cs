namespace Vanguard.Areas.Admin.ViewModels.CategoryViewModels;

public class CategorySelectionVM
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Parent { get; set; }
    public bool Checked { get; set; } = false;
}
