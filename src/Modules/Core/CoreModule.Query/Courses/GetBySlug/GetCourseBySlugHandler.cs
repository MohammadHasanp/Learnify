using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.CourseCategories.DTOs;
using CoreModule.Query.Courses.DTOs;
using CoreModule.Query.DTOs;
using CoreModule.Query.Teachers.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Courses.GetBySlug;

class GetCourseBySlugHandler(QueryContext context) : IQueryHandler<GetCourseBySlugQuery, CourseDto?>
{
    private readonly QueryContext _context = context;

    public async Task<CourseDto?> Handle(GetCourseBySlugQuery request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses.
            Include(v => v.Category).
            Include(l => l.SubCategory)
            .Include(t => t.Teacher.Users)
            .Include(c => c.Sections)
            .ThenInclude(r => r.Episodes)
            .FirstOrDefaultAsync(u => u.Slug == request.Slug, cancellationToken);

        if (course == null)
            return null;

        return new CourseDto()
        {
            Id = course.Id,
            CreationDate = course.CreationDate,
            IsDelete = course.IsDelete,
            TeacherId = course.TeacherId,
            CategoryId = course.CategoryId,
            SubCategoryId = course.SubCategoryId,
            Title = course.Title,
            Slug = course.Slug,
            Description = course.Description,
            ImageName = course.ImageName,
            VideoName = course.VideoName,
            CourseLevel = course.CourseLevel,
            CourseStatus = course.CourseStatus,
            Price = course.Price,
            LastUpdate = course.LastUpdate,
            SeoData = course.SeoData,
            ActionStatus = course.ActionStatus,
            Sections = course.Sections.Select(f => new CourseSectionDto
            {
                Id = f.Id,
                CreationDate = f.CreationDate,
                IsDelete = f.IsDelete,
                CourseId = f.CourseId,
                Title = f.Title,
                DisplayOrder = f.DisplayOrder,
                Episodes = f.Episodes.Select(e => new CourseEpisodeDto
                {
                    Id = e.Id,
                    CreationDate = e.CreationDate,
                    IsDelete = e.IsDelete,
                    SectionId = e.SectionId,
                    Title = e.Title,
                    EnglishTitle = e.EnglishTitle,
                    Token = e.Token,
                    Time = e.Time,
                    VideoName = e.VideoName,
                    AttachmentName = e.AttachmentName,
                    IsActive = e.IsActive,
                    IsFree = e.IsFree,
                }).ToList()
            }).ToList(),
            Teacher = new TeacherDto
            {
                Id = course.Teacher.Id,
                CreationDate = course.Teacher.CreationDate,
                IsDelete = course.Teacher.IsDelete,
                UserName = course.Teacher.UserName,
                CvFileName = course.Teacher.CvFileName,
                TeacherStatus = course.Teacher.TeacherStatus,
                User = new CoreModuleUserDto
                {
                    Id = course.Teacher.Users.Id,
                    CreationDate = course.Teacher.Users.CreationDate,
                    IsDelete = course.Teacher.Users.IsDelete,
                    Name = course.Teacher.Users.Name,
                    Family = course.Teacher.Users.Family,
                    Email = course.Teacher.Users.Email,
                    Avatar = course.Teacher.Users.Avatar,
                    Mobile = course.Teacher.Users.Mobile
                }
            },
            MainCategory = new CourseCategoryDto
            {
                Id = course.Category.Id,
                CreationDate = course.Category.CreationDate,
                IsDelete = course.Category.IsDelete,
                Title = course.Category.Title,
                Slug = course.Category.Slug,
                ParentId = course.Category.ParentId,
                Childs = null
            },
            SubCategory = new CourseCategoryDto
            {
                Id = course.SubCategory.Id,
                CreationDate = course.SubCategory.CreationDate,
                IsDelete = course.SubCategory.IsDelete,
                Title = course.SubCategory.Title,
                Slug = course.SubCategory.Slug,
                ParentId = course.SubCategory.ParentId,
                Childs = null
            }
        };
    }
}