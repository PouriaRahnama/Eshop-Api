using shop.Service.DTOs.CommentsCommand;
using shop.Service.Extension.Util;

namespace shop.Service.Command;

public interface ICommentsService
{
    Task<OperationResult> AddComments(CreateCommentDto CreateCommentDto);
    Task<OperationResult> UpdateComments(EditCommentDto EditCommentDto);
    Task<OperationResult> ChangeStatus(ChangeStatusDto ChangeStatusDto);

}

