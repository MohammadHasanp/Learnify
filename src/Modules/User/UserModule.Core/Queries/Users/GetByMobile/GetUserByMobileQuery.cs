using Common.Query;
using UserModule.Core.Queries.Users.DTOs;

namespace UserModule.Core.Queries.Users.GetByMobile;

public record GetUserByMobileQuery(string Mobile) : IQuery<UserDto?>;
