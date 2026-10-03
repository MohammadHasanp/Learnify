using Common.Application.Validation;
using FluentValidation;

namespace UserModule.Core.Commands.Users.ChangePassword;

public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordValidator()
    {
        RuleFor(u => u.CurrentPasssword)
            .NotEmpty().NotNull().WithMessage(ValidationMessages.Required);

        RuleFor(u => u.NewPassword)
            .NotNull().NotEmpty().WithMessage(ValidationMessages.Required)
            .MinimumLength(6).WithMessage(ValidationMessages.minLength("NewPassword", 6));
    }
}