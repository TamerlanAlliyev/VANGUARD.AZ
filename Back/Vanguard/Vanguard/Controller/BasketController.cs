using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using Vanguard.Data;
using Vanguard.Migrations;
using Vanguard.Models;
using Vanguard.ViewModels.Basket;

namespace Vanguard.Controller;
public class BasketController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly VanguardContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;

    public BasketController(VanguardContext context, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
    {
        _context = context;
        _userManager = userManager;
        _contextAccessor = contextAccessor;
    }

    public async Task<IActionResult> Index()
    {


        BasketListVM ListVM = new BasketListVM();
        List<BasketItemVM> vm = new List<BasketItemVM>();

        List<BasketVM> bskList = new List<BasketVM>();
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

                        ViewData["BasketItemCount"] = ListVM.TotalCount + item.Quantity;
                    }

                }


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
        return View(ListVM);
    }






    public IActionResult Getbasket()
    {
        return ViewComponent("Basket");
    }



    public async Task<IActionResult> AddBasket(int id)
    {

        var product = await _context.Informations
                                   .Where(i => i.Count > 0 && !i.IsDeleted && i.Id == id)
                                   .FirstOrDefaultAsync();
        if (product == null)
        {
            return View();
        }
        if (!User.Identity!.IsAuthenticated)
        {
            var basket = HttpContext.Request.Cookies["basket"];
            List<BasketVM> basketItems = basket == null ? new List<BasketVM>() : JsonConvert.DeserializeObject<List<BasketVM>>(basket);

            basketItems ??= new List<BasketVM>();

            var item = basketItems.SingleOrDefault(i => i.Id == id);

            if (item == null)
            {
                item = new BasketVM
                {
                    Id = id,
                    Count = 1,
                };
                basketItems.Add(item);
            }
            else
            {
                item.Count++;
            }

            HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketItems));
            return Ok();
        }
        else
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var checkBasket = await _context.Baskets.FirstOrDefaultAsync(b => b.AppUserId == user!.Id && b.InformationId == product.Id);
            if (checkBasket != null)
            {
                checkBasket.Quantity++;
                await _context.SaveChangesAsync();
            }
            else
            {
                Basket basket = new Basket
                {
                    AppUser = user!,
                    AppUserId = user!.Id,
                    InformationId = id,
                    Quantity = 1,
                };

                await _context.Baskets.AddAsync(basket);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }


    }

    public async Task<IActionResult> IncreaseBasket(int id)
    {

        if (!User.Identity!.IsAuthenticated)
        {
            var basket = HttpContext.Request.Cookies["basket"];
            List<BasketVM> basketItems = basket == null ? new List<BasketVM>() : JsonConvert.DeserializeObject<List<BasketVM>>(basket);

            basketItems ??= new List<BasketVM>();

            var item = basketItems.SingleOrDefault(i => i.Id == id);

            item.Count++;

            HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketItems));
            return Ok();
        }
        else
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var checkBasket = await _context.Baskets.FirstOrDefaultAsync(b => b.AppUserId == user!.Id && b.Id == id);

            checkBasket.Quantity++;
            await _context.SaveChangesAsync();

            return Ok();
        }

    }


    public async Task<IActionResult> DecreaseBasket(int id)
    {

        if (!User.Identity!.IsAuthenticated)
        {
            var basket = HttpContext.Request.Cookies["basket"];
            List<BasketVM> basketItems = basket == null ? new List<BasketVM>() : JsonConvert.DeserializeObject<List<BasketVM>>(basket);

            basketItems ??= new List<BasketVM>();

            var item = basketItems.SingleOrDefault(i => i.Id == id);

            item.Count--;

            HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketItems));
            return Ok();
        }
        else
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var checkBasket = await _context.Baskets.FirstOrDefaultAsync(b => b.AppUserId == user!.Id && b.Id == id);

            checkBasket.Quantity--;
            await _context.SaveChangesAsync();

            return Ok();
        }

    }

    public async Task<IActionResult> ChangeBasket(int id, int count)
    {

        if (!User.Identity!.IsAuthenticated)
        {
            var basket = HttpContext.Request.Cookies["basket"];
            List<BasketVM> basketItems = basket == null ? new List<BasketVM>() : JsonConvert.DeserializeObject<List<BasketVM>>(basket);

            basketItems ??= new List<BasketVM>();

            var item = basketItems.SingleOrDefault(i => i.Id == id);

            item.Count = count;

            HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketItems));
            return Ok();
        }
        else
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var checkBasket = await _context.Baskets.FirstOrDefaultAsync(b => b.AppUserId == user!.Id && b.Id == id);

            checkBasket.Quantity = count;
            await _context.SaveChangesAsync();

            return Ok();
        }

    }


    public async Task<IActionResult> DeleteBasket(int id)
    {

        if (!User.Identity!.IsAuthenticated)
        {
            var basket = HttpContext.Request.Cookies["basket"];
            List<BasketVM> basketItems = basket == null ? new List<BasketVM>() : JsonConvert.DeserializeObject<List<BasketVM>>(basket);

            basketItems ??= new List<BasketVM>();

            var itemToRemove = basketItems.SingleOrDefault(i => i.Id == id);
            if (itemToRemove != null)
            {
                basketItems.Remove(itemToRemove);
                HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketItems));
            }
            return Ok();
        }
        else
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var checkBasket = await _context.Baskets.FirstOrDefaultAsync(b => b.AppUserId == user!.Id && b.Id == id);

            _context.Baskets.Remove(checkBasket);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }

}
