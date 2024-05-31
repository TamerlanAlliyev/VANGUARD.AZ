using System.Security.Policy;
using Vanguard.Models;
using Vanguard.ViewModels.Shop.AdditionVMs;

namespace Vanguard.ViewModels.Shop;

public class ShopVM
{
    public List<ShopProductVM> Product { get; set; } = null!;
    public PageInfo PageInfo { get; set; } = new PageInfo();
    public bool Grid { get; set; } = true;
    public int? MaxPrice { get; set; }
    public int? MinPrice { get; set; }
    public int? CurrrentMaxPrice { get; set; }
    public int? CurrrentMinPrice { get; set; }
    public int OrderBy { get; set; }

    //Category
    public List<ReadyCategory>? ReadyCategory { get; set; }
    public List<SelectedCats>? SelectedCats { get; set; }
    public int[]? SentCategories { get; set; }

    //Gender
    public List<ReadyGender>? ReadyGender { get; set; }
    public List<SelectedGenders>? SelectedGenders { get; set; }
    public int[]? SentGenders { get; set; }

    //Color
    public List<ReadyColors>? ReadyColors { get; set; }
    public List<SelectedColors>? SelectedColors { get; set; }
    public int[]? SentColors { get; set; }

    //Size
    public List<ReadySizes>? ReadySizes { get; set; }
    public List<SelectedSizes>? SelectedSizes { get; set; }
    public int[]? SentSizes { get; set; }

    //Tag
    //public List<ReadyTags>? ReadyTags { get; set; }
    public List<Tag>? SelectedTags { get; set; }
    public int[]? SentTag { get; set; }



    public List<Tag>? Tags { get; set; }
}
