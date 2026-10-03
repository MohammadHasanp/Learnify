using Common.Application;

namespace CoreModule.Application.CourseCategories.AddChild;

public record AddChildCourseCategoryCommand(string Title, string Slug, Guid ParentId) : IBaseCommand;
