namespace shop.Service.DTOs.OrderCommand
{
    public class IncreaseOrderItemCountDto
    {
        public int OrderItemId { get; set; }
        public int UserId { get; set; }
        public int Count { get; set; }
    }
}
