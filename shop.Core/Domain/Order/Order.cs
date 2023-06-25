using shop.Core.Commons;
using shop.Core.Domain.Comment;
using shop.Core.Domain.User;

namespace shop.Core.Domain.Order
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public int? Discount { get;  set; }
        public OrderStatus Status { get; set; }

        //public int OrderStatusId { get; set; }

        //public OrderStatus orderStatus
        //{
        //    get => (OrderStatus)OrderStatusId;
        //    set => OrderStatusId = (int)value;
        //}

        public int OrderTotal
        {
            get
            {
                var totalPrice = OrderItems.Sum(f => f.TotalPrice);
                if (Discount != null)
                    totalPrice -= Discount.Value;

                return totalPrice;
            }
        }


        public virtual OrderAddress Addresses { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual shop.Core.Domain.User.User User { get; set; }
    }


    public enum OrderStatus
    {
        //0
        Pending,
        //1
        Processing,
        //2
        Completed,
        //3
        Failed
    }


}