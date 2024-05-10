using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Data.Configurations.BaseConfigurations;
using Vanguard.Models;

namespace Vanguard.Data.Configurations
{
    public class ColorConfiguration:BaseEntityConfiguratiion<Color>
    {
        public override void Configure(EntityTypeBuilder<Color> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired(true);
            builder.Property(x => x.HexCode).HasColumnType("varchar").HasMaxLength(50).IsRequired(true);

            builder.ToTable("Colors");
        }
    }
}
