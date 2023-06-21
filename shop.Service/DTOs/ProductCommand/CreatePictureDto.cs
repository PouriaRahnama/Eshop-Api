using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Service.DTOs.ProductCommand
{
    public class CreatePictureDto
    {
        public IFormFile ImageFile { get; set; }
    }
}
