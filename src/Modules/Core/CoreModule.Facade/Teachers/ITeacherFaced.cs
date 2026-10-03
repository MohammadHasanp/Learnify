using Common.Application;
using CoreModule.Application.Teachers.AcceptedRequest;
using CoreModule.Application.Teachers.Register;
using CoreModule.Application.Teachers.RejectedRequest;
using CoreModule.Application.Teachers.Toggle;
using CoreModule.Query.Teachers.DTOs;

namespace CoreModule.Facade.Teachers;

public interface ITeacherFaced
{
    public Task<OperationResult> Register(RegisterTeacherCommand command);
    public Task<OperationResult> Rejected(RejectedTeacherRequestCommand command);
    public Task<OperationResult> Accepted(AcceptedTeacherRequestCommand command);
    public Task<OperationResult> ToggleStatus(Guid teacherId);
    public Task<TeacherDto?> GetByUserId(Guid userId);
    public Task<TeacherDto?> GetById(Guid id);
    public Task<List<TeacherDto>> GetList();
}
