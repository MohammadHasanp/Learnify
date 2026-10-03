using Common.Application;
using System.Xml;

namespace CoreModule.Application.CourseCategories.Edit;

public record EditCourseCategoryCommand(Guid Id, string Title, string Slug) : IBaseCommand;
