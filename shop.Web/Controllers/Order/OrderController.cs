using Microsoft.AspNetCore.Mvc;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.OrderCommand;
using shop.Service.Query;

namespace shop.Web.Controllers.Order
{
    public class OrderController : ShopController
    {
        private readonly IOrderService _orderService;
        private readonly OrderQueryService _orderQueryService;
        public OrderController(IOrderService orderService, OrderQueryService orderQueryService)
        {
            _orderService = orderService;
            _orderQueryService = orderQueryService;

        }


        [HttpGet("GetOrderByFilter*")]
        public async Task<ApiResult<OrderFilterResult>> GetOrderByFilter([FromQuery] OrderFilterParams filterParams)
        {
            var result = await _orderQueryService.GetOrderByFilter(filterParams);
            return QueryResult(result);
        }

        [HttpGet("{orderId}*")]
        public async Task<ApiResult<OrderQueryDto?>> GetOrderById(int orderId)
        {
            var result = await _orderQueryService.GetOrderById(orderId);
            return QueryResult(result);
        }

        [HttpPost("AddOrderItem*")]
        public async Task<ApiResult> AddOrderItem(CreateOrderItemDto command)
        {
            var result = await _orderService.AddOrderItem(command);
            return CreatedResult(result,null);
        }

        [HttpPost("AddOrderAddress*")]
        public async Task<ApiResult> AddOrderAddress(AddOrderAddressDto command)
        {
            var result = await _orderService.AddOrderAddress(command);
            return CommandResult(result);
        }

        [HttpDelete("RemoveOrderAddress*")]
        public async Task<ApiResult> RemoveOrderAddress(RemoveOrderAddressDto command)
        {
            var result = await _orderService.RemoveOrderAddress(command);
            return CommandResult(result);
        }

        [HttpPut("orderItem/IncreaseCount*")]
        public async Task<ApiResult> IncreaseOrderItem(IncreaseOrderItemCountDto command)
        {
            var result = await _orderService.IncreaseOrderItem(command);
            return CommandResult(result);
        }

        [HttpPut("orderItem/DecreaseCount*")]
        public async Task<ApiResult> DecreaseOrderItemCount(DecreaseOrderItemCountDto command)
        {
            var result = await _orderService.DecreaseOrderItem(command);
            return CommandResult(result);
        }

        [HttpDelete("orderItem*")]
        public async Task<ApiResult> RemoveOrderItem(RemoveOrderItemDto command)
        {
            var result = await _orderService.RemoveOrderItem(command);
            return CommandResult(result);
        }

    }
}
