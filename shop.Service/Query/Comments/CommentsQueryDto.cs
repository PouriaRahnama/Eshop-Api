﻿using shop.Core.Domain.Comment;
using shop.Service.Extension.Util;

namespace shop.Service.Query
{
    public class CommentsQueryDto: BaseDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string UserFullName { get; set; }
        public string ProductName { get; set; }
        public string Text { get; set; }
        public CommentStatus Status { get; set; }
    }
    public class CommentFilterParams : BaseFilterParam
    {
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public CommentStatus? CommentStatus { get; set; }

    }
    public class CommentFilterResult : BaseFilter<CommentsQueryDto, CommentFilterParams>
    {

    }
}
