using shop.Core.Commons;

namespace shop.Core.Domain.Order
{
    public class OrderItem:BaseEntity
    {
        public int InventoryId { get; set; }
        public int OrderId { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public int TotalPrice => Price * Count;
        public virtual shop.Core.Domain.Seller.SellerInventory Inventory { get; set; }
        public virtual Order Order { get; set; }
    }
}