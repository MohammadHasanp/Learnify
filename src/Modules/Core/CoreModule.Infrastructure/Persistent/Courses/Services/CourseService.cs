using CoreModule.Domain.Courses.Repository;
using CoreModule.Domain.Courses.Service;

namespace CoreModule.Infrastructure.Persistent.Courses.Services;

public class CourseService(ICourseRepository repository) : ICourseService
{
    private readonly ICourseRepository _repository = repository;
    public bool IsExistsSlug(string slug)
    {
        return _repository.Exists(c => c.Slug == slug);
    }
}
