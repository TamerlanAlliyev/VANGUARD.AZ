namespace Vanguard.ViewModels.Shop;

public class PageInfo
{
    public int TotalItems { get; set; }
    public int ItemsPerPage { get; set; } = 2;
    public int CurrentPage { get; set; } = 1;
    public int TotalPages => (int)System.Math.Ceiling((decimal)TotalItems / ItemsPerPage);
}