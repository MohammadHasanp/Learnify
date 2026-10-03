using Microsoft.AspNetCore.Http;

namespace BlogModule.Services.DTOs.Command;

public class CreatePostCommand
{
    public string Title { get; set; } = null!;
    public Guid UserId { get; set; }
    public string WriterName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public IFormFile ImageFile { get; set; } = null!;
    public Guid CategoryId { get; set; }
}

public class EditPostCommand
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string WriterName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public IFormFile? ImageFile { get; set; }
    public Guid CategoryId { get; set; }
}