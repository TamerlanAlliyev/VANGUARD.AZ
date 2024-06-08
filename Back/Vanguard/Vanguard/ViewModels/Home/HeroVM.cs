using Vanguard.Models;
using Vanguard.ViewModels.Shop;

namespace Vanguard.ViewModels.Home;

public class HeroVM
{
    public SettingHomeHero Hero { get; set; } = new SettingHomeHero();
    public List<ShopProductVM> HeroProducts { get; set; } = new List<ShopProductVM>();
    public string? formattedTime  { get; set; }

}
