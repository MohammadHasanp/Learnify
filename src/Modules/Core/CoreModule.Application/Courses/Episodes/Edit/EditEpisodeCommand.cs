using Common.Application;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.Courses.Episodes.Edit;

public record EditEpisodeCommand(Guid EpisodeId, Guid CourseId, Guid SectionId, string Title, TimeSpan TimeSpan, IFormFile? VideoFile, IFormFile? AttachmentFile,
    bool IsActive, bool IsFree) : IBaseCommand;