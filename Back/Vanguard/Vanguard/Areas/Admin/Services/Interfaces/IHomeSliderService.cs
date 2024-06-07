using Vanguard.Helpers;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Services.Interfaces;

public interface IHomeSliderService
{
    public Task<ServiceResult> SliderImageCreateAsync(HomeSlider homeSlider);

}
