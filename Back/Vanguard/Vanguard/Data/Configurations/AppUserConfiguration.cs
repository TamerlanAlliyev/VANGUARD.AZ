using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Models;

namespace Vanguard.Data.Configurations
{
	public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
	{

		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			builder.HasKey(u => u.Id);
			builder.Property(u => u.Name).IsRequired();
			builder.Property(u => u.Surname).IsRequired();
			builder.Property(u => u.FullName).IsRequired();

			builder.HasOne(u => u.UserAddress)
				   .WithOne(ua => ua.AppUser)
				   .HasForeignKey<AppUser>(u => u.UserAddressId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(u => u.Image)
				   .WithOne(i => i.AppUser)
				   .HasForeignKey<AppUser>(u => u.ImageId);

			builder.HasOne(u => u.AllowedEmployee)
				   .WithOne(e => e.AppUser)
				   .HasForeignKey<AllowedEmployee>(e => e.AppUserId);
		}

	}
}
