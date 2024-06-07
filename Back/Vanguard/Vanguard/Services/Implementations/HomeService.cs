using Vanguard.Models;
using Vanguard.ViewModels.Shop;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using Vanguard.Data;
using Vanguard.Services.Interfaces;
using Vanguard.ViewModels.Home;
using Vanguard.ViewModels.Wish;
using Microsoft.AspNetCore.Identity;
using Vanguard.Migrations;
using Microsoft.AspNetCore.Http;

namespace Vanguard.Services.Implementations
{
    public class HomeService : IHomeService
    {
        readonly VanguardContext _context;
        readonly UserManager<AppUser> _userManager;
        readonly IShopService _shopService;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext _httpContext;
        public HomeService(VanguardContext context, UserManager<AppUser> userManager, IShopService shopService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _shopService = shopService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<TrendyProductVM> TrendySelectedAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            List<WishVM> wishesVM = new List<WishVM>();
            List<Wish> wishes = new List<Wish>();

            if (!httpContext.User.Identity!.IsAuthenticated)
            {
                if (httpContext.Request.Cookies["wish"] != null)
                    wishesVM = JsonConvert.DeserializeObject<List<WishVM>>(httpContext.Request.Cookies["wish"]);
            }
            else
            {
                var user = await _userManager.GetUserAsync(httpContext.User);
                wishes = await _context.Wishs.Where(w => w.AppUserId == user!.Id).ToListAsync();
                wishesVM = wishes.Select(w => new WishVM { Id = w.ProductId }).ToList();
            }
            List<int> wishProductIds = wishesVM.Select(w => w.Id).ToList();

            var products = await _shopService.ProductgetQuery()
                .ToListAsync();

            var productsVM = products.OrderByDescending(p=>p.CreatedDate).Select(p => new ShopProductVM
            {
                Product = p,
                MainImageURL = p.Images.FirstOrDefault(i => i.IsMain)?.Url ?? string.Empty,
                HoverImageURL = p.Images.FirstOrDefault(i => i.IsHover)?.Url ?? string.Empty,
                AdditionalImagesURL = p.Images.Where(i => !i.IsMain && !i.IsHover).Select(i => i.Url).ToList(),
                Color = p.ProductColors.Color.Name,
                Categories = p.ProductCategory.Select(pc => pc.Category.Name).ToList(),
                Tags = p.ProductTag.Select(pc => pc.Tag.Name).ToList(),
                Informations = p.Information.ToList(),
                IsNew = p.CreatedDate >= (DateTime.UtcNow.AddDays(-15).AddHours(4)),
                IsDiscounted = p.DiscountPrice > 0,
                IsBest = p.Information.Sum(info => info.OrderCount) >= 50,
                Offer = p.DiscountPrice > 0 ? (int)(((p.SellPrice - p.DiscountPrice) / p.SellPrice) * 100) : 0,
                IsWish = wishProductIds.Contains(p.Id)
            }).ToList();

            TrendyProductVM trendy = new TrendyProductVM
            {
                AllProduct = productsVM.Take(18).ToList(),
                NewProduct = productsVM.Where(p => p.IsNew).Take(18).ToList(),
                BestProduct = productsVM.Where(p => p.IsBest).Take(18).ToList(),
                DiscountedProduct = productsVM.Where(p => p.IsDiscounted).Take(18).ToList()
            };

            return trendy;
        }
    }
}
