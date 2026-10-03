using Common.Application;
using Common.Application.FileUtil.StorageInterfaces;
using Common.Application.FileUtil.Validations;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Courses.Models;
using CoreModule.Domain.Courses.Repository;
using CoreModule.Domain.Courses.Service;

namespace CoreModule.Application.Courses.Create;

public class CreateCourseHandler(ICourseRepository repository, ICourseService service, ILocalFileService fileService) :
    IBaseCommandHandler<CreateCourseCommand>
{
    private readonly ICourseRepository _courseRepository = repository;
    private readonly ICourseService _courseService = service;
    private readonly ILocalFileService _fileService = fileService;
    public async Task<OperationResult> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, CoreModuleDirectories.CourseImages);

        string? videoName = null;
        var courseId = Guid.NewGuid();

        if (request.VideoFile != null)
            if (request.VideoFile.IsValidVideoFile())
            {
                videoName = await _fileService.SaveFileAndGenerateName(request.VideoFile, CoreModuleDirectories.CourseVideos(courseId));
            }
            else
                return OperationResult.Error("فایل وارد شده نامعتبر است");

        var course = new Course(request.TeacherId, request.Title, request.Description, imageName, videoName, _courseService, request.CourseLevel,
            request.Price, request.SeoData, request.SubCategoryId, request.CategoryId, request.Slug, request.ActionStatus)
        {
            Id = courseId,
        };

        _courseRepository.Add(course);
        await _courseRepository.Save();
        return OperationResult.Success();
    }
}