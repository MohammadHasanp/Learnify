using Common.Application;

namespace UserModule.Core.Commands.Users.Register;

public record RegisterUserCommand(string Password, string Mobile) : IBaseCommand<Guid>;
