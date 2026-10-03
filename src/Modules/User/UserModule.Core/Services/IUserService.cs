using Common.Application;
using UserModule.Core.Commands.Users.ChangePassword;
using UserModule.Core.Commands.Users.Edit;
using UserModule.Core.Commands.Users.FullEdit;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.Queries.Users.DTOs;

namespace UserModule.Core.Services;

public interface IUserService
{
    public Task<OperationResult<Guid>> Register(RegisterUserCommand command);
    public Task<OperationResult> Edit(EditUserCommand command);
    public Task<OperationResult> FullEdit(FullEditUserCommand command);
    public Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command);
    public Task<UserDto?> GetUserById(Guid userId);
    public Task<UserDto?> GetUserByMobile(string mobile);
    public Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams);
}
