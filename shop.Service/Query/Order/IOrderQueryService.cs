using Microsoft.EntityFrameworkCore;

namespace shop.Service.Query
{
    public interface IOrderQueryService
    {
        Task<OrderQueryDto?> GetOrderById(int OrderId);
        Task<OrderFilterResult> GetOrderByFilter(OrderFilterParams filterParams);    
    }
}
