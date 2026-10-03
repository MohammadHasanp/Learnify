using CoreModule.Domain.Categories.Repository;
using CoreModule.Domain.Categories.Services;

namespace CoreModule.Infrastructure.Persistent.CourseCategories.Services;

public class CourseCategoryServide(ICourseCategoryRepository repository) : ICourseCategoryService
{
    private readonly ICourseCategoryRepository _repository = repository;
    public bool IsExistsSlug(string slug)
    {
        return _repository.Exists(c => c.Slug == slug);
    }
}
