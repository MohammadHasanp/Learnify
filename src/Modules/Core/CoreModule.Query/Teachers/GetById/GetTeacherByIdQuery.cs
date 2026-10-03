using Common.Query;
using CoreModule.Query.Teachers.DTOs;

namespace CoreModule.Query.Teachers.GetById;

public record GetTeacherByIdQuery(Guid Id) : IQuery<TeacherDto?>;
