using Vanguard.Areas.Admin.Services.Interfaces;
using Vanguard.Data;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Services.Implementations;

public class SettingProductService: ISettingProductService
{
    readonly VanguardContext _context;

    public SettingProductService(VanguardContext context)
    {
        _context = context;
    }

    public SettingProduct GetOrCreateSettingProduct()
    {
        var settingProduct = _context.SettingProducts.FirstOrDefault();
        if (settingProduct == null)
        {
            settingProduct = new SettingProduct();
            _context.SettingProducts.Add(settingProduct);
            _context.SaveChanges();
        }
        return settingProduct;
    }

    public void UpdateSettingProduct(SettingProduct updatedModel)
    {
        var settingProduct = _context.SettingProducts.FirstOrDefault();
        if (settingProduct != null)
        {
            settingProduct.New = updatedModel.New;
            settingProduct.Best = updatedModel.Best;
            _context.SaveChanges();
        }
    }
}
