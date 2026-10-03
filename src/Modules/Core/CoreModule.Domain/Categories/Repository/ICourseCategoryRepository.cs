using Common.Domain.Repository;

namespace CoreModule.Domain.Categories.Repository;

public interface ICourseCategoryRepository : IBaseRepository<CourseCategory>
{
    public Task Delete(CourseCategory category);
}
