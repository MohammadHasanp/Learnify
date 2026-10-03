using Common.Domain.Repository;
using CoreModule.Domain.Courses.Enums;
using CoreModule.Domain.EntityHelpers.CourseStudents;

namespace CoreModule.Domain.EntityHelpers;

public interface ICourseStudentRepository : IBaseRepository<CourseStudent>
{
    public Task<CourseStudent?> GetCourseStudent(Guid userId, Guid courseId);
    public void Delete(CourseStudent courseStudent);
}