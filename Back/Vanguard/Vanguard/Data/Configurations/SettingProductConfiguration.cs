using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class SettingProductConfiguration: IEntityTypeConfiguration<SettingProduct>
{
    public void Configure(EntityTypeBuilder<SettingProduct> builder)
    {
        builder.Property(p => p.Id)
          .ValueGeneratedOnAdd()
          .UseIdentityColumn(1, 1);

        builder.HasKey(p => p.Id);

    }

}
