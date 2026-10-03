using Common.Application;
using CoreModule.Domain.Courses.Repository;

namespace CoreModule.Application.Courses.Episodes.Delete;

public record DeleteCourseEpisodeCommand(Guid CourseId, Guid EpisodeId) : IBaseCommand;
