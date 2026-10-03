using Common.Application;
using Common.Application.FileUtil.StorageInterfaces;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Teachers;
using CoreModule.Domain.Teachers.Repository;
using CoreModule.Domain.Teachers.Service;

namespace CoreModule.Application.Teachers.Register;

public class RegisterTeacherHandler(ILocalFileService localFileService, ITeacherService service, ITeacherRepository repository) : IBaseCommandHandler<RegisterTeacherCommand>
{
    private readonly ITeacherRepository _repository = repository;
    private readonly ITeacherService _service = service;
    private readonly ILocalFileService _localFileService = localFileService;
    public async Task<OperationResult> Handle(RegisterTeacherCommand request, CancellationToken cancellationToken)
    {
        var cvFileName = await _localFileService.SaveFileAndGenerateName(request.CvFile, CoreModuleDirectories.CvFileNames);

        var teacher = new Teacher(request.UserId, request.UserName, cvFileName, _service);

        _repository.Add(teacher);
        await _repository.Save();
        return OperationResult.Success();
    }
}