using Microsoft.AspNetCore.Mvc;
using shop.Frameworks;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.SellerCommand;
using shop.Service.Query;

namespace shop.Web.Controllers.Seller
{
    public class SellerController : ShopController
    {
        private readonly ISellerService _sellerService;
        private readonly SellerQueryService _sellerQueryService;
        public SellerController(ISellerService sellerService, SellerQueryService sellerQueryService)
        {
            _sellerService = sellerService;
            _sellerQueryService = sellerQueryService;
        }

        [HttpGet("GetSellerByFilter*")]
        public async Task<ApiResult<SellerFilterResult>> GetSellerByFilter([FromQuery]SellerFilterParams filterParams)
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

        [HttpPost("AddSeller*")]
        public async Task<ApiResult> AddSeller(AddSellerDto command)
        {
            var result = await _sellerService.AddSeller(command);
            return CommandResult(result);
        }

        [HttpPut("UpdateSeller*")]
        public async Task<ApiResult> UpdateSeller([FromForm]EditSellerDto command)
        {
            var result = await _sellerService.UpdateSeller(command);
            return CommandResult(result);
        }

        [HttpPost("AddInventory*")]
        public async Task<ApiResult> AddInventory(AddInventoryDto command)
        {
            var result = await _sellerService.AddInventory(command);
            return CommandResult(result);
        }

        [HttpPut("UpdateInventory*")]
        public async Task<ApiResult> EditInventory(EditInventoryDto command)
        {
            var result = await _sellerService.UpdateInventory(command);
            return CommandResult(result);
        }

        [HttpDelete("RemoveInventory*")]
        public async Task<ApiResult> RemoveInventory(RemoveInventoryDto command)
        {
            var result = await _sellerService.RemoveInventory(command);
            return CommandResult(result);
        }
    }
}
