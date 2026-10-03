namespace BlogModule.Services.DTOs.Query;

public class BlogCategoryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
}
