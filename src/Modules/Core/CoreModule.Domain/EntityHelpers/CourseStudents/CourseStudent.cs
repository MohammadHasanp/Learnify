using Common.Domain;

namespace CoreModule.Domain.EntityHelpers.CourseStudents;

public class CourseStudent : Entity
{
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
}