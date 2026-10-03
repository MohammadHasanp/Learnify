using Common.Application;
using CoreModule.Application.Courses.Create;
using CoreModule.Application.Courses.Edit;
using CoreModule.Application.Courses.Episodes.Accept;
using CoreModule.Application.Courses.Episodes.Add;
using CoreModule.Application.Courses.Episodes.Delete;
using CoreModule.Application.Courses.Episodes.Edit;
using CoreModule.Application.Courses.Sections.Add;
using CoreModule.Application.CourseStudents.Add;
using CoreModule.Application.CourseStudents.Remove;
using CoreModule.Query.Courses.DTOs;
using CoreModule.Query.Courses.Episodes.DTOs;

namespace CoreModule.Facade.Courses;

public interface ICourseFacade
{
    public Task<OperationResult> Create(CreateCourseCommand command);
    public Task<OperationResult> Edit(EditCourseCommand command);
    public Task<OperationResult> AddSection(AddSectionCommand command);
    public Task<OperationResult> AddEpisode(AddEpisodeCommand command);
    public Task<OperationResult> EditEpisode(EditEpisodeCommand command);
    public Task<OperationResult> AcceptEpisode(AcceptCourseEpisodeCommand command);
    public Task<OperationResult> DeleteEpisode(DeleteCourseEpisodeCommand command);
    public Task<OperationResult> AddCourseStudent(Guid courseId, Guid userId);
    public Task<OperationResult> RemoveCourseStudent(Guid courseId, Guid userId);

    public Task<CourseFilterResult> GetByFilter(CourseFilterParams filterParams);
    public Task<CourseDto?> GetById(Guid courseId);
    public Task<CourseDto?> GetBySlug(string slug);
    public Task<EpisodeDto?> GetEpisodeById(Guid episodeId);
}
