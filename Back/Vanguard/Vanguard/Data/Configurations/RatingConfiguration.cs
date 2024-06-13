using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Models;

namespace Vanguard.Data.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.Product)
                   .WithMany(p => p.Ratings)
                   .HasForeignKey(r => r.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(r => r.AppUser)
                   .WithMany(p => p.Ratings)
                   .HasForeignKey(r => r.AppUserId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.Property(r => r.Comment)
                   .HasMaxLength(500);

            builder.Property(r => r.UserRating)
                   .IsRequired();

        }
    }
}
