using shop.Core.Domain.Seller;

namespace shop.Service.Query
{
    public static class SellerQueryMapper
    {
        public static SellerDto Map(this Seller seller)
        {
            if (seller == null)
                return null;

            return new SellerDto()
            {
                Id = seller.Id,
                CreationDate = seller.CreateON,
                Status = seller.Status,
                NationalCode = seller.NationalCode,
                ShopName = seller.ShopName,
                UserId = seller.UserId
            };
        }
    }
}
