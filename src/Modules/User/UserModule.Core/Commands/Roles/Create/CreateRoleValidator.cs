using FluentValidation;

namespace UserModule.Core.Commands.Roles.Create;

public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleValidator()
    {
        RuleFor(s => s.Title)
            .NotEmpty()
            .NotNull().WithMessage("عنوان نقش را وارد کنید");
    }
}