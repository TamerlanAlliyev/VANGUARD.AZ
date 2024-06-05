using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Data.Configurations.BaseConfigurations;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class BlogConfiguration : BaseEntityConfiguratiion<Blog>
{
    public override void Configure(EntityTypeBuilder<Blog> builder)
    {
        base.Configure(builder);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title).HasColumnType("nvarchar").HasMaxLength(150).IsRequired(true);
        builder.Property(p => p.MainDescription).HasColumnType("nvarchar").HasMaxLength(1500).IsRequired(true);
        builder.Property(i => i.AddinationDescription).HasColumnType("nvarchar").HasMaxLength(2000).IsRequired(false);


        builder.ToTable("Blog");


    }
}

