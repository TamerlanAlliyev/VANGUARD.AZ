using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using Vanguard.Data.Configurations.BaseConfigurations;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class ProductConfiguration:BaseEntityConfiguratiion<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).HasColumnType("nvarchar").HasMaxLength(150).IsRequired(true);
        builder.Property(p=>p.Description).HasColumnType("nvarchar").HasMaxLength(450).IsRequired(true);
        builder.Property(i => i.Storage).HasColumnType("nvarchar").HasMaxLength(50).IsRequired(false);
        builder.Property(p => p.SellPrice).HasColumnType("decimal").HasPrecision(18, 2).IsRequired(true);
        builder.Property(p=>p.DiscountPrice).HasColumnType("decimal").HasPrecision(18, 2).IsRequired(false);
        builder.Property(p=>p.ClicketCount).HasColumnType("int").IsRequired(false);

        builder.ToTable("Products");
    }
}
