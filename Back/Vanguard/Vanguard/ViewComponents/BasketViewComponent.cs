using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.ContentModel;
using System.Security.Claims;
using Vanguard.Data;
using Vanguard.Migrations;
using Vanguard.Models;
using Vanguard.ViewModels.Basket;
using Vanguard.ViewModels.Wish;


namespace Vanguard.ViewComponents;
[ViewComponent]
public class BasketViewComponent : ViewComponent
{
    readonly VanguardContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;

    public BasketViewComponent(VanguardContext context, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
    {
        _context = context;
        _userManager = userManager;
        _contextAccessor = contextAccessor;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        BasketListVM ListVM = new BasketListVM();
        List<BasketItemVM> vm = new List<BasketItemVM>();

        List<BasketVM> bskList = new List<BasketVM>();
        List<WishVM> wishesVM = new List<WishVM>();
        List<Wish> wishesCount = new List<Wish>();
        if (!User.Identity!.IsAuthenticated)
        {
            if (HttpContext.Request.Cookies["basket"] != null)
            {
                bskList = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]);

                var products = _context.Informations
                               .Include(p => p.Product)
                               .ThenInclude(p => p.Images)
                               .Include(p => p.Color)
                               .Include(p => p.Size)
                               .AsQueryable();


                if (bskList != null)
                {
                    foreach (var bsk in bskList)
                    {
                        var prod = await products.FirstOrDefaultAsync(p => p.Id == bsk.Id);

                        BasketItemVM item = new BasketItemVM
                        {
                            Id = bsk.Id,
                            Quantity = bsk.Count,
                            ProductId = prod!.Product.Id,
                            ItemCount = prod.Count,
                            Image = prod.Product.Images.FirstOrDefault(p => !p.IsDeleted && p.IsMain)!.Url,
                            Status = prod.Count > 0 ? true : false,
                            Name = prod.Product.Name,
                            Color = prod.Color.Name,
                            Size = prod.Size.Name,
                            SellPrice = prod.Product.SellPrice,
                            DiscountPrice = prod.Product.DiscountPrice,
                        };
                        ListVM.BasketItems.Add(item);

                        ListVM!.TotalCount = ListVM.TotalCount + item.Quantity;
                        ListVM.TotalSellPrice = ListVM.TotalSellPrice + (item.SellPrice * item.Quantity);
                        ListVM.TotalDiscountPrice = ListVM.TotalDiscountPrice + (decimal)(item.DiscountPrice != null ? (item.DiscountPrice * item.Quantity) : (item.SellPrice * item.Quantity));

                        TempData["BasketItemCount"] = ListVM.TotalCount + item.Quantity;

                    }

                }
                TempData["WishItemCount"] = wishesCount.Count();


            }

            if (HttpContext.Request.Cookies["wish"] != null)
            {
                wishesVM = JsonConvert.DeserializeObject<List<WishVM>>(_contextAccessor.HttpContext.Request.Cookies["wish"]);
                TempData["WishItemCount"] = wishesVM.Count().ToString();
            }
        }
        else
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var product = await _context.Baskets
                 .Where(b => b.AppUserId == user!.Id)
                      .Include(i => i.Information)
                      .Include(i => i.Information.Size)
                      .Include(i => i.Information.Color)
                      .Include(p => p.Information.Product)
                      .ThenInclude(p => p.Images)
                      .ToListAsync();

            var wishs = await _context.Wishs.Where(w => w.AppUserId == user!.Id).ToListAsync();

            wishesCount.AddRange(wishs);
            foreach (var prod in product)
            {
                BasketItemVM bsk = new BasketItemVM
                {
                    Id = prod.Id,
                    ProductId = prod.Information.Product.Id,
                    Quantity = prod.Quantity,
                    ItemCount = prod.Information.Count,
                    Image = prod.Information.Product.Images.FirstOrDefault(p => !p.IsDeleted && p.IsMain)!.Url,
                    Status = prod.Information.Count > 0 ? true : false,
                    Name = prod.Information.Product.Name,
                    Color = prod.Information.Color.Name,
                    Size = prod.Information.Size.Name,
                    SellPrice = prod.Information.Product.SellPrice,
                    DiscountPrice = prod.Information.Product.DiscountPrice,
                };
                ListVM.BasketItems.Add(bsk);

                ListVM!.TotalCount = ListVM.TotalCount + bsk.Quantity;
                ListVM.TotalSellPrice = ListVM.TotalSellPrice + (bsk.SellPrice * bsk.Quantity);
                ListVM.TotalDiscountPrice = ListVM.TotalDiscountPrice + (decimal)(bsk.DiscountPrice != null ? (bsk.DiscountPrice * bsk.Quantity) : (bsk.SellPrice * bsk.Quantity));
            }
        }
        TempData["BasketItemCount"] = ListVM.TotalCount;
        TempData["WishItemCount"] = wishesCount.Count().ToString();



        return View(ListVM);
    }

}


