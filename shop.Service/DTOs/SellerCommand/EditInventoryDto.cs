using shop.Core.Domain.Seller;

namespace shop.Service.DTOs.SellerCommand
{
    public class EditInventoryDto
    {
        public int SellerId { get;  set; }
        public int InventoryId { get;  set; }
        public int Count { get;  set; }
        public int Price { get;  set; }
        public int? DiscountPercentage { get;  set; }
    }
}
