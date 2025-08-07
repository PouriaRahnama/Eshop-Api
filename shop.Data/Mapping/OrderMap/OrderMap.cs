using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using shop.Core.Domain.Comment;
using shop.Core.Domain.Order;

namespace shop.Data.Mapping
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Ignore(p => p.OrderTotal);

            //builder.Ignore(c => c.orderStatus);

            builder.HasKey(x => x.Id);

            builder.Property(c => c.UserId)
                .IsRequired();

            //builder.Property(c => c.OrderStatusId)
            //    .IsRequired();

            //builder.Property(d => d.Status)
            //    .HasConversion(new EnumToStringConverter<OrderStatus>());
        }
    }
}
