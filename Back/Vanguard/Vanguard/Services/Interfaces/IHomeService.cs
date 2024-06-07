using Vanguard.ViewModels.Home;

namespace Vanguard.Services.Interfaces;

public interface IHomeService
{
    public  Task<TrendyProductVM> TrendySelectedAsync();

}
