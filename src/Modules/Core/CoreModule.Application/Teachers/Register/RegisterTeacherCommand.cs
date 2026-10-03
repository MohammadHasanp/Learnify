using Common.Application;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.Teachers.Register;

public record RegisterTeacherCommand(IFormFile CvFile, Guid UserId, string UserName) : IBaseCommand;
