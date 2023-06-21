using shop.Service.DTOs.CategoryCommand;
using shop.Service.DTOs.SellerCommand;
using shop.Service.Extension.Util;

namespace shop.Service.Command
{
    public interface ISellerService
    {
        Task<OperationResult> AddInventory(AddInventoryDto AddInventoryDto);
        Task<OperationResult> AddSeller(AddSellerDto AddSellerDto);
        Task<OperationResult> UpdateInventory(EditInventoryDto EditInventoryDto);
        Task<OperationResult> UpdateSeller(EditSellerDto EditSellerDto);
        Task<OperationResult> RemoveInventory(RemoveInventoryDto RemoveInventoryDto);

    }
}
