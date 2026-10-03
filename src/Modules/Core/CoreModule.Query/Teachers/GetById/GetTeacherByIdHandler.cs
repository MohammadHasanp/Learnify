using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Teachers.DTOs;
using CoreModule.Query.Teachers.Mapper;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Teachers.GetById;

internal class GetTeacherByIdHandler(QueryContext context) : IQueryHandler<GetTeacherByIdQuery, TeacherDto?>
{
    private readonly QueryContext _context = context;
    public async Task<TeacherDto?> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers.Include(t => t.Users).FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        if (teacher == null)
            return null;

        return teacher.Map();
    }
}