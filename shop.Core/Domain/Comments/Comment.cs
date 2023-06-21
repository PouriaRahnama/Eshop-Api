using shop.Core.Commons;
using shop.Core.Domain.Order;

namespace shop.Core.Domain.Comment
{
    public class Comment:BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; }
        public int StatusId { get; set; }
        public CommentStatus Status
        {
            get => (CommentStatus)StatusId;
            set => StatusId = (int)value;
        }
        public virtual shop.Core.Domain.Product.Product Product { get; set; }
        public virtual shop.Core.Domain.User.User User { get; set; }

    }

    public enum CommentStatus
    {
        Pennding = 10,
        Accepted = 20,
        Rejected = 30
    }
}