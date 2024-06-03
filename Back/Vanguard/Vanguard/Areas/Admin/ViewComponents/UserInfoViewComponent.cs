using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Vanguard.Areas.Admin.ViewModels.Account;
using Vanguard.Areas.Admin.ViewModels.Admin;
using Vanguard.Data;
using Vanguard.Models;
public class UserInfoViewComponent : ViewComponent
{
    readonly VanguardContext _context;
    private readonly UserManager<AppUser> _userManager;
    public UserInfoViewComponent(VanguardContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        UserVM vm = new UserVM();

        if (User.Identity!.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);

            var employee = await _context.AllowedEmployees.Include(i => i!.AppUser!.Image).FirstOrDefaultAsync(e => e.AppUserId == user!.Id);
            if (employee == null) return View();

            vm = new UserVM
            {
                FullName = user.FullName,
                Image = employee.AppUser!.Image?.Url,
            };
        }


        return View(vm);
    }
}