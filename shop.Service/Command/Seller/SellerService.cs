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
            if (seller == null || seller.Status == SellerStatus.Rejected)
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
            var seller = await _repository.GetEntity(s => s.NationalCode == AddSellerDto.NationalCode
            || s.UserId == AddSellerDto.UserId);

            if (seller == null)
            {
                seller = new Seller()
                {
                    ShopName = AddSellerDto.ShopName,
                    NationalCode = AddSellerDto.NationalCode,
                    UserId = AddSellerDto.UserId,
                    Status = SellerStatus.New
                };
            }
            else
                return OperationResult.Error("!فروشنده ای با این مشخصات وجود دارد");

            await _repository.AddAsync(seller);
            return OperationResult.Success();
        }
        public async Task<OperationResult> RemoveInventory(RemoveInventoryDto RemoveInventoryDto)
        {
            var Inventory = await _SellerInventoryRepository.FindByIdAsync(RemoveInventoryDto.InventoryId);
            if (Inventory == null)
                return OperationResult.NotFound();

            Inventory.Deleted = true;
            _SellerInventoryRepository.Update(Inventory);
            return OperationResult.Success();
        }
        public async Task<OperationResult> UpdateInventory(EditInventoryDto EditInventoryDto)
        {
            var seller = await _repository.FindByIdAsync(EditInventoryDto.SellerId);
            if (seller == null)
                return OperationResult.NotFound();

            var Inventory = await _SellerInventoryRepository.FindByIdAsync(EditInventoryDto.InventoryId);
            if(Inventory == null)
                return OperationResult.NotFound();


            Inventory.Count = EditInventoryDto.Count;
            Inventory.Price = EditInventoryDto.Price;
            Inventory.DiscountPercentage = EditInventoryDto.DiscountPercentage;
            Inventory.UpdateON = DateTime.Now;

            _SellerInventoryRepository.Update(Inventory);
            return OperationResult.Success();
        }
        public async Task<OperationResult> UpdateSeller(EditSellerDto EditSellerDto)
        {
            var seller = await _repository.FindByIdAsync(EditSellerDto.SellerId);
            if (seller == null)
                return OperationResult.NotFound();

            var NationalCodeExistInDataBase = _repository.Table.Where(s => s.NationalCode == EditSellerDto.NationalCode).FirstOrDefault();
            //var NationalCodeExistInDataBase2 = _repository.Get(s => s.NationalCode == EditSellerDto.NationalCode).Count();

            if (EditSellerDto.NationalCode != seller.NationalCode)
                if (NationalCodeExistInDataBase != null)
                    return OperationResult.Error("کدملی متعلق به شخص دیگری است");


            seller.Status = EditSellerDto.Status;
            seller.NationalCode = EditSellerDto.NationalCode;
            seller.ShopName = EditSellerDto.ShopName;
            seller.UpdateON = DateTime.Now;



            _repository.Update(seller);
            return OperationResult.Success();
        }
    }
}
