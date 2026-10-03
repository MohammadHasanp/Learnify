using Common.Application.Validation;
using FluentValidation;

namespace CoreModule.Application.CourseCategories.Create;

public class CreateCourseCategoryValidator : AbstractValidator<CreateCourseCategoryCommand>
{
    public CreateCourseCategoryValidator()
    {
        RuleFor(r => r.Title).NotEmpty().NotNull().WithMessage(ValidationMessages.Required);

        RuleFor(r => r.Slug).NotEmpty().NotNull().WithMessage(ValidationMessages.Required);
    }
}