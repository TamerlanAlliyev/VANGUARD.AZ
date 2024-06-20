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
using Microsoft.AspNetCore.Http;
using Vanguard.ViewModels.Blog;

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
            var settings = await _context.SettingProducts.ToListAsync();
            int New = settings[0].New;
            int Best = settings[0].Best;


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
                Id=p.Id,
                Product = p,
                MainImageURL = p.Images.FirstOrDefault(i => i.IsMain)?.Url ?? string.Empty,
                HoverImageURL = p.Images.FirstOrDefault(i => i.IsHover)?.Url ?? string.Empty,
                AdditionalImagesURL = p.Images.Where(i => !i.IsMain && !i.IsHover).Select(i => i.Url).ToList(),
                Color = p.ProductColors.Color.Name,
                Categories = p.ProductCategory.Select(pc => pc.Category.Name).ToList(),
                Tags = p.ProductTag.Select(pc => pc.Tag.Name).ToList(),
                Informations = p.Information.ToList(),
                IsNew = p.CreatedDate >= (DateTime.UtcNow.AddDays(-(New)).AddHours(4)),
                IsDiscounted = p.DiscountPrice > 0,
                IsBest = p.Information.Sum(info => info.OrderCount) >= Best,
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




        public async Task<HeroVM> HeroProductsAsync()
        {
            var hero = await _context.SettingHomeHero.FirstOrDefaultAsync();
            var settings = await _context.SettingProducts.ToListAsync();
            int New = settings[0].New;
            int Best = settings[0].Best;
            var httpContext = _httpContextAccessor.HttpContext;
            var wishesVM = new List<WishVM>();
            var wishes = new List<Wish>();

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

            var wishProductIds = wishesVM.Select(w => w.Id).ToList();

            var products = await _shopService.ProductgetQuery()
                .Where(p => p.ProductCategory.Any(pc => pc.CategoryId == hero.CategoryId) && p.ProductTag.Any(pt => pt.TagId == hero.TagId))
                .ToListAsync();

            if (products.Count()==0)
            {
                products = await _shopService.ProductgetQuery().ToListAsync();
            }

            if (hero.Offer > 0 && hero.Offer <= 100)
            {
                products = products.Where(p =>
                    p.DiscountPrice > 0 && 
                    p.SellPrice > 0 &&    
                    CalculateOfferPercentage(p.SellPrice, (decimal)p.DiscountPrice) >= hero.Offer
                ).ToList();
            }

            var productsVM = products.OrderByDescending(p => p.CreatedDate).Select(p => new ShopProductVM
            {
                Id=p.Id,
                Product = p,
                MainImageURL = p.Images.FirstOrDefault(i => i.IsMain)?.Url ?? string.Empty,
                HoverImageURL = p.Images.FirstOrDefault(i => i.IsHover)?.Url ?? string.Empty,
                AdditionalImagesURL = p.Images.Where(i => !i.IsMain && !i.IsHover).Select(i => i.Url).ToList(),
                Color = p.ProductColors.Color.Name,
                Categories = p.ProductCategory.Select(pc => pc.Category.Name).ToList(),
                Tags = p.ProductTag.Select(pc => pc.Tag.Name).ToList(),
                Informations = p.Information.ToList(),
                IsNew = p.CreatedDate >= (DateTime.UtcNow.AddDays(-(New)).AddHours(4)),
                IsDiscounted = p.DiscountPrice > 0,
                IsBest = p.Information.Sum(info => info.OrderCount) >= Best,
                Offer = p.DiscountPrice > 0 ? (int)(((p.SellPrice - p.DiscountPrice) / p.SellPrice) * 100) : 0,
                IsWish = wishProductIds.Contains(p.Id)
            }).Take(10).ToList();

            return new HeroVM
            {
                Hero = hero,
                HeroProducts = productsVM,
                formattedTime = hero.Time.HasValue ? hero.Time.Value.ToString("yyyy-MM-dd HH:mm:ss.fff") : "",
            };
        }

        private int CalculateOfferPercentage(decimal sellPrice, decimal discountPrice)
        {
            return (int)(((sellPrice - discountPrice) / sellPrice) * 100);
        }



        //public async Task<List<Models.Blog>> LastBlogsAsync()
        //{
        //    var lastBlogs = await _context.Blogs.Where(b => !b.IsDeleted)
        //                                .OrderByDescending(b => b.CreatedDate)
        //                                .Take(10)
        //                                .Include(b => b.Images)
        //                                .Include(b => b.AppUser)
        //                                .ThenInclude(b => b.AllowedEmployee!.Role)
        //                                .Include(b => b.Categories)
        //                                .ThenInclude(bc => bc.Category)
        //                                .Select(b => new BlogAllVM
        //                                {
        //                                    Id = b.Id,
        //                                    Title = b.Title,
        //                                    Clicked = b.Clickeds,
        //                                    Image = b.Images.FirstOrDefault(i => !i.IsDeleted && i.IsMain).Url,
        //                                    IsVideo = b.Images.FirstOrDefault(i => !i.IsDeleted && i.IsMain).IsVideo,
        //                                    Created = b.CreatedDate,
        //                                    CreatedBy = b.AppUser.FullName!,
        //                                    Descripton = b.MainDescription
        //                                }).ToListAsync();
        //    return lastBlogs;
        //}

    }
}
