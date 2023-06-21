using shop.Core.Domain.Seller;

namespace shop.Service.DTOs.SellerCommand
{
    public class EditSellerDto
    {
        public int SellerId { get; set; }
        public string ShopName { get; set; }
        public string NationalCode { get; set; }
        public SellerStatus Status { get; set; }
    }
}
