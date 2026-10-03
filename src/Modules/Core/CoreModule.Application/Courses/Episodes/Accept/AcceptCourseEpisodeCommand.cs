using Common.Application;

namespace CoreModule.Application.Courses.Episodes.Accept;

public record AcceptCourseEpisodeCommand(Guid CourseId, Guid EpisodeId) : IBaseCommand;