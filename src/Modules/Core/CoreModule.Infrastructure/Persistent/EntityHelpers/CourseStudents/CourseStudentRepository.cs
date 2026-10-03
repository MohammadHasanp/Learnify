using Common.Infrastructure;
using CoreModule.Domain.EntityHelpers;
using CoreModule.Domain.EntityHelpers.CourseStudents;
using CoreModule.Infrastructure.Persistent._Context;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Infrastructure.Persistent.EntityHelpers.CourseStudents;

public class CourseStudentRepository(CoreModuleEfContext context) : BaseRepository<CourseStudent, CoreModuleEfContext>(context), ICourseStudentRepository
{
    public Task<CourseStudent?> GetCourseStudent(Guid userId, Guid courseId)
    {
        return Context.CourseStudents.FirstOrDefaultAsync(s => s.UserId == userId && s.CourseId == courseId);
    }

    public void Delete(CourseStudent courseStudent)
    {
        Context.Remove(courseStudent);
    }
}