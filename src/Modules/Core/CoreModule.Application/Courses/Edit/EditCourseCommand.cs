using Common.Application;
using Common.Domain.ValueObjects;
using CoreModule.Domain.Courses.Enums;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.Courses.Edit;

public record EditCourseCommand(Guid CourseId, Guid CategoryId, Guid SubCategoryId, string Title, string Slug, string Description, IFormFile? ImageFile,
    IFormFile? VideoFile, int Price, SeoData? SeoData, CourseLevel CourseLevel, CourseStatus CourseStatus, CourseActionStatus ActionStatus) : IBaseCommand;