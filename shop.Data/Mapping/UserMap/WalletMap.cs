using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using shop.Core.Domain.User;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using shop.Core.Domain.Comment;

namespace shop.Data.Mapping.UserMap
{
    public class WalletMap : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            //builder.Ignore(c => c.Status);


            builder.Property(d => d.Status)
                 .HasConversion(new EnumToStringConverter<WalletType>());
        }
    }
}
