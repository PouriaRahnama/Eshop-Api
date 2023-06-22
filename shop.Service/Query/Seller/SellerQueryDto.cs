using shop.Core.Domain.Seller;
using shop.Service.Extension.Util;
using shop.Service.Query.Commons;

namespace shop.Service.Query
{
    public class SellerDto : BaseDto
    {
        public int UserId { get; set; }
        public string ShopName { get; set; }
        public string NationalCode { get; set; }
        public SellerStatus Status { get; set; }
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
