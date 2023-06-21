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
        //var Comment = _repository.Table.FirstOrDefault(c => c.Id == EditCommentDto.Id);
        var Comment = await _repository.FindByIdAsync(EditCommentDto.Id);

        if (Comment == null || Comment.UserId != EditCommentDto.UserId)
            return OperationResult.NotFound();

        Comment.Text = EditCommentDto.Text;

        await _repository.UpdateAsync(Comment);
        return OperationResult.Success();
    }
    public async Task<OperationResult> ChangeStatus(ChangeStatusDto ChangeStatusDto)
    {
        //var Comment = _repository.Table.FirstOrDefault(c => c.Id == ChangeStatusDto.Id);
        var Comment = await _repository.FindByIdAsync(ChangeStatusDto.Id);
        if (Comment == null)
            return OperationResult.NotFound();

        Comment.Status = ChangeStatusDto.Status;

        await _repository.UpdateAsync(Comment);
        return OperationResult.Success();
    }
}
