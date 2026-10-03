using Common.Infrastructure;
using CoreModule.Domain.Teachers;
using CoreModule.Domain.Teachers.Repository;
using CoreModule.Infrastructure.Persistent._Context;

namespace CoreModule.Infrastructure.Persistent.Teachers;

public class TeacherRepository : BaseRepository<Teacher, CoreModuleEfContext>, ITeacherRepository
{
    public TeacherRepository(CoreModuleEfContext context) : base(context)
    {
    }

    public void Delete(Teacher teacher)
    {
        Context.Remove(teacher);
    }
}
