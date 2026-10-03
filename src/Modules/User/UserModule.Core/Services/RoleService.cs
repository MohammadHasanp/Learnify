using Common.Application;
using MediatR;
using UserModule.Core.Commands.Roles.Create;
using UserModule.Core.Commands.Roles.Delete;
using UserModule.Core.Commands.Roles.Edit;
using UserModule.Core.Queries.Roles.DTOs;
using UserModule.Core.Queries.Roles.GetAll;
using UserModule.Core.Queries.Roles.GetById;

namespace UserModule.Core.Services;

public class RoleService(IMediator mediator) : IRoleService
{
    public async Task<OperationResult> Create(CreateRoleCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> Delete(DeleteRoleCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditRoleCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<List<RoleDto>> GetAllRole()
    {
        return await mediator.Send(new GetAllRoleQuery());
    }

    public async Task<RoleDto?> GetRoleById(Guid roleId)
    {
        return await mediator.Send(new GetRoleByIdQuery(roleId));
    }
}