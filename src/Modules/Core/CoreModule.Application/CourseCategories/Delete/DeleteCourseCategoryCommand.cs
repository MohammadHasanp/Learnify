using Common.Application;

namespace CoreModule.Application.CourseCategories.Delete;

public record DeleteCourseCategoryCommand(Guid Id) : IBaseCommand;
