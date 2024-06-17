using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.Services.Implementations;
using Vanguard.Areas.Admin.Services.Interfaces;
using Vanguard.Areas.Admin.Services;
using Vanguard.Data;
using Vanguard.Models;
using Vanguard.Services.Implementations;
using Vanguard.Services.Interfaces;
using YourNamespace.Filters;

namespace Vanguard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDbContext<VanguardContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
            });

            builder.Services.AddScoped<ITagService, TagService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IShopService, ShopService>();
            builder.Services.AddScoped<IBlogService, BlogService>();
            builder.Services.AddScoped<IHomeSliderService, HomeSliderService>();
            builder.Services.AddScoped<ISettingProductService, SettingProductService>();
            builder.Services.AddScoped<ISettingHomeHeroService, SettingHomeHeroService>();
			builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddScoped<AdminAuthorizationFilter>();
            builder.Services.AddTransient<IBraintreeService, BraintreeService>();


            builder.Services.AddScoped<IHomeService, HomeService>();


            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = true;
                opt.SignIn.RequireConfirmedEmail = false;

            }).AddEntityFrameworkStores<VanguardContext>().AddDefaultTokenProviders();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error500");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
