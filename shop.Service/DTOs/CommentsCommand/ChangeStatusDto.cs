using shop.Core.Domain.Comment;
using shop.Service.DTOs.CommonsCommand;

namespace shop.Service.DTOs.CommentsCommand
{
    public class ChangeStatusDto : BaseDTO
    {
        public CommentStatus Status { get; set; }
    }
}
