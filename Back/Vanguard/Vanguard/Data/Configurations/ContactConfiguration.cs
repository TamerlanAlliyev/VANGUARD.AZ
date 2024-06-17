using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Data.Configurations.BaseConfigurations;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class ContactConfiguration:IEntityTypeConfiguration<Contact>
{
	public  void Configure(EntityTypeBuilder<Contact> builder)
	{
		builder.Property(x => x.Id).UseIdentityColumn(1,1);
		builder.Property(x=>x.Address).HasColumnType("nvarchar").HasMaxLength(3000).IsRequired(true);
		builder.Property(x=>x.Number).HasColumnType("nvarchar").HasMaxLength(50).IsRequired(true);
		builder.Property(x=>x.AddressLink).HasColumnType("nvarchar").HasMaxLength(3000).IsRequired(true);
		builder.Property(x=>x.AddressUrl).HasColumnType("nvarchar").HasMaxLength(3000).IsRequired(true);
        builder.Property(x=>x.Email).HasColumnType("nvarchar").HasMaxLength(150).IsRequired(true);
	}
}
