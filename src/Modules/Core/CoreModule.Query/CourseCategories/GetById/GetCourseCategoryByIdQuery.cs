using Common.Application;
using Common.Query;
using CoreModule.Query.CourseCategories.DTOs;

namespace CoreModule.Query.CourseCategories.GetById;

public record GetCourseCategoryByIdQuery(Guid CategoryId) : IQuery<CourseCategoryDto?>;