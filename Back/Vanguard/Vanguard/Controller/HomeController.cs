using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using Vanguard.Data;
using Vanguard.Models;
using Vanguard.ViewModels.Basket;

namespace Vanguard.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        readonly VanguardContext _context;
        readonly UserManager<AppUser> _userManager;

        public HomeController(VanguardContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

     

        public IActionResult Forbidden()
        {
            return View("Error403");
        }
    }
}

