using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using Vanguard.Data;
using Vanguard.Models;
using Vanguard.Services.Interfaces;
using Vanguard.ViewModels.Blog;
using Vanguard.ViewModels.Home;
using Vanguard.ViewModels.Shop;
using Vanguard.ViewModels.Wish;

public class HomeController : Microsoft.AspNetCore.Mvc.Controller
{
    readonly VanguardContext _context;
    readonly UserManager<AppUser> _userManager;
    readonly IShopService _shopService;
    readonly IHomeService _homeService;
    public HomeController(VanguardContext context, UserManager<AppUser> userManager, IShopService shopService, IHomeService homeService)
    {
        _context = context;
        _userManager = userManager;
        _shopService = shopService;
        _homeService = homeService;
    }

    public async Task<IActionResult> Index()
    {
        var lastBlogs = await _context.Blogs.Where(b=>!b.IsDeleted)
                                             .OrderByDescending(b=>b.CreatedDate)
                                             .Take(10)
                                             .Include(b => b.Images)
                                             .Include(b => b.AppUser)
                                             .ThenInclude(b => b.AllowedEmployee!.Role)
                                             .Include(b => b.Categories)
                                             .ThenInclude(bc => bc.Category)
                                             .Select(b => new BlogAllVM
                                             {
                                                 Id = b.Id,
                                                 Title = b.Title,
                                                 Clicked = b.Clickeds,
                                                 Image = b.Images.FirstOrDefault(i => !i.IsDeleted && i.IsMain).Url,
                                                 IsVideo = b.Images.FirstOrDefault(i => !i.IsDeleted && i.IsMain).IsVideo,
                                                 Created = b.CreatedDate,
                                                 CreatedBy = b.AppUser.FullName!,
                                                 Descripton = b.MainDescription
                                             }).ToListAsync();

        HomeVM vm = new HomeVM
        {
            TrendyVM = await _homeService.TrendySelectedAsync(),
            LastBlogs = lastBlogs,
            Sliders = await _context.HomeSliders.Include(s=>s.Image).Include(s=>s.Tag).ToListAsync(),
        };

        return View(vm);
    }
    public IActionResult Forbidden()
    {
        return View("Error403");
    }
}

