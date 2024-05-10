using Microsoft.AspNetCore.Mvc;

namespace Vanguard.Controllers 
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

