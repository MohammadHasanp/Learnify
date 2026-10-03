using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace UserModule.Core.Commands.Users.FullEdit;

public class FullEditUserCommandValidator : AbstractValidator<FullEditUserCommand>
{
    public FullEditUserCommandValidator()
    {
        RuleFor(f => f.Mobile)
            .NotNull()
            .NotEmpty()
            .ValidPhoneNumber();


        RuleFor(f => f.Email)
            .EmailAddress();
    }
}