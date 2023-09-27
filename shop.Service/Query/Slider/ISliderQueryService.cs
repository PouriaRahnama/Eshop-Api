namespace shop.Service.Query
{
    public interface ISliderQueryService
    {
        Task<SliderDto> GetSliderById(int SliderId);
        Task<List<SliderDto>> GetAllSlider();
    }
}
