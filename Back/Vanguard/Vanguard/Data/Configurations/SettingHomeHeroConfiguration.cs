using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class SettingHomeHeroConfiguration : IEntityTypeConfiguration<SettingHomeHero>
{
    public void Configure(EntityTypeBuilder<SettingHomeHero> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
               .ValueGeneratedOnAdd()
               .UseIdentityColumn(1, 1);

        builder.Property(e => e.HeroName)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(e => e.Title)
               .HasColumnType("nvarchar")
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(e => e.Description)
               .HasColumnType("nvarchar")
               .IsRequired()
               .HasMaxLength(500);

        builder.HasOne(e => e.Category)
               .WithMany()
               .HasForeignKey(e => e.CategoryId);

        builder.HasOne(e => e.Tag)
               .WithMany()
               .HasForeignKey(e => e.TagId);
    }
}
