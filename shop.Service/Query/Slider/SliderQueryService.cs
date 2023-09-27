using Microsoft.EntityFrameworkCore;
using shop.Core.Domain.Slider;
using shop.Data.ApplicationContext;


namespace shop.Service.Query
{
    public class SliderQueryService : ISliderQueryService
    {
        private readonly IApplicationContext _context;

        public SliderQueryService(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<SliderDto> GetSliderById(int SliderId)
        {
            var slider = await _context.Set<Slider>()
                .FirstOrDefaultAsync(f => f.Id == SliderId);
            if (slider == null)
                return null;

            return new SliderDto()
            {
                Id = slider.Id,
                CreationDate = slider.CreateON,
                ImageName = slider.ImageName,
                Link = slider.Link,
                Title = slider.Title
            };
        }

        public async Task<List<SliderDto>> GetAllSlider()
        {
            return await _context.Set<Slider>().OrderByDescending(d => d.Id)
                .Select(slider => new SliderDto()
                {
                    Id = slider.Id,
                    CreationDate = slider.CreateON,
                    ImageName = slider.ImageName,
                    Link = slider.Link,
                    Title = slider.Title
                }).ToListAsync();
        }
    }
}
