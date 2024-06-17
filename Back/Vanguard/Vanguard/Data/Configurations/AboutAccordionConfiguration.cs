using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class AboutAccordionConfiguration : IEntityTypeConfiguration<AboutAccordion>
{
    public void Configure(EntityTypeBuilder<AboutAccordion> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn(1, 1);
        builder.Property(x => x.Title)
               .HasColumnType("nvarchar")
               .HasMaxLength(200)
               .IsRequired(true);

        builder.Property(x => x.Title)
          .HasColumnType("nvarchar")
          .HasMaxLength(1500)
          .IsRequired(true);

    }
}