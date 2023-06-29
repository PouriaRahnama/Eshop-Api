using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shop.Core.Domain.Role;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.CommentsCommand;
using shop.Service.Query.Comments;
using Shop.Api.Infrastructure.JwtUtil;

namespace shop.Web.Controllers.Comments;

public class CommentsController : ShopController
{
    private readonly ICommentsService _commentsService;
    private readonly CommentsQueryService _commentsQueryService;
    public CommentsController(ICommentsService commentsService, CommentsQueryService commentsQueryService)
    {
        _commentsService = commentsService;
        _commentsQueryService = commentsQueryService;
    }

    [PermissionChecker(Permission.Comment_Management)]
    [HttpGet("GetCommentByFilter*")]
    public async Task<ApiResult<CommentFilterResult>> GetCommentByFilter([FromQuery] CommentFilterParams filterParams)
    {
        var result = await _commentsQueryService.GetCommentByFilter(filterParams);
        return QueryResult(result);
    }

    [PermissionChecker(Permission.Comment_Management)]
    [HttpGet("{commentId}*")]
    public async Task<ApiResult<CommentsQueryDto?>> GetCommentById(int commentId)
    {
        var result = await _commentsQueryService.GetCommentById(commentId);
        return QueryResult(result);
    }

    [Authorize]
    [HttpPut("EditComment*")]
    public async Task<ApiResult> EditComment(EditCommentDto command)
    {
        var result = await _commentsService.UpdateComments(command);
        return CommandResult(result);
    }

    [Authorize]
    [HttpPost("AddComment*")]
    public async Task<ApiResult> AddComment(CreateCommentDto command)
    {
        var result = await _commentsService.AddComments(command);
        return CreatedResult(result, null);
    }

    [PermissionChecker(Permission.Comment_Management)]
    [HttpPut("changeStatus*")]
    public async Task<ApiResult> ChangeCommentStatus([FromForm] ChangeStatusDto command)
    {
        var result = await _commentsService.ChangeStatus(command);
        return CommandResult(result);
    }
}

