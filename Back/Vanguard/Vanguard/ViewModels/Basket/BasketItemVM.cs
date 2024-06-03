using Vanguard.Models;

namespace Vanguard.ViewModels.Basket;

public class BasketItemVM
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string Color { get; set; } = null!;
    public string Size { get; set; } = null!;
    public decimal SellPrice { get; set; } 
    public decimal? DiscountPrice { get; set; }
    public int Quantity { get; set; }
    public int ItemCount { get; set; }
    public bool Status { get; set; }
}
