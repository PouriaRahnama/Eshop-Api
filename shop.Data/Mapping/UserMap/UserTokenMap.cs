using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using shop.Core.Domain.User;


namespace shop.Data.Mapping.UserMap
{
    public class UserTokenMap : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.HashJwtToken)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(b => b.HashRefreshToken)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(b => b.Device)
                .IsRequired()
                .HasMaxLength(100);
        }

    }
}
