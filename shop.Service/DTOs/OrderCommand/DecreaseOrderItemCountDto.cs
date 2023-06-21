namespace shop.Service.DTOs.OrderCommand
{
    public class DecreaseOrderItemCountDto
    {
        public int OrderItemId { get; set; }
        public int UserId { get; set; }
        public int Count { get; set; }
    }
}
