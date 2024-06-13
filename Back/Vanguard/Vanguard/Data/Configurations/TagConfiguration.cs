using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Data.Configurations.BaseConfigurations;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class TagConfiguration:BaseEntityConfiguratiion<Tag>
{
    public override void Configure(EntityTypeBuilder<Tag> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .HasColumnType("nvarchar")
               .HasMaxLength(50)
               .IsUnicode()
               .IsRequired();

        builder.ToTable("Tags");
    }
}
