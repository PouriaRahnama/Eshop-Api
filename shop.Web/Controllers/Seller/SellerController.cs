using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shop.Core.Domain.Role;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.SellerCommand;
using shop.Service.Query;
using shop.Web.Infrastructure;
using Shop.Api.Infrastructure.JwtUtil;
using System.Security.Claims;

namespace shop.Web.Controllers.Seller;

public class SellerController : ShopController
{
    private readonly ISellerService _sellerService;
    private readonly SellerQueryService _sellerQueryService;
    private readonly ProductQueryService _productQueryService;
    public SellerController(ISellerService sellerService, SellerQueryService sellerQueryService, ProductQueryService productQueryService)
    {
        _sellerService = sellerService;
        _sellerQueryService = sellerQueryService;
        _productQueryService = productQueryService;
    }

    [PermissionChecker(Permission.Seller_Management)]
    [HttpGet("SellerFilter")]
    public async Task<ApiResult<SellerFilterResult>> GetSellerByFilter([FromQuery] SellerFilterParams filterParams)
    {
        var result = await _sellerQueryService.GetSellerByFilter(filterParams);
        return QueryResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<SellerDto?>> GetSellerById(int id)
    {
        var result = await _sellerQueryService.GetSellerById(id);
        return QueryResult(result);
    }

    [PermissionChecker(Permission.Seller_Management)]
    [HttpPost]
    public async Task<ApiResult> AddSeller(AddSellerDto command)
    {
        var result = await _sellerService.AddSeller(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.Seller_Management)]
    [HttpPut]
    public async Task<ApiResult> UpdateSeller([FromForm] EditSellerDto command)
    {
        var result = await _sellerService.UpdateSeller(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.Add_Inventory)]
    [HttpPost("Inventory")]
    public async Task<ApiResult> AddInventory(AddInventoryDto command)
    {
        var result = await _sellerService.AddInventory(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.Add_Inventory)]
    [HttpPut("Inventory")]
    public async Task<ApiResult> EditInventory(EditInventoryDto command)
    {
        var result = await _sellerService.UpdateInventory(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.Add_Inventory)]
    [HttpDelete("Inventory")]
    public async Task<ApiResult> RemoveInventory(RemoveInventoryDto command)
    {
        var result = await _sellerService.RemoveInventory(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.Seller_Panel)]
    [HttpGet("SellerInventory/{Id}")]
    public async Task<ApiResult<InventoryDto?>> GetSellerInventoryById(int Id)
    {
        var userId = User.GetUserId();
        var seller = await _sellerQueryService.GetSellerByUserId(userId);
        if (seller == null)
            return QueryResult(new InventoryDto());

        var result = await _sellerQueryService.GetSellerInventoryById(Id);

        if (result == null || result.SellerId != seller.Id)
            return QueryResult(new InventoryDto());

        return QueryResult(result);

    }

    [PermissionChecker(Permission.Seller_Panel)]
    [HttpGet("SellerInventory")]
    public async Task<ApiResult<List<InventoryDto?>>> GetAllSellerInventory()
    {
        var userId = User.GetUserId();
        var seller = await _sellerQueryService.GetSellerByUserId(userId);
        if (seller == null)
            return QueryResult(new List<InventoryDto>());

        var result = await _sellerQueryService.GetAllInventories(seller.Id);
        return QueryResult(result);
    }
    
    [AllowAnonymous]
    [HttpGet("Inventory/{ProductId}")]
    public async Task<ApiResult<SingleProductDto?>> GetSellerInventoryByProductId(int ProductId)
    {
        var product = await _productQueryService.GetProductById(ProductId);
        if (product == null)
            return null;

        var inventories = await _sellerQueryService.GetInventoriesByProductId(product.Id);

        var model = new SingleProductDto()
        {
            Inventories = inventories,
            ProductDto = product
        };
        return QueryResult(model);

    }
}

