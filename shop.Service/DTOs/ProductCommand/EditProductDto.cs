using Microsoft.AspNetCore.Http;

namespace shop.Service.DTOs.ProductCommand
{
    public class EditProductDto
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string Description { get; set; }
    }
}
