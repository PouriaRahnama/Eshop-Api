using Common.Application.Validation;
using FluentValidation;
using shop.Service.DTOs.CategoryCommand;

namespace shop.Service.Command
{
    public partial class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotNull().NotEmpty().WithMessage(ValidationMessages.required("عنوان"));
        }
    }

    public partial class EditCategoryCommandValidator : AbstractValidator<EditCategoryDto>
    {
        public EditCategoryCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotNull().NotEmpty().WithMessage(ValidationMessages.required("عنوان"));
        }
    }

    public partial class AddChildCategoryCommandValidator : AbstractValidator<CreateChildCategoryDto>
    {
        public AddChildCategoryCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotNull().NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

            RuleFor(r => r.ParentId)
               .NotNull().NotEmpty().WithMessage(ValidationMessages.required("ParentId"));
        }
    }
}
