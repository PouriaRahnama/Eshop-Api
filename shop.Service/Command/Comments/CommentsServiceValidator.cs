using Common.Application.Validation;
using FluentValidation;
using shop.Service.DTOs.CommentsCommand;


namespace shop.Service.Command
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentDto>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(r => r.Text)
                .NotNull()
                .MinimumLength(5).WithMessage(ValidationMessages.minLength("متن نظر", 5));
        }
    }

    public class EditCommentCommandValidator : AbstractValidator<EditCommentDto>
    {
        public EditCommentCommandValidator()
        {
            RuleFor(r => r.Text)
                .NotNull()
                .MinimumLength(5).WithMessage(ValidationMessages.minLength("متن نظر", 5));
        }
    }

}
