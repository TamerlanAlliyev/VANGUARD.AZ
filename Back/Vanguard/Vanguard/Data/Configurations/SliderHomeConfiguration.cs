using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Data.Configurations.BaseConfigurations;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class SliderHomeConfiguration : BaseEntityConfiguratiion<HomeSlider>
{
    public override void Configure(EntityTypeBuilder<HomeSlider> builder)
    {

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasColumnType("nvarchar").HasMaxLength(200).IsRequired(true);
        builder.Property(x => x.Description).HasColumnType("nvarchar").HasMaxLength(500).IsRequired(true);
        builder.Property(x => x.TagId).IsUnicode(false).IsRequired(true);

        builder.HasOne(hs => hs.Image)
                    .WithOne(i => i.HomeSlider)
                    .HasForeignKey<Image>(i => i.HomeSliderId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(hs => hs.Tag)
            .WithMany(t => t.HomeSliders)
            .HasForeignKey(hs => hs.TagId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("HomeSliders");
    }
}


