using User.Module.Data.Context;
using UserModule.Core.Queries.Users.DTOs;

namespace UserModule.Core.Queries.Users.GetById;

using AutoMapper;
using Common.Query;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public class GetUserByIdHandler(UserContext context, IMapper mapper) : IQueryHandler<GetUserByIdQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users.Include(user => user.UserRoles).ThenInclude(userRole => userRole.Role)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        if (user == null)
            return null!;

        var userDto = mapper.Map<UserDto>(user);
        userDto.Roles = user.UserRoles.Select(s => new RoleDto()
        {
            Id = s.RoleId,
            Title = s.Role.Name
        }).ToList();
        return userDto;
    }
}