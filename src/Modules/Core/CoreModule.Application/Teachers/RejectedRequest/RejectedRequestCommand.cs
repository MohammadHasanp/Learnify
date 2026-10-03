using Common.Application;

namespace CoreModule.Application.Teachers.RejectedRequest;

public record RejectedTeacherRequestCommand(Guid TeacherId,string Description) : IBaseCommand;
