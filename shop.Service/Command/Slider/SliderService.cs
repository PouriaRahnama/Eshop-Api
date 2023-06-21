
using shop.Core.Domain.Slider;
using shop.Data.Repository;
using shop.Service.DTOs.SliderCommand;
using shop.Service.Extension.FileUtil.Interfaces;
using shop.Service.Extension.Util;

namespace shop.Service.Command
{
    public class SliderService : ISliderService
    {
        private readonly IRepository<Slider> _repository;
        private readonly IFileService _fileService;
        public SliderService(IRepository<Slider> Repository, IFileService fileService)
        {
            _repository = Repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> AddSlider(AddSliderDto AddSliderDto)
        {
            var imageName = await _fileService
                .SaveFileAndGenerateName(AddSliderDto.ImageFile, Directories.SliderImages);
            var slider = new Slider()
            {
                ImageName = imageName,
                Link = AddSliderDto.Link,
                Title = AddSliderDto.Title,
                IsActive = false
            };

            await _repository.AddAsync(slider);
            return OperationResult.Success();
        }
        public async Task<OperationResult> EditSlider(EditSliderDto EditSliderDto)
        {
            var slider = await _repository.FindByIdAsync(EditSliderDto.SliderId);
            if (slider == null)
                return OperationResult.NotFound();

            var imageName = slider.ImageName;
            var oldImage = slider.ImageName;

            if (EditSliderDto.ImageFile != null)
                imageName = await _fileService
                    .SaveFileAndGenerateName(EditSliderDto.ImageFile, Directories.SliderImages);

            var Newslider = new Slider()
            {
                Link = EditSliderDto.Link,
                Title = EditSliderDto.Title,
                IsActive = EditSliderDto.IsActive,
                ImageName = imageName,
                UpdateON = DateTime.Now
            };

            await _repository.UpdateAsync(Newslider);

            if (EditSliderDto.ImageFile != null)
                _fileService.DeleteFile(Directories.SliderImages, oldImage);

            return OperationResult.Success();
        }
        public async Task<OperationResult> RemoveSlider(RemoveSliderDto RemoveSliderDto)
        {
            var slider = await _repository.FindByIdAsync(RemoveSliderDto.SliderId);
            if (slider == null)
                return OperationResult.NotFound();

            await _repository.DeleteAsync(slider);

            if (slider.ImageName != null)
                _fileService.DeleteFile(Directories.SliderImages, slider.ImageName);

            return OperationResult.Success();
        }
    }
}
