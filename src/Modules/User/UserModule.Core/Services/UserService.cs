using Common.Application;
using MediatR;
using UserModule.Core.Commands.Users.ChangePassword;
using UserModule.Core.Commands.Users.Edit;
using UserModule.Core.Commands.Users.FullEdit;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.Queries.Users.DTOs;
using UserModule.Core.Queries.Users.GetByFilter;
using UserModule.Core.Queries.Users.GetById;
using UserModule.Core.Queries.Users.GetByMobile;

namespace UserModule.Core.Services;

public class UserService(IMediator mediator) : IUserService
{
    public async Task<OperationResult> FullEdit(FullEditUserCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditUserCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<UserDto?> GetUserById(Guid userId)
    {
        return await mediator.Send(new GetUserByIdQuery(userId));
    }

    public async Task<UserDto?> GetUserByMobile(string mobile)
    {
        return await mediator.Send(new GetUserByMobileQuery(mobile));
    }

    public async Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams)
    {
        return await mediator.Send(new GetUsersByFilterQuery(filterParams));
    }

    public async Task<OperationResult<Guid>> Register(RegisterUserCommand command) => await mediator.Send(command);
}