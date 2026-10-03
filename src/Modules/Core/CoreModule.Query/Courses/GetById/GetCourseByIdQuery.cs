using Common.Application;
using Common.Query;
using CoreModule.Domain.Courses.Enums;
using CoreModule.Query.Courses.DTOs;

namespace CoreModule.Query.Courses.GetById;

public record GetCourseByIdQuery(Guid CourseId) : IQuery<CourseDto?>;