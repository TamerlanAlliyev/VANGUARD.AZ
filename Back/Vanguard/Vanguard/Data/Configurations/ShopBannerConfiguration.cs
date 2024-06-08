using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class ShopBannerConfiguration : IEntityTypeConfiguration<ShopBanner>
{
    public void Configure(EntityTypeBuilder<ShopBanner> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasColumnType("nvarchar")
            .HasMaxLength(150)
            .IsRequired(true);

        builder.Property(x => x.Description)
            .HasColumnType("nvarchar")
            .HasMaxLength(150)
            .IsRequired(true);

        builder.HasOne(hs => hs.Image)
            .WithOne(i => i.ShopBanner)
            .HasForeignKey<Image>(i => i.ShopBannerId);
    }
}
