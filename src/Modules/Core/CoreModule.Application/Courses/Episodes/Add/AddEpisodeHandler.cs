using Common.Application;
using Common.Application.FileUtil.StorageInterfaces;
using Common.Application.FileUtil.Validations;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Courses.Models;
using CoreModule.Domain.Courses.Repository;

namespace CoreModule.Application.Courses.Episodes.Add;

public class AddEpisodeHandler(ICourseRepository courseRepository, ILocalFileService fileService) : IBaseCommandHandler<AddEpisodeCommand>
{
    private readonly ICourseRepository _courseRepository = courseRepository;
    private readonly ILocalFileService _fileService = fileService;

    public async Task<OperationResult> Handle(AddEpisodeCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetTracking(request.CourseId);
        if (course == null)
            return OperationResult.NotFound();

        string? attExName = null;
        if (request.AttachmentFile != null && request.AttachmentFile.IsValidCompressFile())
            attExName = Path.GetExtension(request.AttachmentFile.FileName);

        var episode = course.AddEpisode(request.SectionId, request.Title, Guid.NewGuid(), request.Time
        , Path.GetExtension(request.VideoFile.FileName), attExName, request.IsActive, request.EnglishTitle, request.IsFree);

        await SaveFiles(request, episode);
        await _courseRepository.Save();
        return OperationResult.Success();
    }

    private async Task SaveFiles(AddEpisodeCommand request, Episode episode)
    {

        await _fileService.SaveFile(request.VideoFile,
            CoreModuleDirectories.CourseEpisode(request.CourseId, episode.Token), episode.VideoName);

        if (request.AttachmentFile != null && request.AttachmentFile.IsValidCompressFile())
            await _fileService.SaveFile(request.AttachmentFile,
                CoreModuleDirectories.CourseEpisode(request.CourseId, episode.Token), episode.AttachmentName!);
    }
}