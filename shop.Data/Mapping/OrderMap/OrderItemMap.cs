using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Core.Domain.Order;

namespace shop.Data.Mapping
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Ignore(o => o.TotalPrice);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.OrderId)
                .IsRequired();

            builder.Property(c => c.InventoryId)
                .IsRequired();

            builder.Property(c => c.Price)
                .IsRequired();

            builder.Property(c => c.Count)
                .IsRequired();

        }
    }
}
