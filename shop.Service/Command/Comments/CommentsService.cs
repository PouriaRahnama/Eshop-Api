using shop.Core.Domain.Category;
using shop.Core.Domain.Comment;
using shop.Data.Repository;
using shop.Service.DTOs.CommentsCommand;
using shop.Service.Extension.Util;

namespace shop.Service.Command;

public class CommentsService : ICommentsService
{
    private readonly IRepository<Comment> _repository;
    public CommentsService(IRepository<Comment> Repository)
    {
        _repository = Repository;
    }

    public async Task<OperationResult> AddComments(CreateCommentDto CreateCommentDto)
    {
        var comment = new Comment()
        {
            UserId = CreateCommentDto.UserId,
            ProductId = CreateCommentDto.ProductId,
            Text = CreateCommentDto.Text,
            Status = CommentStatus.Pennding
        };
        await _repository.AddAsync(comment);
        return OperationResult.Success();
    }
    public async Task<OperationResult> UpdateComments(EditCommentDto EditCommentDto)
    {
        var Comment = await _repository.FindByIdAsync(EditCommentDto.Id);

        if (Comment == null || Comment.UserId != EditCommentDto.UserId)
            return OperationResult.NotFound();

        Comment.Text = EditCommentDto.Text;
        Comment.UpdateON = DateTime.Now;

        _repository.Update(Comment);
        return OperationResult.Success();
    }
    public async Task<OperationResult> ChangeStatus(ChangeStatusDto ChangeStatusDto)
    {
        var Comment = await _repository.FindByIdAsync(ChangeStatusDto.Id);
        if (Comment == null)
            return OperationResult.NotFound();

        Comment.Status = ChangeStatusDto.Status;

        _repository.Update(Comment);
        return OperationResult.Success();
    }

    //new 
    public async Task<OperationResult> DeleteComment(DeleteCommentDto deleteCommentDto)
    {
        var comment = await _repository.FindByIdAsync(deleteCommentDto.Id);
        if (comment == null || comment.UserId != deleteCommentDto.UserId)
            return OperationResult.NotFound();

        _repository.Delete(comment);
        return OperationResult.Success();
    }
}
