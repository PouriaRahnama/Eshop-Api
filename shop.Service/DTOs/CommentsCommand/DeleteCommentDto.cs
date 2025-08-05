using shop.Service.DTOs.CommonsCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Service.DTOs.CommentsCommand
{
    public class DeleteCommentDto : BaseDTO
    {
        public long UserId { get; set; }
    }
}
