using Common.Query;
using CoreModule.Query.Courses.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoreModule.Query.Courses.GetByFilter;

public class GetCourseByFilterQuery(CourseFilterParams filterParams) : QueryFilter<CourseFilterResult, CourseFilterParams>(filterParams)
{

}