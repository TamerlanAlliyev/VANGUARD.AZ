using Microsoft.AspNetCore.Mvc;

namespace Vanguard.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Microsoft.AspNetCore.Mvc.Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
