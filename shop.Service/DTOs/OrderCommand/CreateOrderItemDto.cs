namespace shop.Service.DTOs.OrderCommand
{
    public class CreateOrderItemDto
    {
        public int inventoryId { get; set; }
        public int Count { get; set; }
        public int userId { get; set; }
    }
}
