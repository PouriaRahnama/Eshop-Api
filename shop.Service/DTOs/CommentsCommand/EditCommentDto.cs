using shop.Service.DTOs.CommonsCommand;

namespace shop.Service.DTOs.CommentsCommand
{
    public class EditCommentDto : BaseDTO
    {
        public int UserId { get; set; }
        public string Text { get; set; }

    }
}
