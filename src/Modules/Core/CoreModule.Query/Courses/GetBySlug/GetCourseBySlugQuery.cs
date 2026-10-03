using Common.Query;
using CoreModule.Query.Courses.DTOs;

namespace CoreModule.Query.Courses.GetBySlug;

public record GetCourseBySlugQuery(string Slug) : IQuery<CourseDto?>;