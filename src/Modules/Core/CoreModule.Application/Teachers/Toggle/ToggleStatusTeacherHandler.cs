using Common.Application;
using CoreModule.Domain.Teachers.Repository;

namespace CoreModule.Application.Teachers.Toggle;

public class ToggleStatusTeacherHandler(ITeacherRepository repository) : IBaseCommandHandler<ToggleStatusTeacherCommand>
{
    private readonly ITeacherRepository _repository = repository;
    public async Task<OperationResult> Handle(ToggleStatusTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _repository.GetTracking(request.TeacherId);
        if (teacher == null)
            return OperationResult.NotFound();

        teacher.ToggleStatus();
        await _repository.Save();
        return OperationResult.Success();
    }
}