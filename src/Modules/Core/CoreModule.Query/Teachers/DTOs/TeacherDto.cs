using Common.Query;
using CoreModule.Domain.Teachers.Enums;
using CoreModule.Query.DTOs;

namespace CoreModule.Query.Teachers.DTOs;

public class TeacherDto : BaseDto
{
    public string UserName { get; set; } = null!;
    public string CvFileName { get; set; } = null!;
    public TeacherStatus TeacherStatus { get; set; }
    public CoreModuleUserDto User { get; set; } = null!;
}
