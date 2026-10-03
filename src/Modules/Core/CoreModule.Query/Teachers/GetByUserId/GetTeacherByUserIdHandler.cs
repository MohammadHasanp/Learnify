using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Teachers.DTOs;
using CoreModule.Query.Teachers.Mapper;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Teachers.GetByUserId;

class GetTeacherByUserIdHandler(QueryContext context) : IQueryHandler<GetTeacherByUserIdQuery, TeacherDto?>
{
    private readonly QueryContext _context = context;
    public async Task<TeacherDto?> Handle(GetTeacherByUserIdQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers.Include(t => t.Users).FirstOrDefaultAsync(t => t.UserId == request.UserId, cancellationToken);
        if (teacher == null)
            return null;

        return teacher.Map();
    }
}