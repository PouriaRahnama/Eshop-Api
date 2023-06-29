using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;
using shop.Service.DTOs.ProductCommand;


namespace shop.Service.Command
{
    public class AddProductSpecificationCommandValidator : AbstractValidator<AddProductSpecificationDto>
    {
        public AddProductSpecificationCommandValidator()
        {
            RuleFor(b => b.Value)
                .NotNull().WithMessage(ValidationMessages.required("مقدار"));

            RuleFor(b => b.Name)
                .NotEmpty().WithMessage(ValidationMessages.required("نام"));
        }
    }

    public class CreateProductCommandValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(r => r.Title)
                .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

            RuleFor(r => r.Description)
               .NotEmpty().WithMessage(ValidationMessages.required("توضیحات"));

            RuleFor(r => r.ImageFile)
               .JustImageFile();
        }
    }

    public class CreatePictureCommandValidator : AbstractValidator<CreatePictureDto>
    {
        public CreatePictureCommandValidator()
        {
            RuleFor(b => b.ImageFile)
                .NotNull().WithMessage(ValidationMessages.required("عکس"))
                .JustImageFile();
        }
    }

    public class EditProductCommandValidator : AbstractValidator<EditProductDto>
    {
        public EditProductCommandValidator()
        {

            RuleFor(r => r.Title)
                .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

            RuleFor(r => r.Description)
               .NotEmpty().WithMessage(ValidationMessages.required("توضیحات"));

            RuleFor(r => r.ImageFile)
               .JustImageFile();
        }
    }
}
