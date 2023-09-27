namespace shop.Service.Query
{
    public interface ISellerQueryService
    {
        Task<SellerDto?> GetSellerById(int Id);
        Task<SellerFilterResult> GetSellerByFilter(SellerFilterParams filterParams);
        Task<SellerDto?> GetSellerByUserId(int UserId);
        Task<InventoryDto?> GetSellerInventoryById(int InventoryId);
        Task<List<InventoryDto>> GetAllInventories(int SellerId);
        Task<List<InventoryDto>> GetInventoriesByProductId(int ProductId);
    }
}
