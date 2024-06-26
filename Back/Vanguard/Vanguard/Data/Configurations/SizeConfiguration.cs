﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Data.Configurations.BaseConfigurations;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class SizeConfiguration: BaseEntityConfiguratiion<Size>
{
    public override void Configure(EntityTypeBuilder<Size> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(50).IsUnicode(true).IsRequired(true);

        builder.ToTable("Sizes");

    }
}
