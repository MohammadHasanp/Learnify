using Common.Application;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.Courses.Episodes.Add;

public record AddEpisodeCommand(Guid CourseId, Guid SectionId, string Title, string EnglishTitle, TimeSpan Time,
    IFormFile? AttachmentFile, IFormFile VideoFile, bool IsActive,bool IsFree) : IBaseCommand;