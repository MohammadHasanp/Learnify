using Common.Infrastructure;
using CoreModule.Domain.Categories;
using CoreModule.Domain.Categories.Repository;
using CoreModule.Infrastructure.Persistent._Context;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Infrastructure.Persistent.CourseCategories;

public class CourseCategoryRepository : BaseRepository<CourseCategory, CoreModuleEfContext>, ICourseCategoryRepository
{
    public CourseCategoryRepository(CoreModuleEfContext context) : base(context)
    {
    }

    public async Task Delete(CourseCategory category)
    {
        var categoryHasCourse = await Context.Courses.AnyAsync(f => f.CategoryId == category.Id || f.SubCategoryId == category.Id);

        if (categoryHasCourse)
        {
            throw new Exception("این دسته بندی دارای چندین دوره است");
        }

        var children = await Context.CourseCategories.Where(r => r.ParentId == category.Id).ToListAsync();
        if (children.Any())
        {
            foreach (var child in children)
            {
                var isAnyCourse = await Context.Courses.AnyAsync(f => f.CategoryId == child.Id || f.SubCategoryId == child.Id);

                if (isAnyCourse)
                {
                    throw new Exception("این دسته بندی دارای چندین دوره است");
                }
                else
                {
                    Context.Remove(child);
                }
            }
        }
        Context.Remove(category);
    }
}
