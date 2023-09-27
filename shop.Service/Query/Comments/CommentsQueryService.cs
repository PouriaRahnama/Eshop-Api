﻿using Microsoft.EntityFrameworkCore;
using shop.Core.Domain.Comment;
using shop.Data.ApplicationContext;

namespace shop.Service.Query
{
    public class CommentsQueryService: ICommentsQueryService
    {
        private readonly IApplicationContext _context;
        public CommentsQueryService(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<CommentFilterResult> GetCommentByFilter(CommentFilterParams request)
        {
            var @params = request;

            var result = _context.Set<Comment>().OrderByDescending(d => d.CreateON).AsQueryable();

            if (@params.CommentStatus != null)
                result = result.Where(r => r.Status == @params.CommentStatus);

            if (@params.ProductId != null)
                result = result.Where(r => r.ProductId == @params.ProductId);

            if (@params.UserId != null)
                result = result.Where(r => r.UserId == @params.UserId);

            if (@params.StartDate != null)
                result = result.Where(r => r.CreateON.Date >= @params.StartDate.Value.Date);

            if (@params.EndDate != null)
                result = result.Where(r => r.CreateON.Date <= @params.EndDate.Value.Date);

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new CommentFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take)
                    .Select(comment => comment.MapFilterComment())
                    .ToListAsync(),
                FilterParams = @params
            };
            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }

        public async Task<CommentsQueryDto?> GetCommentById(int CommentId)
        {
            var comment = await _context.Set<Comment>().FirstOrDefaultAsync(f => f.Id == CommentId);

            return comment.Map();
        }
    }
}
