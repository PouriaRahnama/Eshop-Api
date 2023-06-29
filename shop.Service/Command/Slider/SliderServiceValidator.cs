using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;
using shop.Service.DTOs.SliderCommand;


namespace shop.Service.Command
{
    public class AddSliderCommandValidator : AbstractValidator<AddSliderDto>
    {
        public AddSliderCommandValidator()
        {
            RuleFor(r => r.ImageFile)
                .NotNull().WithMessage(ValidationMessages.required("عکس"))
                .JustImageFile();

            RuleFor(r => r.Link)
                .NotNull()
                .NotEmpty().WithMessage(ValidationMessages.required("لینک"));

            RuleFor(r => r.Title)
                .NotNull()
                .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));
        }
    }

    public class EditSliderCommandValidator : AbstractValidator<EditSliderDto>
    {
        public EditSliderCommandValidator()
        {
            RuleFor(r => r.ImageFile)
                .JustImageFile();

            RuleFor(r => r.Link)
                .NotNull()
                .NotEmpty().WithMessage(ValidationMessages.required("لینک"));

            RuleFor(r => r.Title)
                .NotNull()
                .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));
        }
    }
}
