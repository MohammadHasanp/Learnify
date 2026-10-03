namespace CoreModule.Domain.Courses.Service;

public interface ICourseService
{
    public bool IsExistsSlug(string slug);
}
