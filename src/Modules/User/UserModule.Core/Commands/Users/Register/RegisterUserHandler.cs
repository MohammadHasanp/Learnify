using Common.Application;
using Common.Application.SecurityUtil;
using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using User.Module.Data.Context;

namespace UserModule.Core.Commands.Users.Register;

public class RegisterUserHandler(UserContext context, IEventBus eventBus) : IBaseCommandHandler<RegisterUserCommand, Guid>
{
    public async Task<OperationResult<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(u => u.Mobile == request.Mobile, cancellationToken: cancellationToken))
            return OperationResult<Guid>.Error("شماره تلفن تکراری است");

        var user = new User.Module.Data.Entities.Users.User
        {
            Mobile = request.Mobile,
            Password = Sha256Hasher.Hash(request.Password),
            Avatar = "avatar.png",
        };

        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);

        await eventBus.Publish(new UserRegistered
        {
            Id = user.Id,
            Name = user.Name,
            Family = user.Family,
            Mobile = user.Mobile,
            Email = user.Email,
            Avatar = user.Avatar,
            Password = user.Password
        }, null, Exchanges.UserTopicExchange, ExchangeType.Topic, "user.register");
        return OperationResult<Guid>.Success(user.Id);
    }
}