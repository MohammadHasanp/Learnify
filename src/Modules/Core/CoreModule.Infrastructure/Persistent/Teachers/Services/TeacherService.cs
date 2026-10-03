using CoreModule.Domain.Teachers.Repository;
using CoreModule.Domain.Teachers.Service;

namespace CoreModule.Infrastructure.Persistent.Teachers.Services;

public class TeacherService(ITeacherRepository repository) : ITeacherService
{
    private readonly ITeacherRepository _repository = repository;
    public bool IsExistsUserName(string userName)
    {
        return _repository.Exists(c => c.UserName == userName);
    }
}
