using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Models;

namespace Vanguard.Data.Configurations
{
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.HasKey(ua => ua.Id);
            builder.Property(ua => ua.Country).HasColumnType("nvarchar").HasMaxLength(30).IsRequired(true);
            builder.Property(ua => ua.City).HasColumnType("nvarchar").HasMaxLength(30).IsRequired(true);
            builder.Property(ua => ua.HomeAddress).HasColumnType("nvarchar").HasMaxLength(30).IsRequired(true);
            builder.Property(ua => ua.PostalCode).HasColumnType("nvarchar").HasMaxLength(150).IsRequired(true);


        }
    }
}
