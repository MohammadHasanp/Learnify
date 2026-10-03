using FluentValidation;

namespace UserModule.Core.Commands.Roles.Edit;

public class EditRoleValidation : AbstractValidator<EditRoleCommand>
{
    public EditRoleValidation()
    {
        RuleFor(s => s.Title)
            .NotEmpty()
            .NotNull().WithMessage("عنوان نقش مورد نظر را وارد کنید");
    }
}