using Common.Application;
using Common.Application.FileUtil.StorageInterfaces;
using Common.Application.FileUtil.Validations;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Courses.Models;
using CoreModule.Domain.Courses.Repository;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.Courses.Episodes.Edit;

internal class EditEpisodeCommandHandler(ICourseRepository repository, ILocalFileService localFileService) : IBaseCommandHandler<EditEpisodeCommand>
{
    public async Task<OperationResult> Handle(EditEpisodeCommand request, CancellationToken cancellationToken)
    {
        var course = await repository.GetTracking(request.CourseId);
        if (course == null)
        {
            return OperationResult.NotFound();
        }

        var episode = course.GetEpisodeById(request.EpisodeId);
        if (episode == null)
        {
            return OperationResult.NotFound();
        }

        string? attname = null;
        if (request.AttachmentFile != null)
        {
            await SaveAttachment(request.AttachmentFile, episode, course.Id);
        }
        if (request.VideoFile != null)
        {
            await SaveVideoFile(request.VideoFile, episode, course.Id);
        }
        course.EditEpisode(request.EpisodeId, request.SectionId, request.Title, request.IsActive, request.TimeSpan, attname, request.IsFree);
        await repository.Save();
        return OperationResult.Success();
    }

    private async Task<string?> SaveAttachment(IFormFile attachment, Episode episode, Guid courseId)
    {
        if (!attachment.IsValidCompressFile()) return null;

        var attName = episode.VideoName.Replace(".mp4", Path.GetExtension(attachment.FileName));
        await localFileService.SaveFile(attachment, CoreModuleDirectories.CourseEpisode(courseId, episode.Token), attName);
        return attName;


    }
    private async Task SaveVideoFile(IFormFile videoFile, Episode episode, Guid courseId)
    {
        if (videoFile.IsValidVideoFile())
        {
            await localFileService.SaveFile(videoFile, CoreModuleDirectories.CourseEpisode(courseId, episode.Token), episode.VideoName);
        }
    }

}