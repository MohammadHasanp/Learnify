using Common.Query;

namespace CoreModule.Query.Courses.Episodes.DTOs;

public class EpisodeDto : BaseDto
{
    public Guid SectionId { get; set; }
    public string Title { get; set; } = null!;
    public string EnglishTitle { get; set; } = null!;
    public Guid Token { get; set; }
    public TimeSpan Time { get; set; }
    public string VideoName { get; set; } = null!;
    public string? AttachmentName { get; set; }
    public bool IsActive { get; set; }
    public bool IsFree { get; set; }
}