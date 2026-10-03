using Common.Application.Validation;
using FluentValidation;

namespace UserModule.Core.Commands.Users.UserNotifications.Create;

public class CreateUserNotificationValidator : AbstractValidator<CreateUserNotificationCommand>
{
    public CreateUserNotificationValidator()
    {
        RuleFor(n => n.Text)
            .NotNull().
             NotEmpty().WithMessage(ValidationMessages.Required);

        RuleFor(n => n.Title)
             .NotNull()
             .NotEmpty().WithMessage(ValidationMessages.Required);
    }
}