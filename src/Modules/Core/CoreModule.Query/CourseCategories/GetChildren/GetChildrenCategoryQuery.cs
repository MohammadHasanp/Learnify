using Common.Query;
using CoreModule.Query.CourseCategories.DTOs;

namespace CoreModule.Query.CourseCategories.GetChildren;

public record GetChildrenCategoryQuery(Guid ParentId) : IQuery<List<CourseCategoryDto>>;