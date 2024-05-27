using Vanguard.Models;
using Vanguard.ViewModels.Shop.AdditionVMs;

namespace Vanguard.ViewModels.Shop;

public class ShopVM
{
    public List<ShopProductVM> Product { get; set; } = null!;
    public PageInfo PageInfo { get; set; } = new PageInfo();
    public string? script { get; set; }
    public List<CategorySelecting>? CategorySelecting { get; set; }
    public List<SelectedCats>? SelectedCats { get; set; }
    public int[]? SelectedCategories { get; set; }
    public bool Grid { get; set; }=true;
}
