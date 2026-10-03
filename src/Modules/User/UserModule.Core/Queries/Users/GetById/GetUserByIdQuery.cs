namespace UserModule.Core.Queries.Users.GetById;

using Common.Query;
using UserModule.Core.Queries.Users.DTOs;

public record GetUserByIdQuery(Guid UserId) : IQuery<UserDto?>;
