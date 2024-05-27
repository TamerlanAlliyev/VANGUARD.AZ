using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Data.Configurations.BaseConfigurations;
using Vanguard.Models;
using Vanguard.Models.BaseEntitys;

namespace Vanguard.Data.Configurations
{
    public class InformationConfiguration : BaseEntityConfiguratiion<Information>
    {
        public override void Configure(EntityTypeBuilder<Information> builder)
        {
            base.Configure(builder);

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Dimensions).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(i => i.Weight).HasColumnType("decimal").IsRequired();
            builder.Property(i => i.Count).HasColumnType("int").IsRequired();

            builder.ToTable("Informations");

            builder.HasOne(i => i.Product)
                .WithMany(i => i.Information)
                .HasForeignKey(i => i.ProductId);

            builder.HasOne(i => i.Size)
                .WithMany(i => i.Information)
                .HasForeignKey(i => i.SizeId);

            builder.HasOne(i => i.Color)
                .WithMany(i => i.Information)
                .HasForeignKey(i => i.ColorId);
        }
    }
}
