using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace CoreModule.Application.Courses.Episodes.Add;

public class AddEpisodeValidator : AbstractValidator<AddEpisodeCommand>
{
    public AddEpisodeValidator()
    {
        RuleFor(c => c.Title)
            .NotNull().NotEmpty().WithMessage("عنوان را وارد کنید");

        RuleFor(c => c.VideoFile)
            .JustValidFile();

        RuleFor(c => c.AttachmentFile)
            .JustValidCompressFile();

    }
}