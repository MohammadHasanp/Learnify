using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.DTOs;
using CoreModule.Query.Teachers.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Teachers.GetList;

class GetListTeacherHandler(QueryContext context) : IQueryHandler<GetListTeacherQuery, List<TeacherDto>>
{
    private readonly QueryContext _context = context;
    public Task<List<TeacherDto>> Handle(GetListTeacherQuery request, CancellationToken cancellationToken)
    {

        return _context.Teachers.Include(t => t.Users).Select(static t => new TeacherDto
        {
            CreationDate = t.CreationDate,
            TeacherStatus = t.TeacherStatus,
            IsDelete = t.IsDelete,
            CvFileName = t.CvFileName,
            Id = t.Id,
            UserName = t.UserName,
            User = new CoreModuleUserDto
            {
                Id = t.Users.Id,
                Avatar = t.Users.Avatar,
                IsDelete = t.Users.IsDelete,
                CreationDate = t.Users.CreationDate,
                Email = t.Users.Email,
                Family = t.Users.Family,
                Mobile = t.Users.Mobile,
                Name = t.Users.Name
            }

        }).ToListAsync(cancellationToken: cancellationToken);
    }
}