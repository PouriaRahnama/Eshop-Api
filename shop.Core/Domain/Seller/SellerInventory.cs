using shop.Core.Commons;
using shop.Core.Domain.Order;

namespace shop.Core.Domain.Seller
{
    public class SellerInventory : BaseEntity
    {
        public int SellerId { get;  set; }
        public int ProductId { get;  set; }
        public int Count { get;  set; }
        public int ReservedCount { get; set; }   // مقدار رزرو شده
        public int AvailableCount => Count - ReservedCount;  // موجودی قابل فروش
        public int Price { get;  set; }
        public int? DiscountPercentage { get;  set; }

        public virtual Seller Seller { get; set; }
        public virtual shop.Core.Domain.Product.Product Product { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }


    }
}