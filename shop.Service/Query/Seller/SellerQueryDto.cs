using shop.Core.Domain.Seller;
using shop.Service.Extension.Util;

namespace shop.Service.Query
{
    public class SingleProductDto
    {
        public ProductQueryDto ProductDto { get; set; }
        public List<InventoryDto> Inventories { get; set; }
    }

    public class SellerDto : BaseDto
    {
        public int UserId { get; set; }
        public string ShopName { get; set; }
        public string NationalCode { get; set; }
        public SellerStatus Status { get; set; }
    }

    public class InventoryDto : BaseDto
    {
        public int SellerId { get; set; }
        public string ShopName { get; set; }
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductImage { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int? DiscountPercentage { get; set; }
    }

    public class SellerFilterParams : BaseFilterParam
    {
        public string? ShopName { get; set; }
        public string? NationalCode { get; set; }
    }

    public class SellerFilterResult : BaseFilter<SellerDto, SellerFilterParams>
    {

    }
}
