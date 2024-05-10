using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class ProductColorsConfiguration : IEntityTypeConfiguration<ProductColor>
{
    public void Configure(EntityTypeBuilder<ProductColor> builder)
    {
        builder.HasKey(pc=>pc.Id);

        builder.HasOne(pc=>pc.Product)
            .WithMany(pc=>pc.ProductColors)
            .HasForeignKey(pc=>pc.ProductId);

        builder.HasOne(pc=>pc.Color)
            .WithMany(pc=>pc.ProductColors)
            .HasForeignKey(pc=>pc.ColorId);
    }
}
