using Common.Application;
using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using User.Module.Data.Context;

namespace UserModule.Core.Commands.Users.Edit;

public class EditUserHandler(UserContext userContext, IEventBus eventBus) : IBaseCommandHandler<EditUserCommand>
{
    private readonly IEventBus _eventBus = eventBus;
    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);
        if (user == null)
            return OperationResult.Error();

        user.Name = request.Name;
        user.Family = request.Family;

        if (!string.IsNullOrWhiteSpace(request.Email))
            user.Email = request.Email;

        await userContext.SaveChangesAsync(cancellationToken);
        await _eventBus.Publish(new UserEdited
        {
            UserId = user.Id,
            Email = user.Email,
            Name = user.Name,
            Family = user.Family,
            Mobile = user.Mobile
        }, null, Exchanges.UserTopicExchange, ExchangeType.Topic, "user.edited");
        return OperationResult.Success();
    }
}
