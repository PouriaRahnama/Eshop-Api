using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using shop.Core.Domain.Seller;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using shop.Core.Domain.Comment;

namespace shop.Data.Mapping
{
    public class SellerMap : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.HasKey(x => x.Id);
            //builder.Ignore(c => c.SellerStatus);
            //builder.Property(d => d.Status)
            //    .HasConversion(new EnumToStringConverter<SellerStatus>());
        }
    }
}
