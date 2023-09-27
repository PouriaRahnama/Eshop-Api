namespace shop.Service.Query.Product.GetForShop
{
    public interface IGetProductsForShopQuery
    {
        Task<ProductShopResult> Handle(ProductShopFilterParam request);
    }
}
