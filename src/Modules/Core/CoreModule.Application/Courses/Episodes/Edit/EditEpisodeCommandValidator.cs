using FluentValidation;

namespace CoreModule.Application.Courses.Episodes.Edit;

public class EditEpisodeCommandValidator : AbstractValidator<EditEpisodeCommand>
{
    public EditEpisodeCommandValidator()
    {
        RuleFor(r => r.Title)
            .NotNull()
            .NotEmpty();
    }
}