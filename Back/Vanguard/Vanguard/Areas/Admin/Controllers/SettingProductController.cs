using Microsoft.AspNetCore.Mvc;
using Vanguard.Areas.Admin.Services.Implementations;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.Controllers;

public class SettingProductController : Microsoft.AspNetCore.Mvc.Controller
{

    private readonly SettingProductService _settingProductService;

    public SettingProductController(SettingProductService settingProductService)
    {
        _settingProductService = settingProductService;
    }

    public IActionResult Index()
    {
        var model = _settingProductService.GetOrCreateSettingProduct();
        return View(model);
    }

    [HttpPost]
    public IActionResult Update(SettingProduct model)
    {
        if (ModelState.IsValid)
        {
            _settingProductService.UpdateSettingProduct(model);
            return RedirectToAction("Index");
        }

        return View("Index", model);
    }
}
