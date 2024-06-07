using Vanguard.Models;
using Vanguard.ViewModels.Shop;

namespace Vanguard.ViewModels.Home;

public class TrendyProductVM
{
    public List<ShopProductVM>? AllProduct { get; set; }
    public List<ShopProductVM>? NewProduct { get; set; }
    public List<ShopProductVM>? BestProduct { get; set; }
    public List<ShopProductVM>? DiscountedProduct { get; set; }
}
