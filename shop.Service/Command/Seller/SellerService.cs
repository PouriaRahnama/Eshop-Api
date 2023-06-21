using Azure.Core;
using shop.Core.Domain.Seller;
using shop.Data.Repository;
using shop.Service.DTOs.SellerCommand;
using shop.Service.Extension.Util;

namespace shop.Service.Command
{
    public class SellerService : ISellerService
    {
        private readonly IRepository<Seller> _repository;
        private readonly IRepository<SellerInventory> _SellerInventoryRepository;
        public SellerService(IRepository<Seller> Repository, IRepository<SellerInventory> sellerInventoryRepository)
        {
            _repository = Repository;
            _SellerInventoryRepository = sellerInventoryRepository;
        }

        public async Task<OperationResult> AddInventory(AddInventoryDto AddInventoryDto)
        {
            var seller = await _repository.FindByIdAsync(AddInventoryDto.SellerId);
            if (seller == null || seller.SellerStatus == SellerStatus.Rejected)
                return OperationResult.NotFound();

            var inventory = new SellerInventory()
            {
                Price = AddInventoryDto.Price,
                SellerId = AddInventoryDto.SellerId,
                ProductId = AddInventoryDto.ProductId,
                Count = AddInventoryDto.Count,
                DiscountPercentage = AddInventoryDto.PercentageDiscount
            };

            await _SellerInventoryRepository.AddAsync(inventory);
            return OperationResult.Success();
        }
        public async Task<OperationResult> AddSeller(AddSellerDto AddSellerDto)
        {
            var Seller = await _repository.GetEntity(s => s.NationalCode == AddSellerDto.NationalCode
            || s.UserId == AddSellerDto.UserId);
            if (Seller != null)
                return OperationResult.Error("!فروشنده ای با این کد ملی وجود دارد");


            var seller = new Seller()
            {
                ShopName = AddSellerDto.ShopName,
                NationalCode = AddSellerDto.NationalCode,
                UserId = AddSellerDto.UserId,
                SellerStatus = SellerStatus.New
            };

            await _repository.AddAsync(seller);
            return OperationResult.Success();
        }
        public async Task<OperationResult> RemoveInventory(RemoveInventoryDto RemoveInventoryDto)
        {
            var Inventory = await _SellerInventoryRepository.FindByIdAsync(RemoveInventoryDto.InventoryId);
            if (Inventory == null)
                return OperationResult.NotFound();

            await _SellerInventoryRepository.DeleteAsync(Inventory);
            return OperationResult.Success();
        }
        public async Task<OperationResult> UpdateInventory(EditInventoryDto EditInventoryDto)
        {
            var seller = await _repository.FindByIdAsync(EditInventoryDto.SellerId);
            if (seller == null)
                return OperationResult.NotFound();

            var NewInventory = new SellerInventory()
            {
                Count = EditInventoryDto.Count,
                Price = EditInventoryDto.Price,
                DiscountPercentage = EditInventoryDto.DiscountPercentage
            };

            await _SellerInventoryRepository.UpdateAsync(NewInventory);
            return OperationResult.Success();
        }
        public async Task<OperationResult> UpdateSeller(EditSellerDto EditSellerDto)
        {
            var seller = await _repository.FindByIdAsync(EditSellerDto.SellerId);
            if (seller == null)
                return OperationResult.NotFound();

            var NationalCodeExistInDataBase = _repository.GetEntity(r => r.NationalCode == EditSellerDto.NationalCode);

            if (EditSellerDto.NationalCode != seller.NationalCode)
                if (NationalCodeExistInDataBase != null)
                    return OperationResult.Error("کدملی متعلق به شخص دیگری است");

            var NewSeller = new Seller()
            {
                SellerStatus = EditSellerDto.Status,
                NationalCode = EditSellerDto.NationalCode,
                ShopName = EditSellerDto.ShopName,
                UpdateON = DateTime.Now
            };

            await _repository.UpdateAsync(seller);
            return OperationResult.Success();
        }
    }
}
