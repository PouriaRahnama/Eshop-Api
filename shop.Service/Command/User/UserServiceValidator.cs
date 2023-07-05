using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;
using shop.Service.DTOs.UserCommand;

namespace shop.Service.Command
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(r => r.PhoneNumber)
                .ValidPhoneNumber();

            RuleFor(r => r.Email)
                .EmailAddress().WithMessage("ایمیل نامعتبر است");

            RuleFor(f => f.Password)
                .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور"))
                .NotNull().WithMessage(ValidationMessages.required("کلمه عبور"))
                .MinimumLength(4).WithMessage("کلمه عبور باید بشتر از 4 کارکتر باشد");
        }
    }

    public class EditUserCommandValidator : AbstractValidator<EditUserDto>
    {
        public EditUserCommandValidator()
        {
            RuleFor(r => r.PhoneNumber)
                .ValidPhoneNumber();

            RuleFor(r => r.Email)
                .EmailAddress().WithMessage("ایمیل نامعتبر است");

            RuleFor(f => f.Password)
                .MinimumLength(4).WithMessage("کلمه عبور باید بشتر از 4 کارکتر باشد");

            RuleFor(f => f.Avatar)
                .JustImageFile();
        }
    }

    public class AddUserAddressCommandValidator : AbstractValidator<AddUserAddressDto>
    {
        public AddUserAddressCommandValidator()
        {
            RuleFor(f => f.City)
                .NotEmpty().WithMessage(ValidationMessages.required("شهر"));

            RuleFor(f => f.Shire)
                .NotEmpty().WithMessage(ValidationMessages.required("استان"));

            RuleFor(f => f.Name)
                .NotEmpty().WithMessage(ValidationMessages.required("نام"));

            RuleFor(f => f.Family)
                .NotEmpty().WithMessage(ValidationMessages.required("نام خانوادگی"));

            RuleFor(f => f.NationalCode)
                .NotEmpty().WithMessage(ValidationMessages.required("کدملی"))
                .ValidNationalId();

            RuleFor(f => f.PostalAddress)
                .NotEmpty().WithMessage(ValidationMessages.required("آدرس پستی"));

            RuleFor(f => f.PostalCode)
                .NotEmpty().WithMessage(ValidationMessages.required("کد پستی"));
        }
    }
}
