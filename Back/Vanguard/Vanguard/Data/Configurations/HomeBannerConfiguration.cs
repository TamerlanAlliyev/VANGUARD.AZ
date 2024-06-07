using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class HomeBannerConfiguration:IEntityTypeConfiguration<HomeBanner>
{
	public void Configure(EntityTypeBuilder<HomeBanner> builder)
	{

        builder.HasKey(x => x.Id);

        builder.Property(x=>x.SubTitle).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(false);
		builder.Property(x=>x.Title).HasColumnType("nvarchar").HasMaxLength(150).IsRequired(true);

		builder.HasOne(x => x.Category)
			.WithMany(x => x.HomeBanners)
			.HasForeignKey(x => x.Id);

        builder.HasOne(hs => hs.Image)
            .WithOne(i => i.HomeBanner)
            .HasForeignKey<Image>(i => i.HomeBannerId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
