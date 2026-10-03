using Common.Query;
using CoreModule.Query.CourseCategories.DTOs;

namespace CoreModule.Query.CourseCategories.GetAll;

public record GetAllCourseCategoryQuery : IQuery<List<CourseCategoryDto>>;