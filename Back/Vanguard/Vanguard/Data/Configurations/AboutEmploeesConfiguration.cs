using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class AboutEmploeesConfiguration : IEntityTypeConfiguration<AboutEmploees>
{
    public void Configure(EntityTypeBuilder<AboutEmploees> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn(1, 1);
        builder.Property(x => x.FullName)
               .HasColumnType("nvarchar")
               .HasMaxLength(50)
               .IsRequired(true);

        builder.Property(x => x.Position)
           .HasColumnType("nvarchar")
           .HasMaxLength(50)
           .IsRequired(true);


        builder.HasOne(x => x.Image)
               .WithOne(x => x.AboutEmploees)
               .HasForeignKey<AboutEmploees>(x => x.ImageId);
    }
}