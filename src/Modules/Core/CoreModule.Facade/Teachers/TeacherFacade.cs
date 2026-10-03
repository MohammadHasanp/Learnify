using Common.Application;
using CoreModule.Application.Teachers.AcceptedRequest;
using CoreModule.Application.Teachers.Register;
using CoreModule.Application.Teachers.RejectedRequest;
using CoreModule.Application.Teachers.Toggle;
using CoreModule.Query.Teachers.DTOs;
using CoreModule.Query.Teachers.GetById;
using CoreModule.Query.Teachers.GetByUserId;
using CoreModule.Query.Teachers.GetList;
using MediatR;

namespace CoreModule.Facade.Teachers;

public class TeacherFacade(IMediator mediator) : ITeacherFaced
{
    private readonly IMediator _mediator = mediator;
    public async Task<OperationResult> Accepted(AcceptedTeacherRequestCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> ToggleStatus(Guid teacherId)
    {
        return await _mediator.Send(new ToggleStatusTeacherCommand(teacherId));
    }

    public async Task<TeacherDto?> GetByUserId(Guid userId)
    {
        return await _mediator.Send(new GetTeacherByUserIdQuery(userId));
    }

    public async Task<TeacherDto?> GetById(Guid id)
    {
        return await _mediator.Send(new GetTeacherByIdQuery(id));
    }

    public async Task<List<TeacherDto>> GetList()
    {
        return await _mediator.Send(new GetListTeacherQuery());
    }

    public async Task<OperationResult> Register(RegisterTeacherCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Rejected(RejectedTeacherRequestCommand command)
    {
        return await _mediator.Send(command);
    }
}