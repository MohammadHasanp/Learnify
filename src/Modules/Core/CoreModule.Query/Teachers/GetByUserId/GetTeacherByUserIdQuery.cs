using Common.Query;
using CoreModule.Query.Teachers.DTOs;

namespace CoreModule.Query.Teachers.GetByUserId;

public record GetTeacherByUserIdQuery(Guid UserId) : IQuery<TeacherDto?>;
