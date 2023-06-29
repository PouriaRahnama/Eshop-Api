using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shop.Core.Caching;
using shop.Core.Domain.Role;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.ProductCommand;
using shop.Service.Query;
using Shop.Api.Infrastructure.JwtUtil;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.GetForShop;

namespace shop.Web.Controllers.Product;

[PermissionChecker(Permission.CRUD_Product)]
public class ProductController : ShopController
{
    private readonly IProductService _productService;
    private readonly ProductQueryService _productQueryService;
    private readonly GetProductsForShopQuery _getProductsForShopQuery;
    private readonly ICacheManager _cacheManager;
    public ProductController(IProductService productService,
        ProductQueryService productQueryService,
        ICacheManager cacheManager,
        GetProductsForShopQuery getProductsForShopQuery)
    {
        _productService = productService;
        _productQueryService = productQueryService;
        _cacheManager = cacheManager;
        _getProductsForShopQuery = getProductsForShopQuery;
    }

    [HttpPost("AddProductCategory*")]
    public async Task<ApiResult> AddProductCategory(AddProductCategoryDto command)
    {
        var result = await _productService.AddProductCategory(command);
        return CreatedResult(result, null);
    }

    [HttpPost("AddProductPicture*")]
    public async Task<ApiResult> AddProductPicture(AddProductPictureDto command)
    {
        var result = await _productService.AddProductPicture(command);
        return CommandResult(result);
    }

    [HttpPost("AddProductSpecification*")]
    public async Task<ApiResult> AddProductSpecification(AddProductSpecificationDto command)
    {
        var result = await _productService.AddProductSpecification(command);
        return CommandResult(result);
    }

    [HttpPost("AddPicture*")]
    public async Task<ApiResult> AddPicture([FromForm] CreatePictureDto command)
    {
        var result = await _productService.AddPicture(command);
        return CommandResult(result);
    }

    [HttpPost("AddProduct*")]
    public async Task<ApiResult> AddProduct([FromForm] CreateProductDto command)
    {
        var result = await _productService.AddProduct(command);
        return CommandResult(result);
    }

    [HttpGet("{productId}*")]
    public async Task<ApiResult<ProductQueryDto?>> GetProductById(int productId)
    {
        var product = await _productQueryService.GetProductById(productId);
        await _cacheManager.GetAsync("GetProductById", 60, async () => _productQueryService.GetProductById(productId).Result);
        return QueryResult(product);
    }

    [HttpPut("UpdateProduct*")]
    public async Task<ApiResult> UpdateProduct([FromForm] EditProductDto command)
    {
        var result = await _productService.UpdateProduct(command);
        return CommandResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetProductByFilter*")]
    public async Task<ApiResult<ProductFilterResult>> GetProductByFilter([FromQuery] ProductFilterParams filterParams)
    {
        return QueryResult(await _productQueryService.GetProductByFilter(filterParams));
    }

    [HttpDelete("RemovePicture*")]
    public async Task<ApiResult> RemovePicture([FromForm] RemovePictureDto command)
    {
        var result = await _productService.RemovePicture(command);
        return CommandResult(result);
    }

    [HttpDelete("RemoveProductCategory*")]
    public async Task<ApiResult> RemoveProductCategory(RemoveProductCategoryDto command)
    {
        var result = await _productService.RemoveProductCategory(command);
        return CommandResult(result);
    }

    [HttpDelete("RemoveProductPicture*")]
    public async Task<ApiResult> RemoveProductPicture(RemoveProductPictureDto command)
    {
        var result = await _productService.RemoveProductPicture(command);
        return CommandResult(result);
    }

    [HttpDelete("RemoveProductSpecification*")]
    public async Task<ApiResult> RemoveProductSpecification(RemoveProductSpecificationDto command)
    {
        var result = await _productService.RemoveProductSpecification(command);
        return CommandResult(result);
    }

    [AllowAnonymous]
    [HttpGet("Shop")]
    public async Task<ApiResult<ProductShopResult>> GetProductForShopFilter([FromQuery] ProductShopFilterParam filterParams)
    {
        return QueryResult(await _getProductsForShopQuery.Handle(filterParams));
    }
}

