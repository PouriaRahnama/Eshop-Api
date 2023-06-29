using IntelliTect.Coalesce.Utilities;
using Microsoft.AspNetCore.Mvc;
using shop.Core.Domain.Role;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.SellerCommand;
using shop.Service.Query;
using Shop.Api.Infrastructure.JwtUtil;
using System.Security.Claims;

namespace shop.Web.Controllers.Seller;

public class SellerController : ShopController
{
    private readonly ISellerService _sellerService;
    private readonly SellerQueryService _sellerQueryService;
    public SellerController(ISellerService sellerService, SellerQueryService sellerQueryService)
    {
        _sellerService = sellerService;
        _sellerQueryService = sellerQueryService;
    }

    [PermissionChecker(Permission.Seller_Management)]
    [HttpGet("GetSellerByFilter*")]
    public async Task<ApiResult<SellerFilterResult>> GetSellerByFilter([FromQuery] SellerFilterParams filterParams)
    {
        var result = await _sellerQueryService.GetSellerByFilter(filterParams);
        return QueryResult(result);
    }

    [HttpGet("{id}*")]
    public async Task<ApiResult<SellerDto?>> GetSellerById(int id)
    {
        var result = await _sellerQueryService.GetSellerById(id);
        return QueryResult(result);
    }

    [PermissionChecker(Permission.Seller_Management)]
    [HttpPost("AddSeller*")]
    public async Task<ApiResult> AddSeller(AddSellerDto command)
    {
        var result = await _sellerService.AddSeller(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.Seller_Management)]
    [HttpPut("UpdateSeller*")]
    public async Task<ApiResult> UpdateSeller([FromForm] EditSellerDto command)
    {
        var result = await _sellerService.UpdateSeller(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.Add_Inventory)]
    [HttpPost("AddInventory*")]
    public async Task<ApiResult> AddInventory(AddInventoryDto command)
    {
        var result = await _sellerService.AddInventory(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.Add_Inventory)]
    [HttpPut("UpdateInventory*")]
    public async Task<ApiResult> EditInventory(EditInventoryDto command)
    {
        var result = await _sellerService.UpdateInventory(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.Add_Inventory)]
    [HttpDelete("RemoveInventory*")]
    public async Task<ApiResult> RemoveInventory(RemoveInventoryDto command)
    {
        var result = await _sellerService.RemoveInventory(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.Seller_Panel)]
    [HttpGet("GetSellerInventoryById/{inventoryId}*")]
    public async Task<ApiResult<InventoryDto?>> GetSellerInventoryById(int inventoryId)
    {
        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var seller = await _sellerQueryService.GetSellerByUserId(userId);
        if (seller == null)
            return QueryResult(new InventoryDto());

        var result = await _sellerQueryService.GetSellerInventoryById(inventoryId);

        if (result == null || result.SellerId != seller.Id)
            return QueryResult(new InventoryDto());

        return QueryResult(result);

    }

    [PermissionChecker(Permission.Seller_Panel)]
    [HttpGet("GetAllSellerInventory")]
    public async Task<ApiResult<List<InventoryDto?>>> GetAllSellerInventory()
    {
        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var seller = await _sellerQueryService.GetSellerByUserId(userId);
        if (seller == null)
            return QueryResult(new List<InventoryDto>());

        var result = await _sellerQueryService.GetAllInventories(seller.Id);
        return QueryResult(result);
    }
}

