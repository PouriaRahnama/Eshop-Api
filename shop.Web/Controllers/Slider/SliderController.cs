using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.SliderCommand;
using shop.Service.Query;

namespace shop.Web.Controllers.Slider;

public class SliderController : ShopController
{

    private readonly ISliderService _sliderService;
    private readonly SliderQueryService _sliderQueryService;
    public SliderController(ISliderService sliderService, SliderQueryService sliderQueryService)
    {
        _sliderService = sliderService;
        _sliderQueryService = sliderQueryService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ApiResult<List<SliderDto>>> GetList()
    {
        var result = await _sliderQueryService.GetAllSlider();
        return QueryResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<SliderDto?>> GetById(int id)
    {
        var result = await _sliderQueryService.GetSliderById(id);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult> AddSlider([FromForm] AddSliderDto command)
    {
        var result = await _sliderService.AddSlider(command);
        return CreatedResult(result, null);
    }

    [HttpPut]
    public async Task<ApiResult> EditSlider([FromForm] EditSliderDto command)
    {
        var result = await _sliderService.EditSlider(command);
        return CommandResult(result);
    }

    [HttpDelete]
    public async Task<ApiResult> RemoveSlider(RemoveSliderDto command)
    {
        var result = await _sliderService.RemoveSlider(command);
        return CommandResult(result);
    }
}

