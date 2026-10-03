using Common.Query;
using CoreModule.Query.Teachers.DTOs;

namespace CoreModule.Query.Teachers.GetList;

public record GetListTeacherQuery() : IQuery<List<TeacherDto>>;
