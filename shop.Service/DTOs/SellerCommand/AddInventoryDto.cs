namespace shop.Service.DTOs.SellerCommand
{
    public class AddInventoryDto
    {
        public int SellerId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int? PercentageDiscount { get; set; }
    }
}
