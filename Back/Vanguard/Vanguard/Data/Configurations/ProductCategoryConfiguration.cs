using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(pc => pc.Id);

        builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);

        builder.HasOne(pc => pc.Product)
               .WithMany(pc => pc.ProductCategory)
               .HasForeignKey(pc => pc.ProductId);

        builder.HasOne(pc => pc.Category)
               .WithMany(pc => pc.ProductCategory)
               .HasForeignKey(pc => pc.CategoryId);
    }
}
