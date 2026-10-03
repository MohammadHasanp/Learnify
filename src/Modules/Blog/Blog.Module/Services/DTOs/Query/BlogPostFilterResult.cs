using Common.Query;

namespace BlogModule.Services.DTOs.Query;

public class BlogPostFilterResult : BaseFilter<BlogPostFilterData>;


public class BlogPostFilterParams : BaseFilterParam
{
    public string? Search { get; set; }
    public string? CategorySlug { get; set; } = null!;
}

public class BlogPostFilterData : BaseDto
{
    public string Title { get; set; } = null!;
    public Guid UserId { get; set; }
    public string WriterName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public long Visit { get; set; }
    public string ImageName { get; set; } = null!;
    public BlogCategoryDto Category { get; set; } = null!;
}