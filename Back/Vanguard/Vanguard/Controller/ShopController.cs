using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using Vanguard.Data;
using Vanguard.Helpers;
using Vanguard.Models;
using Vanguard.Services.Interfaces;
using Vanguard.ViewModels.Shop;
using Vanguard.ViewModels.Shop.AdditionVMs;
using Vanguard.ViewModels.Wish;

namespace Vanguard.Controller;

public class ShopController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly VanguardContext _context;
    readonly IShopService _shopService;
    private readonly UserManager<AppUser> _userManager;

    public ShopController(VanguardContext context, IShopService shopService, UserManager<AppUser> userManager)
    {
        _context = context;
        _shopService = shopService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(int? tagId,ShopVM shopVM)
    {
        var query = _shopService.ProductgetQuery();

        List<Product> products = new List<Product>();
        int totalItems = 0;




        //Category
        List<SelectedCats> cats = new List<SelectedCats>();
        if (shopVM.SentCategories != null && shopVM.SentCategories.Any())
        {
            foreach (var cat in shopVM.SentCategories)
            {
                SelectedCats ca = new SelectedCats
                {
                    catId = cat,
                    select = true,
                };
                cats.Add(ca);
            }
            var selectedCategoryIds = cats.Select(c => c.catId).ToList();
            query = query.Where(p => p.ProductCategory.Any(pc => selectedCategoryIds.Contains(pc.CategoryId)));
        }



        //Gender
        List<SelectedGenders> genders = new List<SelectedGenders>();
        if (shopVM.SentGenders != null && shopVM.SentGenders.Any())
        {
            foreach (var gender in shopVM.SentGenders)
            {
                SelectedGenders gen = new SelectedGenders
                {
                    genId = gender,
                    select = true,
                };
                genders.Add(gen);
            }
            var selectedGenderIds = genders.Select(c => c.genId).ToList();
            var validGenders = await _context.Genders.Where(c => !c.IsDeleted && selectedGenderIds.Contains(c.Id))
                                     .Select(c => c.Id)
                                     .ToListAsync();


            query = query.Where(p => validGenders.Contains(p.GenderId));
        }




        //Color
        List<SelectedColors> cols = new List<SelectedColors>();
        if (shopVM.SentColors != null && shopVM.SentColors.Any())
        {
            foreach (var col in shopVM.SentColors)
            {
                SelectedColors co = new SelectedColors
                {
                    colorId = col,
                    select = true,
                };
                cols.Add(co);
            }


            var selectedColorIds = cols.Select(c => c.colorId).ToList();
            var validColorIds = await _context.Colors
                                     .Where(c => !c.IsDeleted && selectedColorIds.Contains(c.Id))
                                     .Select(c => c.Id)
                                     .ToListAsync();

            query = query.Where(p => validColorIds.Contains(p.ProductColors.ColorId));
        }



        //Size
        List<SelectedSizes> sizesLi = new List<SelectedSizes>();
        if (shopVM.SentSizes != null && shopVM.SentSizes.Any())
        {
            foreach (var size in shopVM.SentSizes)
            {
                SelectedSizes siz = new SelectedSizes
                {
                    sizeId = size,
                    select = true,
                };
                sizesLi.Add(siz);
            }

            var selectedSizeIds = sizesLi.Select(c => c.sizeId).ToList();
            query = query.Where(p => p.Information.Any(pc => selectedSizeIds.Contains(pc.SizeId)));
        }


        //Tags
        List<Tag> selectedTags = new List<Tag>();
        if (shopVM.SentTag != null && shopVM.SentTag.Any())
        {
            selectedTags = await _context.Tags.Where(pc => shopVM.SentTag.Contains(pc.Id)).ToListAsync();

            query = query.Where(p => p.ProductTag.Any(pc => shopVM.SentTag.Contains(pc.TagId)));
        }

        if (tagId!=null)
        {
            query = query.Where(p=>p.ProductTag.Any(t=>t.TagId==tagId));
        }





        //Price
        if (shopVM.MinPrice != null || shopVM.MaxPrice != null)
        {
            if (shopVM.MinPrice != null)
            {
                query = query.Where(p => p.SellPrice >= shopVM.MinPrice);
            }
            if (shopVM.MaxPrice != null)
            {
                query = query.Where(p => p.SellPrice <= shopVM.MaxPrice);
            }
        }



        if (shopVM.OrderBy == 2)
        {
            query = query.Where(p => p.Information.Sum(p => p.OrderCount) >= 50);
        }
        else if (shopVM.OrderBy == 3)
        {
            query = query.Where(p => p.CreatedDate >= (DateTime.UtcNow.AddDays(-5).AddHours(4)));
        }
        else if (shopVM.OrderBy == 4)
        {
            query = query.Where(p => p.DiscountPrice > 0);
        }

        if (shopVM.OrderBy == 5)
        {
            query = query.OrderByDescending(p => p.SellPrice).ThenBy(p => p.Id);
        }
        else if (shopVM.OrderBy == 6)
        {
            query = query.OrderBy(p => p.SellPrice).ThenBy(p => p.Id);
        }
        else
        {
            query = query.OrderByDescending(p => p.Id);
        }

        totalItems = await query.CountAsync();
        // Products
        products = await query
               .Skip((shopVM.PageInfo.CurrentPage - 1) * shopVM.PageInfo.ItemsPerPage)
               .Take(shopVM.PageInfo.ItemsPerPage)
               .ToListAsync();


        List<WishVM> wishesVM = new List<WishVM>();
        List<Wish> wishes = new List<Wish>();

        if (!User.Identity!.IsAuthenticated)
        {
            if (HttpContext.Request.Cookies["wish"] != null)
                wishesVM = JsonConvert.DeserializeObject<List<WishVM>>(HttpContext.Request.Cookies["wish"]);
        }
        else
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            wishes = await _context.Wishs.Where(w => w.AppUserId == user!.Id).ToListAsync();
            wishesVM = wishes.Select(w => new WishVM { Id = w.ProductId }).ToList();
        }

        List<int> wishProductIds = wishesVM.Select(w => w.Id).ToList();

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
            Offer = p.DiscountPrice > 0 ? (int)(((p.SellPrice - p.DiscountPrice) / p.SellPrice) * 100) : 0,
            IsWish = wishProductIds.Contains(p.Id)
        }).ToList();


        //////// ViewModel
        //Categories
        var categories = await _context.Categories.Where(c => !c.IsDeleted && c.ProductCategory!.Any(pc => pc.CategoryId == c.Id))
                                                  .Include(c => c.ProductCategory)
                                                  .ToListAsync();

        var ReadyCategory = categories.Select(category => new ReadyCategory
        {
            Category = category,
        }).ToList();


        //Color
        var colors = await _context.Colors
                                   .Where(c => !c.IsDeleted && c.ProductColors.Any(pc => pc.ColorId == c.Id))
                                   .Include(c => c.ProductColors)
                                   .ToListAsync();


        var ReadyColor = colors.Select(color => new ReadyColors
        {
            Color = color,
        }).ToList();


        //Size
        var sizes = await _context.Sizes
                                   .Where(c => !c.IsDeleted && c.Information!.Any(pc => pc.SizeId == c.Id))
                                   .Include(c => c.Information)
                                   .ToListAsync();


        var ReadySize = sizes.Select(size => new ReadySizes
        {
            Size = size,
        }).ToList();


        //Gender
        var gendrs = await _context.Genders.Where(g => g.Products.Any()).ToListAsync();

        var ReadyGender = gendrs.Select(gender => new ReadyGender
        {
            Gender = gender,
        }).ToList();



        // Price
        decimal? MaPrices = null;
        decimal? MiPrices = null;
        var price = await _context.Products.Where(p => !p.IsDeleted).ToListAsync();
        if (price.Any())
        {
            MaPrices = price.Max(p => p.SellPrice);
            MiPrices = price.Min(p => p.SellPrice);
        }

        int MaxPrices = MaPrices.HasValue ? (int)MaPrices.Value : 0;
        int MinPrices = MiPrices.HasValue ? (int)MiPrices.Value : 0;


        ShopVM vm = new ShopVM
        {
            Product = shopProductVMs,
            ReadyCategory = ReadyCategory,
            SelectedCats = cats,
            ReadyColors = ReadyColor,
            SelectedColors = cols,

            SelectedGenders = genders,
            ReadyGender = ReadyGender,

            ReadySizes = ReadySize,
            SelectedSizes = sizesLi,

            SelectedTags = selectedTags,
            MinPrice = MinPrices,
            MaxPrice = MaxPrices,
            Grid = shopVM.Grid,
            PageInfo = new PageInfo
            {
                CurrentPage = shopVM.PageInfo.CurrentPage,
                TotalItems = totalItems,
                ItemsPerPage = shopVM.PageInfo.ItemsPerPage,
            },
            CurrrentMinPrice = shopVM.MinPrice,
            CurrrentMaxPrice = shopVM.MaxPrice
        };



        return View(vm);
    }


    public async Task<IActionResult> TagsSearch(string? text)
    {
        if (text == null)
        {
            ViewData["TagsSearch"] = null;
            return PartialView("_TagsSearchPartialView");
        }
        text = text.ToLower().Trim();

        List<Tag> tags = await _context.Tags
                                       .Where(p => !p.IsDeleted && p.Name.Contains(text))
                                       .ToListAsync();


        ViewData["TagsSearch"] = tags != null ? tags : null;

        return PartialView("_TagsSearchPartialView");
    }




    public async Task<IActionResult> Detail(int? id)
    {
        if (id == null || id < 1) return View("Error404");

        try
        {
            var vm = await _shopService.DetailAsync(id.Value);
            return View(vm);
        }
        catch (KeyNotFoundException ex)
        {
            return View("Error404", new ServiceResult(false, ex.Message, 404));
        }
        catch (ArgumentException ex)
        {
            return View("Error400", new ServiceResult(false, ex.Message, 400));
        }
        catch (Exception ex)
        {
            return View("Error500", new ServiceResult(false, ex.Message, 500));
        }
    }


}