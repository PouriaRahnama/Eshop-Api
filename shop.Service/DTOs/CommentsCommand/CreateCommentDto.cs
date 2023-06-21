using shop.Core.Domain.Comment;

namespace shop.Service.DTOs.CommentsCommand
{
    public class CreateCommentDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; }
    }
}
