using Common.Application;
using Common.Application.FileUtil.StorageInterfaces;
using Common.Application.FileUtil.Validations;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Courses.Models;
using CoreModule.Domain.Courses.Repository;
using CoreModule.Domain.Courses.Service;

namespace CoreModule.Application.Courses.Edit;


public class EditCourseHandler(ICourseRepository repository, ICourseService service, ILocalFileService fileService) : IBaseCommandHandler<EditCourseCommand>
{
    private readonly ICourseRepository _courseRepository = repository;
    private readonly ICourseService _courseService = service;
    private readonly ILocalFileService _fileService = fileService;
    public async Task<OperationResult> Handle(EditCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetTracking(request.CourseId);
        if (course == null)
        {
            return OperationResult.NotFound();
        }

        var imageName = course.ImageName;
        string videoPath = course.VideoName;

        var oldVideoFileName = course.VideoName;
        var oldImageNameFileName = course.ImageName;
        if (request.VideoFile != null)
        {
            if (request.VideoFile.IsValidFile() == false)
            {
                return OperationResult.Error("فایل وارد شده نامعتبر است");
            }

            videoPath = await _fileService.SaveFileAndGenerateName(request.VideoFile, CoreModuleDirectories.CourseVideos(course.Id));
        }

        if (request.ImageFile.IsImage())
        {
            imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile!, CoreModuleDirectories.CourseImages);
        }


        course.Edit(request.Title, request.Description, imageName, videoPath, _courseService, request.CourseLevel, request.CourseStatus, request.Price,
            request.SeoData, request.SubCategoryId, request.CategoryId, request.Slug, request.ActionStatus);
        await _courseRepository.Save();

        DeleteOldFiles(oldImageNameFileName, oldVideoFileName, request.VideoFile != null, request.ImageFile != null, course);
        return OperationResult.Success();

    }


    void DeleteOldFiles(string image, string? video, bool isUploadNewVideo, bool isUploadNewImage, Course course)
    {
        if (isUploadNewVideo && string.IsNullOrWhiteSpace(video) == false)
        {
            _fileService.DeleteFile(CoreModuleDirectories.CourseVideos(course.Id), video);
        }

        if (isUploadNewImage)
        {
            _fileService.DeleteFile(CoreModuleDirectories.CourseImages, image);
        }
    }
}