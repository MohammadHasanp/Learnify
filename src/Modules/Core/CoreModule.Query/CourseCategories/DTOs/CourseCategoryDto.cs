using Common.Query;

namespace CoreModule.Query.CourseCategories.DTOs;

public class CourseCategoryDto : BaseDto
{
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public Guid? ParentId { get; set; }
    public List<CategoryChildDto> Childs { get; set; } = [];
}

public class CategoryChildDto : BaseDto
{
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public Guid ParentId { get; set; }
}