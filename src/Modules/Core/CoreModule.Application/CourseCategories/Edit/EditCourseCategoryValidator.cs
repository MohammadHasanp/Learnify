using Common.Application.Validation;
using FluentValidation;

namespace CoreModule.Application.CourseCategories.Edit;

public class EditCourseCategoryValidator : AbstractValidator<EditCourseCategoryCommand>
{
    public EditCourseCategoryValidator()
    {
        RuleFor(r => r.Title).NotEmpty().NotNull().WithMessage(ValidationMessages.Required);

        RuleFor(r => r.Slug).NotEmpty().NotNull().WithMessage(ValidationMessages.Required);
    }
}