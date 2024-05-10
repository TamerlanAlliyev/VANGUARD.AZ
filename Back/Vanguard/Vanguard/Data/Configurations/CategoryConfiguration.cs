using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Data.Configurations.BaseConfigurations;
using Vanguard.Models;

namespace Vanguard.Data.Configurations;

public class CategoryConfiguration:BaseEntityConfiguratiion<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(50).IsRequired(true);
        builder.Property(c => c.ParentCategoryId).HasColumnType("int").IsRequired(false);
        builder.ToTable("Categories");

        builder
             .HasOne(c => c.ParentCategory)
             .WithMany(c => c.ChildCategories)
             .HasForeignKey(c => c.ParentCategoryId);
    }
}
