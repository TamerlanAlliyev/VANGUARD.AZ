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

        public async Task<IActionResult> AddCart(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                var product = await _context.Products
                    .Where(p => !p.IsDeleted)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    return NotFound();
                }

                var basket = HttpContext.Request.Cookies["basket"];
                List<BasketVM> basketItems;

                if (basket == null)
                {
                    basketItems = new List<BasketVM>();
                }
                else
                {
                    basketItems = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                }

                var item = basketItems.SingleOrDefault(i => i.Id == id);
                if (item == null)
                {
                    item = new BasketVM
                    {
                        Id = id,
                        Count = 1
                    };
                    basketItems.Add(item);
                }
                else
                {
                    item.Count++;
                }

                HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketItems));
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var existingBasket = await _context.Baskets
                    .FirstOrDefaultAsync(b => b.ProductId == id);

                if (existingBasket != null)
                {
                    existingBasket.Count++;
                    existingBasket.UserId = userId;
                }
                else
                {
                    var newBasket = new Basket
                    {
                        Count = 1,
                        ProductId = id,
                        UserId = userId
                    };

                    _context.Baskets.Add(newBasket);
                }

                await _context.SaveChangesAsync();
            }

            return Ok();
        }


        public IActionResult Forbidden()
        {
            return View("Error403");
        }
    }
}

