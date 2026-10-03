using Common.Application.Validation;
using FluentValidation;

namespace UserModule.Core.Commands.Users.Register;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(u => u.Password)
            .NotEmpty().NotNull().WithMessage(ValidationMessages.Required)
            .MinimumLength(6).WithMessage(ValidationMessages.minLength("کلمه عبور", 6));

        RuleFor(u => u.Mobile)
            .NotEmpty().NotNull().WithMessage(ValidationMessages.Required)
            .Length(11).WithMessage(ValidationMessages.InvalidPhoneNumber);
    }
}