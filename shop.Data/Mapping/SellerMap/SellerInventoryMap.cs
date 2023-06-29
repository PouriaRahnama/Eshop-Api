using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using shop.Core.Domain.Seller;

namespace shop.Data.Mapping
{
    public class SellerInventoryMap : IEntityTypeConfiguration<SellerInventory>
    {
        public void Configure(EntityTypeBuilder<SellerInventory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(p => p.OrderItems)
                .WithOne(p => p.Inventory)
                .HasForeignKey(p => p.InventoryId)
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
        }
    }
}
