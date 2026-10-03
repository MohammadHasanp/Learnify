using Common.Application;
using Common.Application.FileUtil.StorageInterfaces;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Courses.Repository;

namespace CoreModule.Application.Courses.Episodes.Delete;

public class DeleteCourseEpisodeHandler(ICourseRepository repository, ILocalFileService fileService) : IBaseCommandHandler<DeleteCourseEpisodeCommand>
{
    private readonly ICourseRepository _repository = repository;
    private readonly ILocalFileService _fileService = fileService;

    public async Task<OperationResult> Handle(DeleteCourseEpisodeCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetTracking(request.CourseId);
        if (course == null)
            return OperationResult.NotFound();

        var episode = course.DeleteEpisode(request.EpisodeId);
        await _repository.Save();
        _fileService.DeleteDirectory(CoreModuleDirectories.CourseEpisode(request.CourseId,episode.Token));
        return OperationResult.Success();
    }
}