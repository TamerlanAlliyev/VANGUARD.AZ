using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class AboutConfiguration : IEntityTypeConfiguration<About>
{
    public void Configure(EntityTypeBuilder<About> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn(1, 1);
        builder.Property(x => x.Title)
               .HasColumnType("nvarchar")
               .HasMaxLength(1500)
               .IsRequired(true);

        builder.HasOne(x => x.Image)
               .WithOne(x => x.About)
               .HasForeignKey<Image>(x => x.AboutId);
    }
}