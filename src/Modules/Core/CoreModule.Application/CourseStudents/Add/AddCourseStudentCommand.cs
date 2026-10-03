using Common.Application;
using CoreModule.Domain.EntityHelpers;
using CoreModule.Domain.EntityHelpers.CourseStudents;

namespace CoreModule.Application.CourseStudents.Add;

public record AddCourseStudentCommand(Guid UserId, Guid CourseId) : IBaseCommand;


public class AddCourseStudentHandler(ICourseStudentRepository repository) : IBaseCommandHandler<AddCourseStudentCommand>
{
    public async Task<OperationResult> Handle(AddCourseStudentCommand request, CancellationToken cancellationToken)
    {
        var courseStudent = await repository.GetCourseStudent(request.UserId, request.CourseId);
        if (courseStudent != null)
        {
            return OperationResult.Success();
        }
        else
        {
            await repository.AddAsync(new CourseStudent
            {
                CourseId = request.CourseId,
                UserId = request.UserId,
            });
            await repository.Save();
            return OperationResult.Success();
        }
    }
}