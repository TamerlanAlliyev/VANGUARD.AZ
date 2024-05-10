﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
{
    public void Configure(EntityTypeBuilder<ProductTag> builder)
    {
        builder.HasKey(pt=>pt.Id);

        builder.HasOne(pt=>pt.Product)
            .WithMany(pt=>pt.ProductTag)
            .HasForeignKey(pt=>pt.ProductId);

        builder.HasOne(pt=>pt.Tag)
            .WithMany(pt=>pt.ProductTag)
            .HasForeignKey(pt=>pt.TagId);
    }
}
