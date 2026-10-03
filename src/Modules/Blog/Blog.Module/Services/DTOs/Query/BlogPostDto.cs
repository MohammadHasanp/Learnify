using Common.Query;

namespace BlogModule.Services.DTOs.Query;

public class BlogPostDto : BaseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public Guid UserId { get; set; }
    public string WriterName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public long Visit { get; set; }
    public string ImageName { get; set; } = null!;
    public Guid CategoryId { get; set; }
}

