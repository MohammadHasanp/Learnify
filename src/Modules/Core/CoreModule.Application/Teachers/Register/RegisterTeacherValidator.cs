using Common.Application.Validation;
using FluentValidation;

namespace CoreModule.Application.Teachers.Register;

public class RegisterTeacherValidator : AbstractValidator<RegisterTeacherCommand>
{
    public RegisterTeacherValidator()
    {
        RuleFor(a => a.CvFile)
            .NotNull().NotEmpty().WithMessage(ValidationMessages.Required);

        RuleFor(a => a.UserName)
            .NotNull().NotEmpty().WithMessage(ValidationMessages.Required);
    }
}