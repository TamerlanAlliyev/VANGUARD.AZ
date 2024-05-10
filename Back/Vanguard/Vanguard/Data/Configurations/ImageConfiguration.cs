using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Data.Configurations.BaseConfigurations;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class ImageConfiguration: BaseEntityConfiguratiion<Image>
{
    public override void Configure(EntityTypeBuilder<Image> builder)
    {
        base.Configure(builder);

        builder.HasKey(i => i.Id);

        builder.Property(i=>i.Url).HasColumnType("varchar").HasMaxLength(150).IsRequired(true);
        builder.Property(i=>i.IsMain).HasColumnType("bit").HasDefaultValue(true).IsRequired(true);
        builder.Property(i=>i.IsHover).HasColumnType("bit").HasDefaultValue(true).IsRequired(true);

        builder.ToTable("Images");

        builder.HasOne(i => i.Product)
            .WithMany(i => i.Images)
            .HasForeignKey(i => i.ProductId);
    }
}
