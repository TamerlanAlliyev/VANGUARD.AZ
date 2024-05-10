using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Models.BaseEntitys;

namespace Vanguard.Data.Configurations.BaseConfigurations;

public class BaseEntityConfiguratiion<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseAuditable
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.Id).HasColumnType("int").UseIdentityColumn(1, 1);
        builder.Property(e=>e.IsDeleted).HasColumnType("bit").HasDefaultValue("false");
        builder.Property(e=>e.CreatedBy).HasColumnType("nvarchar").HasMaxLength(70).IsRequired(true);
        builder.Property(e=>e.CreatedDate).HasColumnType("datetime").IsRequired(true);
        builder.Property(e=>e.ModifiedBy).HasColumnType("nvarchar").HasMaxLength(70).IsRequired(false);
        builder.Property(e=>e.ModifiedDate).HasColumnType("datetime").IsRequired(false);
        builder.Property(e=>e.IPAddress).HasColumnType("varchar").HasMaxLength(250).IsRequired(true);
    }
}

