using Vanguard.Helpers;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Services.Interfaces;

public interface IHomeSliderService
{
    public Task<ServiceResult> SliderImageCreateAsync(HomeSlider homeSlider,string url);
    public  Task<HomeSlider> SliderEditAsync(HomeSlider slider);

    //public Task<ServiceResult> SliderEditAsync(HomeSlider slider);
    public Task<ServiceResult> ImageDeleteAsync(string url);
    //public  Task<ServiceResult> SliderDeleteAsync(int id);

}
