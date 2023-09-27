using Microsoft.EntityFrameworkCore;
using shop.Core.Domain.Seller;
using shop.Data.ApplicationContext;

namespace shop.Service.Query
{
    public class SellerQueryService : ISellerQueryService
    {
        private readonly IApplicationContext _Context;

        public SellerQueryService(IApplicationContext Context)
        {
            _Context = Context;
        }

        public async Task<SellerDto?> GetSellerById(int Id)
        {
            var seller = await _Context.Set<Seller>().FirstOrDefaultAsync(f => f.Id == Id);
            return seller.Map();
        }

        public async Task<SellerFilterResult> GetSellerByFilter(SellerFilterParams filterParams)
        {
            var @params = filterParams;
            var result = _Context.Set<Seller>().OrderByDescending(d => d.Id).AsQueryable();

            if (!string.IsNullOrWhiteSpace(@params.NationalCode))
                result = result.Where(r => r.NationalCode.Contains(@params.NationalCode));

            if (!string.IsNullOrWhiteSpace(@params.ShopName))
                result = result.Where(r => r.ShopName.Contains(@params.ShopName));

            var skip = (@params.PageId - 1) * @params.Take;

            var sellerResult = new SellerFilterResult()
            {
                FilterParams = @params,
                Data = await result.Skip(skip).Take(@params.Take)
                    .Select(seller => seller.Map())
                    .ToListAsync()
            };

            sellerResult.GeneratePaging(result, @params.Take, @params.PageId);
            return sellerResult;
        }

        public async Task<SellerDto?> GetSellerByUserId(int UserId)
        {
            var seller = await _Context.Set<Seller>().FirstOrDefaultAsync(f => f.UserId == UserId);
            return seller.Map();
        }

        public async Task<InventoryDto?> GetSellerInventoryById(int InventoryId)
        {
            var inventory = await _Context.Set<SellerInventory>().FirstOrDefaultAsync(i => i.Id == InventoryId);

            return inventory.InventoryMap();
        }

        public async Task<List<InventoryDto>> GetAllInventories(int SellerId)
        {
            var inventory = await _Context.Set<SellerInventory>().Where(i => i.SellerId == SellerId).ToListAsync();

            return inventory.GetAllInventoryMap();
        }

        public async Task<List<InventoryDto>> GetInventoriesByProductId(int ProductId)
        {
            var inventory = await _Context.Set<SellerInventory>().Where(i => i.ProductId == ProductId).ToListAsync();

            return inventory.GetAllInventoryMap();
        }

    }
}
