using Common.Application;

namespace CoreModule.Application.Courses.Sections.Add;

public record AddSectionCommand(string Title, int DisplayOrder, Guid CourseId) : IBaseCommand;