using Common.Query;
using Microsoft.EntityFrameworkCore;
using User.Module.Data.Context;
using UserModule.Core.Queries.Users.DTOs;

namespace UserModule.Core.Queries.Users.GetByFilter;

public class GetUsersByFilterQueryHandler(UserContext context) : IQueryHandler<GetUsersByFilterQuery, UserFilterResult>
{
    public async Task<UserFilterResult> Handle(GetUsersByFilterQuery request, CancellationToken cancellationToken)
    {
        var result = context.Users.OrderByDescending(d => d.CreationDate).AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.FilterParams.Email))
        {
            result = result.Where(r => r.Email != null && r.Email.Contains(request.FilterParams.Email));
        }

        if (!string.IsNullOrWhiteSpace(request.FilterParams.PhoneNumber))
        {
            result = result.Where(r => r.Mobile.Contains(request.FilterParams.PhoneNumber));
        }

        if (request.FilterParams.StartDate != null)
        {
            result = result.Where(r => r.CreationDate.Date >= request.FilterParams.StartDate.Value.Date);
        }

        if (request.FilterParams.EndDate != null)
        {
            result = result.Where(r => r.CreationDate.Date <= request.FilterParams.EndDate.Value.Date);
        }

        var skip = (request.FilterParams.PageId - 1) * request.FilterParams.Take;
        var model = new UserFilterResult
        {
            Datas = await result.Skip(skip).Take(request.FilterParams.Take).Select(s => new UserDto
            {
                Id = s.Id,
                CreationDate = s.CreationDate,
                Name = s.Name,
                Family = s.Family,
                Mobile = s.Mobile,
                Email = s.Email,
                Password = null,
                Avatar = s.Avatar,
                Roles = null
            }).ToListAsync(cancellationToken)
        };
        model.GeneratePaging(result, request.FilterParams.Take, request.FilterParams.PageId);
        return model;
    }
}