using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class ProductSizeConfiguration:IEntityTypeConfiguration<ProductSize>
{
    public virtual void Configure(EntityTypeBuilder<ProductSize> builder)
    {
        builder.HasKey(ps=>ps.Id);

        builder.HasOne(ps => ps.Product)
            .WithMany(ps => ps.ProductSizes)
            .HasForeignKey(ps => ps.ProductId);

        builder.HasOne(ps => ps.Size)
            .WithMany(ps=>ps.ProductSizes)
            .HasForeignKey(ps => ps.SizeId);
    }
}
