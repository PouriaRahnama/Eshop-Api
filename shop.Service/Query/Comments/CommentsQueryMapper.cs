using shop.Core.Domain.Comment;

namespace shop.Service.Query.Comments
{
    public static class CommentsQueryMapper
    {
        public static CommentsQueryDto? Map(this Comment? comment)
        {
            if (comment == null)
                return null;
            return new CommentsQueryDto()
            {
                Id = comment.Id,
                CreationDate = comment.CreateON,
                Status = comment.Status,
                UserId = comment.UserId,
                ProductId = comment.ProductId,
                Text = comment.Text,
                ProductName = comment.Product.Name,
                UserFullName = comment.User.Name + comment.User.Family

            };
        }
        public static CommentsQueryDto MapFilterComment(this Comment comment)
        {
            if (comment == null)
                return null;
            return new CommentsQueryDto()
            {
                Id = comment.Id,
                CreationDate = comment.CreateON,
                Status = comment.Status,
                UserId = comment.UserId,
                ProductId = comment.ProductId,
                Text = comment.Text,
                ProductName = comment.Product.Name,
                UserFullName = comment.User.Name + comment.User.Family
            };

        }
    }
}
