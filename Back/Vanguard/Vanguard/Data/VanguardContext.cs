using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vanguard.Models;
using Vanguard.Models.BaseEntitys;

namespace Vanguard.Data;

public class VanguardContext : IdentityDbContext<AppUser>
{
	private readonly IHttpContextAccessor? _accessor;

	public VanguardContext(DbContextOptions options, IHttpContextAccessor accessor) : base(options)
	{
		_accessor = accessor;
	}

	public DbSet<Product> Products { get; set; } = null!;
	public DbSet<Size> Sizes { get; set; }
	public DbSet<Image> Images { get; set; }
	public DbSet<Color> Colors { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<ProductCategory> ProductCategory { get; set; }
	public DbSet<ProductTag> ProductTag { get; set; }
	public DbSet<Tag> Tags { get; set; }
	public DbSet<Information> Informations { get; set; }
	public DbSet<ProductColor> ProductColor { get; set; }
	public DbSet<Gender> Genders { get; set; }
	public DbSet<UserAddress> UserAddresses { get; set; }
	public DbSet<Basket> Baskets{ get; set; }
	public DbSet<Wish> Wishs { get; set; }
    public DbSet<AllowedEmployee> AllowedEmployees { get; set; }
	public DbSet<Blog> Blogs { get; set; }
	public DbSet<BlogCategory> BlogCategory { get; set; }
	public DbSet<BlogTag> BlogTag { get; set; }
	public DbSet<HomeBanner> HomeBanners { get; set; }
	public DbSet<HomeSlider> HomeSliders {  get; set; } 
	public DbSet<ShopBanner> ShopBanner { get; set; }
    public DbSet<SettingProduct> SettingProducts { get; set; }
	public DbSet<SettingHomeHero> SettingHomeHero {  get; set; }
	public DbSet<Rating> Ratings { get; set; }
	public DbSet<BlogComment> BlogComments { get; set; }
	public DbSet<About> About { get; set; }
	public DbSet<AboutAccordion> AboutAccordion { get; set; }
	public DbSet<AboutEmploees> AboutEmploees { get; set; }
    public DbSet<Connection> Connections { get; set; }
	public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Contact> Contacts { get; set; }	
	public DbSet<EmailSend> EmailSends { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<IdentityUser>().HasKey(u => u.Id);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(VanguardContext).Assembly);
	}


	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		if (_accessor != null)
		{
			var entries = ChangeTracker.Entries<BaseAuditable>();

			foreach (var entry in entries)
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedBy = _accessor.HttpContext?.User.Identity?.Name ?? "newUser";
						entry.Entity.CreatedDate = DateTime.UtcNow.AddHours(4);
						entry.Entity.IsDeleted = false;
						entry.Entity.IPAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "unknown";
						break;
					case EntityState.Modified:
						entry.Entity.ModifiedBy = _accessor.HttpContext?.User.Identity?.Name ?? "unknown";
						entry.Entity.ModifiedDate = DateTime.UtcNow.AddHours(4);
						entry.Entity.IPAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "unknown";
						break;
					default:
						break;
				}
			}
		}
		return base.SaveChangesAsync(cancellationToken);
	}
}

