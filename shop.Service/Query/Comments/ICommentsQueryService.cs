namespace shop.Service.Query
{
    public interface ICommentsQueryService
    {
        Task<CommentFilterResult> GetCommentByFilter(CommentFilterParams request);
        Task<CommentsQueryDto?> GetCommentById(int CommentId);
    }
}
