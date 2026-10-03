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
using CoreModule.Query.Courses.Episodes.GetById;
using CoreModule.Query.Courses.GetByFilter;
using CoreModule.Query.Courses.GetById;
using CoreModule.Query.Courses.GetBySlug;
using MediatR;

namespace CoreModule.Facade.Courses;

public class CourseFacade(IMediator mediator) : ICourseFacade
{
    private readonly IMediator _mediator = mediator;
    public async Task<OperationResult> Create(CreateCourseCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCourseCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddSection(AddSectionCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddEpisode(AddEpisodeCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditEpisode(EditEpisodeCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AcceptEpisode(AcceptCourseEpisodeCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteEpisode(DeleteCourseEpisodeCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddCourseStudent(Guid courseId, Guid userId)
    {
        return await _mediator.Send(new RemoveCourseStudentCommand(userId, courseId));
    }

    public async Task<OperationResult> RemoveCourseStudent(Guid courseId, Guid userId)
    {
        return await _mediator.Send(new RemoveCourseStudentCommand(userId, courseId));
    }

    public async Task<CourseFilterResult> GetByFilter(CourseFilterParams filterParams)
    {
        return await _mediator.Send(new GetCourseByFilterQuery(filterParams));
    }

    public async Task<CourseDto?> GetById(Guid courseId)
    {
        return await _mediator.Send(new GetCourseByIdQuery(courseId));
    }

    public async Task<CourseDto?> GetBySlug(string slug)
    {
        return await _mediator.Send(new GetCourseBySlugQuery(slug));
    }

    public async Task<EpisodeDto?> GetEpisodeById(Guid episodeId)
    {
        return await _mediator.Send(new GetCourseEpisodeCommand(episodeId));
    }
}
