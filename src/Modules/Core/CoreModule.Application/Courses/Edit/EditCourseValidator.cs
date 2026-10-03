using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace CoreModule.Application.Courses.Edit;

public class EditCourseValidator : AbstractValidator<EditCourseCommand>
{
    public EditCourseValidator()
    {

        RuleFor(c => c.Title)
            .NotEmpty().NotNull().WithMessage(ValidationMessages.Required);

        RuleFor(c => c.Slug)
            .NotEmpty().NotNull().WithMessage(ValidationMessages.Required);

        RuleFor(c => c.Description)
            .NotEmpty().NotNull().WithMessage(ValidationMessages.Required);

        RuleFor(c => c.ImageFile)
            .NotEmpty().NotNull().WithMessage(ValidationMessages.Required)
            .JustImageFile();

        RuleFor(c => c.Price)
            .GreaterThan(0).WithMessage("مبلغ باید بزرگ تر از صفر باشد ");
    }
}