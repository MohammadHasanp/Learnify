using FluentValidation;

namespace CoreModule.Application.Courses.Sections.Add;

public class AddSectionValidator : AbstractValidator<AddSectionCommand>
{
    public AddSectionValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty().NotNull().WithMessage("عنوان را وارد کنید");

        RuleFor(c => c.DisplayOrder)
            .GreaterThan(0).WithMessage("ترتیب نمایش نمیواند عدد منفی باشد");
    }
}