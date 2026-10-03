using Common.Domain.Repository;

namespace CoreModule.Domain.Teachers.Repository;

public interface ITeacherRepository : IBaseRepository<Teacher>
{
    public void Delete(Teacher teacher);
}
