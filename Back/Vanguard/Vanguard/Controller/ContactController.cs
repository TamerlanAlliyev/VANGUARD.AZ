using Microsoft.AspNetCore.Mvc;

namespace Vanguard.Controller
{
    public class ContactController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
