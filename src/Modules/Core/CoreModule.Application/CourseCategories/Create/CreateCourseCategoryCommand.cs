using Common.Application;

namespace CoreModule.Application.CourseCategories.Create;

public record CreateCourseCategoryCommand(string Title, string Slug) : IBaseCommand;
