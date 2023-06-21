using shop.Core.Domain.Seller;

namespace shop.Service.DTOs.SellerCommand
{
    public class EditInventoryDto
    {
        public long SellerId { get; private set; }
        public long InventoryId { get; private set; }
        public int Count { get; private set; }
        public int Price { get; private set; }
        public int? DiscountPercentage { get; private set; }
    }
}
