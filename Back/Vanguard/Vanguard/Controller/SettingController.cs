using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using Vanguard.Data;
using Vanguard.Migrations;
using Vanguard.Models;
using Vanguard.Services.Interfaces;
using Vanguard.ViewModels.Basket;
using Vanguard.ViewModels.Setting;
using Vanguard.ViewModels.Shop;
using Vanguard.ViewModels.Wish;

namespace Vanguard.Controller
{
    [Route("setting")]
    public class SettingController : Microsoft.AspNetCore.Mvc.Controller
    {
        readonly VanguardContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        readonly IShopService _shopService;

        public SettingController(VanguardContext context, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IShopService shopService)
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _shopService = shopService;
        }

        [Route("Index/{id?}")]
        public async Task<IActionResult> Index(int? id)
        {
            SettingFilterVM vm = new SettingFilterVM();

            List<WishVM> wishesVM = new List<WishVM>();
            List<Wish> wishes = new List<Wish>();

            if (!User.Identity!.IsAuthenticated)
            {
                if (HttpContext.Request.Cookies["wish"] != null)
                    wishesVM = JsonConvert.DeserializeObject<List<WishVM>>(HttpContext.Request.Cookies["wish"]);
            }
            else
            {
                vm.IsAuthenticated = true;
                var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
                wishes = await _context.Wishs.Where(w => w.AppUserId == user!.Id).ToListAsync();
                wishesVM = wishes.Select(w => new WishVM { Id = w.ProductId }).ToList();

                //user
                vm.User.Id = user!.Id;
                //vm.

            }

            switch (id)
            {
                case 1:
                    vm.IsSetting = true;
                    return View(vm);
                case 2:
                    vm.IsOrder = true;
                    return View(vm);
                case 3:
                    vm.IsWish = true;
                    break;
            }

         

            List<int> wishProductIds = wishesVM.Select(w => w.Id).ToList();

            var query = _shopService.ProductgetQuery();

            var products = await query.Where(p => !p.IsDeleted && wishProductIds
                                         .Contains(p.Id))
                                         .ToListAsync();




            var shopProductVMs = products.Select(p => new ShopProductVM
            {
                Product = p,
                MainImageURL = p.Images.FirstOrDefault(i => i.IsMain)!.Url,
                HoverImageURL = p.Images.FirstOrDefault(i => i.IsHover)!.Url,
                AdditionalImagesURL = p.Images
                    .Where(i => !i.IsMain && !i.IsHover)
                    .Select(i => i.Url)
                    .ToList(),
                Color = p.ProductColors.Color.Name,
                Categories = p.ProductCategory.Select(pc => pc.Category.Name).ToList(),
                Tags = p.ProductTag.Select(pc => pc.Tag.Name).ToList(),
                Informations = p.Information.ToList()!,
                IsNew = p.CreatedDate >= (DateTime.UtcNow.AddDays(-5).AddHours(4)),
                IsDiscounted = p.DiscountPrice > 0,
                IsBest = p.Information.Sum(p => p.OrderCount) >= 50,
                Offer = p.DiscountPrice > 0 ? (int)(((p.SellPrice - p.DiscountPrice) / p.SellPrice) * 100) : 0
            }).ToList();

            vm.Products = shopProductVMs;

            return View(vm);
        }









        //Wish
        [Route("AddWish/{id?}")]
        public async Task<IActionResult> AddWish(int id)
        {
            var product = await _context.Products
                                  .Where(i => !i.IsDeleted && i.Id == id)
                                  .FirstOrDefaultAsync();
            List<WishVM> vms = new List<WishVM>();

            if (!User.Identity.IsAuthenticated)
            {
                var wishCookieValue = HttpContext.Request.Cookies["wish"];
                if (wishCookieValue != null)
                {
                    vms = JsonConvert.DeserializeObject<List<WishVM>>(wishCookieValue);
                }

                var itemToRemove = vms.SingleOrDefault(item => item.Id == id);

                if (itemToRemove == null)
                {
                    WishVM wishVM = new WishVM
                    {
                        Id = id
                    };
                    vms.Add(wishVM);
                }
                else
                {
                    vms.Remove(itemToRemove);
                }

                var updatedWishCookieValue = JsonConvert.SerializeObject(vms);
                HttpContext.Response.Cookies.Append("wish", updatedWishCookieValue, new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(30)
                });

                return Ok();
            }
            else
            {
                var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
                var checkWish = await _context.Wishs.FirstOrDefaultAsync(b => b.AppUserId == user!.Id && b.ProductId == product!.Id);
                if (checkWish != null)
                {
                    _context.Wishs.Remove(checkWish);
                }
                else
                {
                    Wish wish = new Wish
                    {
                        ProductId = product!.Id,
                        AppUser = user!,
                        AppUserId = user!.Id,
                    };
                    await _context.Wishs.AddAsync(wish);
                }
                await _context.SaveChangesAsync();
                return Ok();
            }
        }

    }
}
