using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Courses.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Courses.GetByFilter;

internal class GetCourseByFilterHandler(QueryContext queryContext) : IQueryHandler<GetCourseByFilterQuery, CourseFilterResult>
{
    private readonly QueryContext _queryContext = queryContext;
    public async Task<CourseFilterResult> Handle(GetCourseByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        var result = _queryContext.Courses
            .Include(c => c.Sections)
            .ThenInclude(c => c.Episodes)
            .Include(b => b.Category)
            .Include(m => m.SubCategory)
            .Include(d => d.Teacher)
            .ThenInclude(f => f.Users)
            .OrderBy(c => c.LastUpdate).AsQueryable();

        switch (@params.FilterSort)
        {
            case CourseFilterSort.Latest:
                result = result.OrderByDescending(c => c.LastUpdate);
                break;
            case CourseFilterSort.Oldest:
                result = result.OrderByDescending(c => c.Price);
                break;
            case CourseFilterSort.Expensive:
                result = result.OrderBy(c => c.LastUpdate);
                break;
        }

        result = @params.SearchByPrice switch
        {
            SearchByPrice.Free => result.Where(d => d.Price == 0),
            SearchByPrice.NotFree => result.Where(d => d.Price > 0),
            _ => result
        };

        if (@params.CourseLevel != null)
            result = result.Where(e => e.CourseLevel == @params.CourseLevel);

        if (@params.CourseStatus != null)
            result = result.Where(e => e.CourseStatus == @params.CourseStatus);

        if (!string.IsNullOrWhiteSpace(request.FilterParams.CategorySlug))
            result = result.Where(e => e.Category.Slug == @params.CategorySlug || e.SubCategory.Slug == @params.CategorySlug);

        if (@params.TeacherId != null)
            result = result.Where(c => c.TeacherId == @params.TeacherId);

        if (@params.ActionStatus != null)
            result = result.Where(d => d.ActionStatus == @params.ActionStatus);

        if (!string.IsNullOrWhiteSpace(request.FilterParams.Search))
        {
            result = result.Where(r => r.Slug.Contains(request.FilterParams.Search) ||
                                       r.Title.Contains(request.FilterParams.Search));
        }

        var skip = (@params.PageId - 1) * @params.Take;
        var courses = await result.Skip(skip).Take(@params.Take).ToListAsync(cancellationToken);
        var model = new CourseFilterResult
        {
            Datas = courses.Select(c => new CourseFilterData
            {
                Id = c.Id,
                CreationDate = c.CreationDate,
                IsDelete = c.IsDelete,
                ImageName = c.ImageName,
                Title = c.Title,
                Slug = c.Slug,
                ActionStatus = c.ActionStatus,
                Price = c.Price,
                TeacherName = $"{c.Teacher.Users.Name} {c.Teacher.Users.Family}",
                Sections = c.Sections.Select(s => new CourseSectionDto
                {
                    Id = s.Id,
                    CreationDate = s.CreationDate,
                    IsDelete = s.IsDelete,
                    CourseId = s.CourseId,
                    Title = s.Title,
                    DisplayOrder = s.DisplayOrder,
                    Episodes = s.Episodes.Select(e => new CourseEpisodeDto
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
                        IsFree = e.IsFree
                    }).ToList()
                }).ToList()
            }).ToList(),
        };

        model.GeneratePaging(result, @params.Take, @params.PageId);
        return model;
    }
}