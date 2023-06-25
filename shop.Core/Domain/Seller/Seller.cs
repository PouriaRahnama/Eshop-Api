using shop.Core.Commons;
using shop.Core.Domain.Order;

namespace shop.Core.Domain.Seller
{
    public class Seller : BaseEntity
    {
        public int UserId { get; set; }
        public string ShopName { get; set; }
        public string NationalCode { get; set; }
        public SellerStatus Status { get; set; }

        //public int SellerStatusId { get; set; }
        //public SellerStatus SellerStatus
        //{
        //    get => (SellerStatus)SellerStatusId;
        //    set => SellerStatusId = (int)value;
        //}
        public virtual ICollection<SellerInventory> Inventories { get; set; }
        public virtual shop.Core.Domain.User.User User { get; set; }

    }
}