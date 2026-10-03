using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.CourseCategories.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.CourseCategories.GetById;

internal class GetCourseCategoryByIdHandler(QueryContext context) : IQueryHandler<GetCourseCategoryByIdQuery, CourseCategoryDto?>
{
    private readonly QueryContext _context = context;
    public async Task<CourseCategoryDto?> Handle(GetCourseCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _context.CourseCategories
            .Include(c => c.Childs)
            .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);

        if (course == null)
            return null;

        return new CourseCategoryDto
        {
            Id = course.Id,
            CreationDate = course.CreationDate,
            IsDelete = course.IsDelete,
            Title = course.Title,
            Slug = course.Slug,
            ParentId = course.ParentId,
            Childs = course.Childs.Select(c => new CategoryChildDto
            {
                Id = c.Id,
                CreationDate = c.CreationDate,
                IsDelete = c.IsDelete,
                Title = c.Title,
                Slug = c.Slug,
                ParentId = (Guid)c.ParentId!
            }).ToList()
        };
    }
}