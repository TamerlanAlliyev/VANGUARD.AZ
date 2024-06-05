using Microsoft.AspNetCore.Mvc;

namespace Vanguard.Controller
{
    public class AboutController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
