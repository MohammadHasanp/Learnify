using Common.Application;

namespace CoreModule.Application.Teachers.AcceptedRequest;

public record AcceptedTeacherRequestCommand(Guid TeacherId) : IBaseCommand;
