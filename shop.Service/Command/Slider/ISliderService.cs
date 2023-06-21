using shop.Service.DTOs.SliderCommand;
using shop.Service.Extension.Util;

namespace shop.Service.Command
{
    public interface ISliderService
    {
        Task<OperationResult> AddSlider(AddSliderDto AddSliderDto);
        Task<OperationResult> EditSlider(EditSliderDto EditSliderDto);
        Task<OperationResult> RemoveSlider(RemoveSliderDto RemoveSliderDto);
    }
}
