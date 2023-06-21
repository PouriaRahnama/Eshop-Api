using shop.Core.Commons;

namespace shop.Core.Domain.Slider
{
    public class Slider : BaseEntity
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public bool IsActive { get; set; }
        public string ImageName { get; set; }

    }
}