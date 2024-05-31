using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Data.Configurations.BaseConfigurations;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class GenderConfiguration:BaseEntityConfiguratiion<Gender>
{
    public override void Configure(EntityTypeBuilder<Gender> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x=>x.Name).HasColumnType("nvarchar").HasMaxLength(30).IsRequired();
        builder.ToTable(nameof(Gender));

        builder.HasMany(x => x.Products)
            .WithOne(x => x.Gender)
            .HasForeignKey(x => x.GenderId);
    }
}
