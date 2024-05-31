namespace Vanguard.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq;
using Vanguard.Helpers;
using Vanguard.Models;
using Vanguard.ViewModels.Shop;

public interface IShopService
{
    public IQueryable<Product> ProductgetQuery();
    public  Task<Product?> ProductGetAsync(int id);
    public  Task<List<Product>> ProductModelAsync(string model);
    public  Task<ShopDetailVM?> DetailAsync(int id);


}
