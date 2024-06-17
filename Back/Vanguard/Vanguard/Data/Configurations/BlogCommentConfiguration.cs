using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vanguard.Data.Configurations.BaseConfigurations;
using Vanguard.Models;

namespace Vanguard.Data.Configurations
{
    public class BlogCommentConfiguration: BaseEntityConfiguratiion<BlogComment>
    {
        public override void Configure(EntityTypeBuilder<BlogComment> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Comment).HasColumnType("nvarchar(max)").IsRequired(true);

            builder.HasOne(bc => bc.AppUser)
           .WithMany(au => au.BlogComments)
           .HasForeignKey(bc => bc.AppUserId)
           .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bc => bc.Blog)
                .WithMany(b => b.BlogComments)
                .HasForeignKey(bc => bc.BlogId)
                .OnDelete(DeleteBehavior.NoAction); 

            builder.HasOne(bc => bc.Reply)
                .WithMany(bc => bc.AnswerComments)
                .HasForeignKey(bc => bc.ReplyId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
