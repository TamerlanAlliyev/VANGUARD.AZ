using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Data.Configurations.BaseConfigurations;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class InformationConfiguration: BaseEntityConfiguratiion<Information>
{
    public override void Configure(EntityTypeBuilder<Information> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(i=>i.Weight).HasColumnType("varchar").HasMaxLength(50).IsRequired(false);
        builder.Property(i=>i.Dimensions).HasColumnType("varchar").HasMaxLength(50).IsRequired(false);

        builder.ToTable("Informations");

        builder.HasOne(i=>i.Product)
            .WithMany(i=>i.Informations)
            .HasForeignKey(i=>i.ProductId);

        builder.HasOne(i=>i.Size)
            .WithMany(i=>i.Informations)
            .HasForeignKey(i=>i.SizeId);
    }
}
