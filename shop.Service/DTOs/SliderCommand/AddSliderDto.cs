using Microsoft.AspNetCore.Http;

namespace shop.Service.DTOs.SliderCommand
{
    public class AddSliderDto
    {
        public string Link { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Title { get; set; }
    }
}
