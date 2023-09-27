using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shop.Core.Domain.Role;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.OrderCommand;
using shop.Service.Query;
using Shop.Api.Infrastructure.JwtUtil;

namespace shop.Web.Controllers.Order;

[Authorize]
public class OrderController : ShopController
{
    private readonly IOrderService _orderService;
    private readonly IOrderQueryService _orderQueryService;
    public OrderController(IOrderService orderService, IOrderQueryService orderQueryService)
    {
        _orderService = orderService;
        _orderQueryService = orderQueryService;
    }

    [PermissionChecker(Permission.Order_Management)]
    [HttpGet("OrderFilter")]
    public async Task<ApiResult<OrderFilterResult>> GetOrderByFilter([FromQuery] OrderFilterParams filterParams)
    {
        var result = await _orderQueryService.GetOrderByFilter(filterParams);
        return QueryResult(result);
    }

    [HttpGet("{Id}")]
    public async Task<ApiResult<OrderQueryDto?>> GetOrderById(int Id)
    {
        var result = await _orderQueryService.GetOrderById(Id);
        return QueryResult(result);
    }

    [HttpPost("OrderItem")]
    public async Task<ApiResult> AddOrderItem(CreateOrderItemDto command)
    {
        var result = await _orderService.AddOrderItem(command);
        return CreatedResult(result, null);
    }

    [HttpPost("OrderAddress")]
    public async Task<ApiResult> AddOrderAddress(AddOrderAddressDto command)
    {
        var result = await _orderService.AddOrderAddress(command);
        return CommandResult(result);
    }

    [HttpDelete("OrderAddress")]
    public async Task<ApiResult> RemoveOrderAddress(RemoveOrderAddressDto command)
    {
        var result = await _orderService.RemoveOrderAddress(command);
        return CommandResult(result);
    }

    [HttpPut("orderItem/IncreaseCount")]
    public async Task<ApiResult> IncreaseOrderItem(IncreaseOrderItemCountDto command)
    {
        var result = await _orderService.IncreaseOrderItem(command);
        return CommandResult(result);
    }

    [HttpPut("orderItem/DecreaseCount")]
    public async Task<ApiResult> DecreaseOrderItemCount(DecreaseOrderItemCountDto command)
    {
        var result = await _orderService.DecreaseOrderItem(command);
        return CommandResult(result);
    }

    [HttpDelete("orderItem")]
    public async Task<ApiResult> RemoveOrderItem(RemoveOrderItemDto command)
    {
        var result = await _orderService.RemoveOrderItem(command);
        return CommandResult(result);
    }
}
