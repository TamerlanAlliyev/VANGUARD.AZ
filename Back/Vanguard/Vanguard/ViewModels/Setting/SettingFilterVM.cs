using Vanguard.Models;
using Vanguard.ViewModels.Shop;

namespace Vanguard.ViewModels.Setting;

public class SettingFilterVM
{
    public bool IsSetting { get; set; }
    public bool IsOrder { get; set; }
    public bool IsWish { get; set; }
    public bool IsAuthenticated { get; set; }
    public List<ShopProductVM> Products { get; set; } = new List<ShopProductVM>();
    public UserInfoUpdate? User { get; set; }
}
