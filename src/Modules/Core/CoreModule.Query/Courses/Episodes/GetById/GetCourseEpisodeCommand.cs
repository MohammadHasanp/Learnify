using Common.Application;
using Common.Query;
using CoreModule.Query.Courses.Episodes.DTOs;

namespace CoreModule.Query.Courses.Episodes.GetById;

public record GetCourseEpisodeCommand(Guid EpisodeId) : IQuery<EpisodeDto?>;