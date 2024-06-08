using Vanguard.ViewModels.Home;

namespace Vanguard.Services.Interfaces;

public interface IHomeService
{
    public  Task<TrendyProductVM> TrendySelectedAsync();
    public  Task<HeroVM> HeroProductsAsync();
    //public  Task<List<Models.Blog>> LastBlogsAsync();

}
