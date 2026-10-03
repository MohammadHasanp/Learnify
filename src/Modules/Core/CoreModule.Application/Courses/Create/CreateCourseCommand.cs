using Common.Application;
using Common.Domain.ValueObjects;
using CoreModule.Domain.Courses.Enums;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.Courses.Create;

public record CreateCourseCommand(Guid TeacherId, Guid CategoryId, Guid SubCategoryId, string Title, string Slug, string Description, IFormFile ImageFile,
    IFormFile? VideoFile, int Price, SeoData? SeoData, CourseLevel CourseLevel, CourseActionStatus ActionStatus) : IBaseCommand;