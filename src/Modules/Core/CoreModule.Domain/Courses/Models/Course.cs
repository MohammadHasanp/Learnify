
using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utilities;
using Common.Domain.ValueObjects;
using CoreModule.Domain.Courses.Enums;
using CoreModule.Domain.Courses.Service;

namespace CoreModule.Domain.Courses.Models;

public class Course : AggregateRoot
{
    private Course() { }
    public Guid TeacherId { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid SubCategoryId { get; private set; }
    public string Title { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string ImageName { get; private set; } = null!;
    public string? VideoName { get; private set; }
    public CourseStatus CourseStatus { get; private set; }
    public CourseLevel CourseLevel { get; private set; }
    public CourseActionStatus ActionStatus { get; private set; }
    public int Price { get; private set; }
    public DateTimeOffset LastUpdate { get; private set; }
    public SeoData? SeoData { get; private set; }

    public List<Section> Sections { get; private set; } = [];

    public Course(Guid teacherId, string title, string description, string imageName, string? videoName, ICourseService service,
        CourseLevel courseLevel, int price, SeoData? seoData, Guid subCategoryId, Guid categoryId, string slug, CourseActionStatus actionStatus)
    {
        Guard(title, description, imageName, slug, service);
        TeacherId = teacherId;
        Title = title;
        Description = description;
        ImageName = imageName;
        VideoName = videoName;
        CourseStatus = CourseStatus.StartSoon;
        CourseLevel = courseLevel;
        Price = price;
        ActionStatus = actionStatus;
        LastUpdate = DateTimeOffset.UtcNow;
        SeoData = seoData;
        Sections = [];
        SubCategoryId = subCategoryId;
        CategoryId = categoryId;
        Slug = slug;
    }

    public void Edit(string title, string description, string imageName, string? videoName, ICourseService service,
        CourseLevel courseLevel, CourseStatus courseStatus, int price, SeoData? seoData, Guid subCategoryId, Guid categoryId, string slug, CourseActionStatus actionStatus)
    {
        Guard(title, description, imageName, slug, service);
        Title = title;
        Description = description;
        ImageName = imageName;
        VideoName = videoName;
        CourseStatus = courseStatus;
        CourseLevel = courseLevel;
        ActionStatus = actionStatus;
        Price = price;
        LastUpdate = DateTimeOffset.UtcNow;
        SeoData = seoData;
        SubCategoryId = subCategoryId;
        CategoryId = categoryId;
        Slug = slug;
    }
    public void AddSection(string title, int displayOrder)
    {
        if (Sections.Any(s => s.Title == title))
            throw new InvalidDomainDataException("title Is Exsist");

        Sections.Add(new Section(title, displayOrder)
        {
            CourseId = Id
        });
    }

    public void RemoveSection(Guid sectionId)
    {
        var section = Sections.FirstOrDefault(s => s.Id == sectionId);
        if (section == null)
            throw new NullOrEmptyDomainDataException("section NotFound");

        Sections.Remove(section);
    }

    public void EditSection(Guid sectionId, string title, int displayOrder)
    {
        var section = Sections.FirstOrDefault(s => s.Id == sectionId);
        if (section == null)
            throw new NullOrEmptyDomainDataException("Section NotFound");

        section.Edit(title, displayOrder);
    }

    public Episode AddEpisode(Guid sectionId, string title, Guid token, TimeSpan time, string videoExtension,
        string? attachmentExtension, bool isActive, string englishTitle, bool isFree)
    {
        var section = Sections.FirstOrDefault(s => s.Id == sectionId);
        if (section == null)
            throw new NullOrEmptyDomainDataException("section NotFound");

        var episodeCount = Sections.Sum(s => s.Episodes.Count);
        var episodeTitle = $"{episodeCount + 1}_{englishTitle}";

        string? attName = null;
        if (!string.IsNullOrWhiteSpace(attachmentExtension))
            attName = $"{episodeTitle}{attachmentExtension}";

        var vidName = $"{episodeTitle}{videoExtension}";

        if (isActive)
        {
            LastUpdate = DateTimeOffset.UtcNow;

            if (CourseStatus == CourseStatus.StartSoon)
                CourseStatus = CourseStatus.InProgress;
        }



        var episode = section.AddEpisode(title, token, time, vidName, attName, isActive, englishTitle, isFree);
        return episode;
    }

    public void EditEpisode(Guid episodeId, Guid sectionId, string title, bool isActive, TimeSpan timeSpan, string? attachmentName, bool isFree)
    {
        var section = Sections.FirstOrDefault(f => f.Id == sectionId);
        if (section == null) throw new InvalidDomainDataException("Section NotFound");

        var episode = section.Episodes.FirstOrDefault(f => f.Id == episodeId);
        if (episode == null) throw new InvalidDomainDataException("episode NotFound");

        episode.Edit(title, isActive, timeSpan, attachmentName, isFree);
    }

    public void AcceptEpisode(Guid episodeId)
    {
        var section = Sections.FirstOrDefault(s => s.Episodes.Any(e => e.Id == episodeId && !e.IsActive));
        if (section == null)
            throw new NullOrEmptyDomainDataException("section NotFound");

        var episode = section.Episodes.First(e => e.Id == episodeId);

        episode.ToggleStatus();
        LastUpdate = DateTimeOffset.UtcNow;
    }

    public Episode DeleteEpisode(Guid episodeId)
    {
        var section = Sections.FirstOrDefault(s => s.Episodes.Any(e => e.Id == episodeId));
        if (section == null)
            throw new NullOrEmptyDomainDataException("section NotFound");

        var episode = section.Episodes.First(e => e.Id == episodeId);

        section.Episodes.Remove(episode);
        return episode;
    }

    public Episode? GetEpisodeById(Guid episodeId)
    {
        var section = Sections.FirstOrDefault(f => f.Episodes.Any(e => e.Id == episodeId));
        return section?.Episodes.FirstOrDefault(f => f.Id == episodeId);
    }

    private void Guard(string title, string description, string imageName, string slug, ICourseService service)
    {
        NullOrEmptyDomainDataException.CheckString((title, nameof(title)), (description, nameof(description)), (imageName, nameof(imageName)));

        if (Slug != slug)
            if (service.IsExistsSlug(slug))
                throw new InvalidDomainDataException("slug Invalid");

        if (slug.IsUniCode())
            throw new InvalidDomainDataException("slug Invalid");
    }
}
