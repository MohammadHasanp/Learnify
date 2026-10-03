using Common.Application.Validation;
using FluentValidation;

namespace CoreModule.Application.CourseCategories.AddChild;

public class AddChildCourseCategoryValidator : AbstractValidator<AddChildCourseCategoryCommand>
{
    public AddChildCourseCategoryValidator()
    {

        RuleFor(r => r.Title).NotEmpty().NotNull().WithMessage(ValidationMessages.Required);

        RuleFor(r => r.Slug).NotEmpty().NotNull().WithMessage(ValidationMessages.Required);
    }
}