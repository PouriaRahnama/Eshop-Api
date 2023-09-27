namespace shop.Service.Query
{
    public interface IProductQueryService
    {
        Task<ProductQueryDto?> GetProductById(int productId);
        Task<ProductFilterResult> GetProductByFilter(ProductFilterParams request);
    }
}
