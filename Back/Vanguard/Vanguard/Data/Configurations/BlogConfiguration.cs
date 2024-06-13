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

        builder.Property(p => p.Title)
               .HasColumnType("nvarchar")
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(p => p.MainDescription)
               .HasColumnType("nvarchar(MAX)")
               .IsRequired();

        builder.Property(p => p.AddinationDescription)
               .HasColumnType("nvarchar(MAX)");

        builder.ToTable("Blog");
    }
}

