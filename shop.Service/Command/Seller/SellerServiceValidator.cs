using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;
using shop.Service.DTOs.SellerCommand;

namespace shop.Service.Command
{
    public class CreateSellerCommandValidator : AbstractValidator<AddSellerDto>
    {
        public CreateSellerCommandValidator()
        {
            RuleFor(r => r.ShopName)
                .NotEmpty().WithMessage(ValidationMessages.required("نام فروشگاه"));

            RuleFor(r => r.ShopName)
                .NotEmpty().WithMessage(ValidationMessages.required("کدملی"))
                .ValidNationalId();
        }
    }
    public class EditSellerCommandValidator : AbstractValidator<EditSellerDto>
    {
        public EditSellerCommandValidator()
        {
            RuleFor(r => r.ShopName)
                .NotEmpty().WithMessage(ValidationMessages.required("نام فروشگاه"));

            RuleFor(r => r.ShopName)
                .NotEmpty().WithMessage(ValidationMessages.required("کدملی"))
                .ValidNationalId();
        }
    }
}
