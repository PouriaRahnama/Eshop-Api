using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;
using shop.Service.DTOs.OrderCommand;

namespace shop.Service.Command
{
    public class AddOrderItemCommandValidator : AbstractValidator<CreateOrderItemDto>
    {
        public AddOrderItemCommandValidator()
        {
            RuleFor(f => f.Count)
                .GreaterThanOrEqualTo(1).WithMessage("تعداد باید بیشتر از 0 باشد");
        }
    }

    public class AddOrderAddressCommandValidator : AbstractValidator<AddOrderAddressDto>
    {
        public AddOrderAddressCommandValidator()
        {
            RuleFor(f => f.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage(ValidationMessages.required("نام"));

            RuleFor(f => f.Family)
               .NotNull()
               .NotEmpty()
               .WithMessage(ValidationMessages.required("نام خانوادگی"));

            RuleFor(f => f.City)
               .NotNull()
               .NotEmpty()
               .WithMessage(ValidationMessages.required("شهر"));

            RuleFor(f => f.Shire)
               .NotNull()
               .NotEmpty()
               .WithMessage(ValidationMessages.required("استان"));

            RuleFor(f => f.PostalAddress)
              .NotNull()
              .NotEmpty()
              .WithMessage(ValidationMessages.required("استان"));

            RuleFor(f => f.PostalCode)
             .NotNull()
             .NotEmpty()
             .WithMessage(ValidationMessages.required("استان"));

            RuleFor(f => f.PhoneNumber)
              .NotNull()
              .NotEmpty()
              .WithMessage(ValidationMessages.required("شماره"))
              .MaximumLength(11).WithMessage("شماره موبایل نامعتبر است")
              .MinimumLength(11).WithMessage("شماره موبایل نامعتبر است");

            RuleFor(f => f.NationalCode)
             .NotNull()
             .NotEmpty()
             .WithMessage(ValidationMessages.required("کد ملی"))
             .MaximumLength(10).WithMessage(" کدملی نامعتبر است")
             .MinimumLength(10).WithMessage("کدملی نامعتبر است")
             .ValidNationalId();
        }
    }

    public class DecreaseOrderItemCountCommandValidator : AbstractValidator<DecreaseOrderItemCountDto>
    {
        public DecreaseOrderItemCountCommandValidator()
        {
            RuleFor(f => f.Count)
                .GreaterThanOrEqualTo(1).WithMessage("تعداد باید بیشتر از 0 باشد");
        }
    }

    public class IncreaseOrderItemCountCommandValidator : AbstractValidator<IncreaseOrderItemCountDto>
    {
        public IncreaseOrderItemCountCommandValidator()
        {
            RuleFor(f => f.Count)
                .GreaterThanOrEqualTo(1).WithMessage("تعداد باید بیشتر از 0 باشد");
        }
    }
}
