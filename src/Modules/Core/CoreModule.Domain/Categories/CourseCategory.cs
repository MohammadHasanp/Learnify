using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utilities;
using CoreModule.Domain.Categories.Services;

namespace CoreModule.Domain.Categories;

public class CourseCategory : AggregateRoot
{
    private CourseCategory() { }

    public string Title { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public Guid? ParentId { get; private set; }


    public CourseCategory(string title, string slug, Guid? parentId, ICourseCategoryService service)
    {
        Guard(title, slug, service);
        Title = title;
        Slug = slug.ToSlug();
        ParentId = parentId;
    }

    public void Edit(string title, string slug, ICourseCategoryService service)
    {
        Guard(title, slug, service);
        Title = title;
        Slug = slug.ToSlug();
    }


    void Guard(string title, string slug, ICourseCategoryService service)
    {
        NullOrEmptyDomainDataException.CheckString((title, nameof(title)), (slug, nameof(slug)));

        if (slug.IsUniCode())
            throw new InvalidDomainDataException("Slug Invalid");

        if (Slug != slug)
            if (service.IsExistsSlug(slug))
                throw new InvalidDomainDataException("slug is Exists");
    }
}
