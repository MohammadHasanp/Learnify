using AutoMapper;
using Common.Query;
using Microsoft.EntityFrameworkCore;
using User.Module.Data.Context;
using UserModule.Core.Queries.Users.DTOs;

namespace UserModule.Core.Queries.Users.GetByMobile;

public class GetUserByMobileHandler(UserContext context, IMapper mapper) : IQueryHandler<GetUserByMobileQuery, UserDto?>
{
    private readonly UserContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<UserDto?> Handle(GetUserByMobileQuery request, CancellationToken cancellationToken)
    {
        var user = await this._context.Users.FirstOrDefaultAsync(u => u.Mobile == request.Mobile);
        if (user == null)
            return null;

        return _mapper.Map<UserDto>(user);
    }
}