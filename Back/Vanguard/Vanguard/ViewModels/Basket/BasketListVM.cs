namespace Vanguard.ViewModels.Basket;

public class BasketListVM
{
    public List<BasketItemVM> BasketItems { get; set; } = new List<BasketItemVM>();
    public int TotalCount { get; set; }
    public decimal TotalSellPrice { get; set; }
    public decimal TotalDiscountPrice { get; set; }
}
