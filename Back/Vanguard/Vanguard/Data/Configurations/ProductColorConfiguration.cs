using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class ProductColorConfiguration : IEntityTypeConfiguration<ProductColor>
{
    public void Configure(EntityTypeBuilder<ProductColor> builder)
    {
        builder.HasKey(pc => pc.Id);

        builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);

        builder.HasOne(pc => pc.Product)
               .WithOne(pc => pc.ProductColors);

        builder.HasOne(pc => pc.Color)
               .WithMany(pc => pc.ProductColors)
               .HasForeignKey(pc => pc.ColorId);
    }
}
