using Common.Application;
using CoreModule.Domain.Courses.Repository;

namespace CoreModule.Application.Courses.Sections.Add;

public class AddSectionHandler(ICourseRepository courseRepository) : IBaseCommandHandler<AddSectionCommand>
{
    private readonly ICourseRepository _courseRepository = courseRepository;
    public async Task<OperationResult> Handle(AddSectionCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetTracking(request.CourseId);
        if (course == null)
            return OperationResult.NotFound();

        course.AddSection(request.Title, request.DisplayOrder);
        _courseRepository.Update(course);
        await _courseRepository.Save();
        return OperationResult.Success();
    }
}