using Microsoft.EntityFrameworkCore;
using shop.Data.ApplicationContext;

namespace shop.Service.Query
{
    public class OrderQueryService
    {
       private readonly IApplicationContext _Context;

        public OrderQueryService(IApplicationContext Context)
        {
            _Context = Context;
        }

        public async Task<OrderQueryDto?> GetOrderById(int OrderId)
        {
            var order = await _Context.Set<Core.Domain.Order.Order>()
                .FirstOrDefaultAsync(f => f.Id == OrderId);

            if (order == null)
                return null;

            var orderDto = order.Map();

            orderDto.UserFullName = await _Context.Set<Core.Domain.User.User>()
                .Where(f => f.Id == orderDto.UserId)
                .Select(s => $"{s.Name} {s.Family}").FirstAsync();

            orderDto.OrderItem =await orderDto.GetOrderItems(_Context);

            return orderDto;
        }

        public async Task<OrderFilterResult> GetOrderByFilter(OrderFilterParams filterParams)
        {
            var @params = filterParams;
            var result = _Context.Set<Core.Domain.Order.Order>().AsQueryable();

            if (@params.Status != null)
                result = result.Where(r => r.Status == @params.Status);

            if (@params.UserId != null)
                result = result.Where(r => r.UserId == @params.UserId);

            if (@params.StartDate != null)
                result = result.Where(r => r.CreateON.Date >= @params.StartDate.Value.Date);

            if (@params.EndDate != null)
                result = result.Where(r => r.CreateON.Date <= @params.EndDate.Value.Date);


            var skip = (@params.PageId - 1) * @params.Take;
            var model = new OrderFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take)
                    .Select(order => order.MapFilterData())
                    .ToListAsync(),
                FilterParams = @params
            };
            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }

    }
}
