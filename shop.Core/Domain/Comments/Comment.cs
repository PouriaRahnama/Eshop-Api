using shop.Core.Commons;
using shop.Core.Domain.Order;

namespace shop.Core.Domain.Comment
{
    public class Comment:BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public string Text { get; set; }
        public CommentStatus Status { get; set; }
        //public int StatusId { get; set; }
        //public CommentStatus Status
        //{
        //    get => (CommentStatus)StatusId;
        //    set => StatusId = (int)value;
        //}
        public virtual shop.Core.Domain.Product.Product Product { get; set; }
        public virtual shop.Core.Domain.User.User User { get; set; }

    }

    public enum CommentStatus
    {
        //0
        Pennding,
        //1
        Accepted,
        //2
        Rejected
    }
}