using Vanguard.Models;

namespace Vanguard.Areas.Admin.Services.Interfaces;

public interface ISettingProductService
{
    public SettingProduct GetOrCreateSettingProduct();
    public void UpdateSettingProduct(SettingProduct updatedModel);

}
