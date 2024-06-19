using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Data.Configurations.BaseConfigurations;
using Vanguard.Models;

namespace Vanguard.Data.Configurations
{
    public class ConnectionConfiguration : BaseEntityConfiguratiion<Connection>
    {
        public override void Configure(EntityTypeBuilder<Connection> builder)
        {
            base.Configure(builder);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).HasColumnType("nvarchar").HasMaxLength(150).IsRequired(true);
            builder.Property(p => p.Email).HasColumnType("nvarchar").HasMaxLength(150).IsRequired(true);
            builder.Property(p => p.Message).HasColumnType("nvarchar").HasMaxLength(1000).IsRequired(true);


        }
    }
}