using Common.Application.Validation;
using FluentValidation;
using shop.Service.DTOs.RoleCommand;

namespace shop.Service.Command
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleDto>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(r => r.Title)
                .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));
        }
    }

    public class EditRoleCommandCommandValidator : AbstractValidator<EditRoleDto>
    {
        public EditRoleCommandCommandValidator()
        {
            RuleFor(r => r.Title)
                .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));
        }
    }
}
