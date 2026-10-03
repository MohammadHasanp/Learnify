using Common.Application;
using UserModule.Core.Commands.Roles.Create;
using UserModule.Core.Commands.Roles.Delete;
using UserModule.Core.Commands.Roles.Edit;
using UserModule.Core.Queries.Roles.DTOs;

namespace UserModule.Core.Services;

public interface IRoleService
{
    public Task<OperationResult> Create(CreateRoleCommand command);
    public Task<OperationResult> Delete(DeleteRoleCommand command);
    public Task<OperationResult> Edit(EditRoleCommand command);


    public Task<List<RoleDto>> GetAllRole();
    public Task<RoleDto?> GetRoleById(Guid roleId);
}