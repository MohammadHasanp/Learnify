using Common.Application;

namespace CoreModule.Application.Teachers.Toggle;

public record ToggleStatusTeacherCommand(Guid TeacherId) : IBaseCommand;