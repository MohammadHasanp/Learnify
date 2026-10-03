using Common.Application;
using MediatR;
using UserModule.Core.Commands.Users.UserNotifications.Create;
using UserModule.Core.Commands.Users.UserNotifications.Delete;
using UserModule.Core.Commands.Users.UserNotifications.DeleteAll;
using UserModule.Core.Commands.Users.UserNotifications.Seen;
using UserModule.Core.Queries.Users.DTOs;
using UserModule.Core.Queries.Users.UserNotifications.GetFilter;

namespace UserModule.Core.Services;

public interface IUserNotificationService
{
    public Task<OperationResult> Create(CreateUserNotificationCommand command);
    public Task<OperationResult> Seen(SeenNotificationCommand command);
    public Task<OperationResult> Delete(DeleteUserNotificationCommand command);
    public Task<OperationResult> DeleteAll(DeleteAllUserNotificationCommand command);
    public Task<UserNotificationFilterResult> GetByFilter(UserNotificationFilterParams filterParams);
}


class UserNotificationService(IMediator mediator) : IUserNotificationService
{
    private readonly IMediator _mediator = mediator;
    public async Task<OperationResult> Create(CreateUserNotificationCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Delete(DeleteUserNotificationCommand command)
    {
        return await _mediator.Send(command);

    }

    public async Task<OperationResult> DeleteAll(DeleteAllUserNotificationCommand command)
    {
        return await _mediator.Send(command);

    }

    public async Task<UserNotificationFilterResult> GetByFilter(UserNotificationFilterParams filterParams)
    {
        return await _mediator.Send(new GetUserNotificationByFilterQuery(filterParams));
    }

    public async Task<OperationResult> Seen(SeenNotificationCommand command)
    {
        return await _mediator.Send(command);

    }
}
