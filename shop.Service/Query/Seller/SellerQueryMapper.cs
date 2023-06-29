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

        public static InventoryDto InventoryMap(this SellerInventory Inventory)
        {
            if (Inventory == null)
                return null;

            return new InventoryDto()
            {
                Id = Inventory.Id,
                CreationDate = Inventory.CreateON,
                Count = Inventory.Count,
                Price = Inventory.Price,
                ShopName = Inventory.Seller.ShopName,
                ProductId = Inventory.ProductId,
                ProductImage = Inventory.Product.ImageName,
                ProductTitle = Inventory.Product.Name,
                SellerId = Inventory.SellerId,
                DiscountPercentage = Inventory.DiscountPercentage
            };
        }



        public static List<InventoryDto> GetAllInventoryMap(this List<SellerInventory> Inventories)
        {
            if (Inventories == null)
                return null;

            List<InventoryDto> List = new List<InventoryDto>();

            foreach (var Inventory in Inventories)
            {
                List.Add(new InventoryDto()
                {
                    Id = Inventory.Id,
                    CreationDate = Inventory.CreateON,
                    Count = Inventory.Count,
                    Price = Inventory.Price,
                    ShopName = Inventory.Seller.ShopName,
                    ProductId = Inventory.ProductId,
                    ProductImage = Inventory.Product.ImageName,
                    ProductTitle = Inventory.Product.Name,
                    SellerId = Inventory.Seller.Id,
                    DiscountPercentage = Inventory.DiscountPercentage
                });
            }
            return List;
        }





    }
}
