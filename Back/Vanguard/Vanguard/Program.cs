using Microsoft.EntityFrameworkCore;
using Vanguard.Areas.Admin.Services;
using Vanguard.Areas.Admin.Services.Implementations;
using Vanguard.Areas.Admin.Services.Interfaces;
using Vanguard.Data;

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

            var app = builder.Build();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

      

            app.Run();
        }

    }
}
