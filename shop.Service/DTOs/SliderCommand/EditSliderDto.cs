using Microsoft.AspNetCore.Http;

namespace shop.Service.DTOs.SliderCommand
{
    public class EditSliderDto
    {
        public int SliderId { get; set; }
        public string Link { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }
}
