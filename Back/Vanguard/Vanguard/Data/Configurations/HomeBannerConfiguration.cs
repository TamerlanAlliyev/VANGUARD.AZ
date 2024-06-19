using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Data.Configurations.BaseConfigurations;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class HomeBannerConfiguration : IEntityTypeConfiguration<HomeBanner>
{
    public   void Configure(EntityTypeBuilder<HomeBanner> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("int").UseIdentityColumn(1,1);

        builder.Property(x => x.SubTitle).HasColumnType("nvarchar").HasMaxLength(100); // Made nullable
        builder.Property(x => x.Title).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();

        builder.HasOne(hb => hb.Image)
            .WithOne(i => i.HomeBanner)
            .HasForeignKey<Image>(i => i.HomeBannerId);
    }
}
