using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using shop.Core.Domain.Comment;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;

namespace shop.Data.Mapping.Comments
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            //builder.Ignore(c => c.Status);

            builder.HasKey(x => x.Id);

            builder.Property(b => b.Text)
              .HasMaxLength(500)
              .IsRequired();

            builder.Property(b => b.UserId)
              .IsRequired();

            builder.Property(b => b.ProductId)
              .IsRequired();

            builder.Property(d => d.Status)
                 .HasConversion(new EnumToStringConverter<CommentStatus>());

            //builder.Property(b => b.StatusId)
            // .IsRequired();

        }
    }
}
