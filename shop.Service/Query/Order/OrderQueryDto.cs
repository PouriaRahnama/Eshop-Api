using shop.Core.Domain.Order;
using shop.Service.Extension.Util;

namespace shop.Service.Query
{
    public class OrderQueryDto : BaseDto
    {
        public int UserId { get; set; }
        public string? UserFullName { get; set; }
        public OrderStatus Status { get; set; }
        public int? Discount { get; set; }
        public DateTime? LastUpdate { get; set; }
        public List<OrderItemDto> OrderItem { get; set; }
        public int OrderTotal { get; set; }
        public string? Shire { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? PostalAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? NationalCode { get; set; }
    }

    public class OrderItemDto : BaseDto
    {
        public string? ProductName { get; set; }
        public string? ProductImageName { get; set; }
        public string? ShopName { get; set; }
        public int OrderId { get; set; }
        public int InventoryId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int TotalPrice => Price * Count;
    }

    public class OrderFilterData : BaseDto
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Family { get; set; }
        public OrderStatus Status { get; set; }
        public string? Shire { get; set; }
        public string? City { get; set; }
        public int TotalPrice { get; set; }
        public int TotalItemCount { get; set; }
    }

    public class OrderFilterParams : BaseFilterParam
    {
        public int? UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public OrderStatus? Status { get; set; }

    }

    public class OrderFilterResult : BaseFilter<OrderFilterData, OrderFilterParams>
    {

    }
}
