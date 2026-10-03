using Common.Application;
using CoreModule.Domain.EntityHelpers;

namespace CoreModule.Application.CourseStudents.Remove;

public record RemoveCourseStudentCommand(Guid UserId, Guid CourseId) : IBaseCommand;


public class RemoveCourseStudentHandler(ICourseStudentRepository repository) : IBaseCommandHandler<RemoveCourseStudentCommand>
{
    public async Task<OperationResult> Handle(RemoveCourseStudentCommand request, CancellationToken cancellationToken)
    {
        var courseStudent = await repository.GetCourseStudent(request.UserId, request.CourseId);
        if (courseStudent != null)
        {
            repository.Delete(courseStudent);
            await repository.Save();
            return OperationResult.Success();
        }

        return OperationResult.NotFound();
    }
}