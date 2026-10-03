using Common.Domain;
using Common.Domain.Exceptions;
using static Common.Domain.Exceptions.BaseDomainExceotion;

namespace CoreModule.Domain.Courses.Models;

public class Section : Entity
{
    public Guid CourseId { get; internal set; }
    public string Title { get; private set; } = null!;
    public int DisplayOrder { get; private set; }

    public List<Episode> Episodes { get; private set; } = [];

    public Section(string title, int displayOrder)
    {
        Guard(title, displayOrder);
        Title = title;
        DisplayOrder = displayOrder;
        Episodes = [];
    }

    public void Edit(string title, int displayOrder)
    {
        Guard(title, displayOrder);
        Title = title;
        DisplayOrder = displayOrder;
    }
    public Episode AddEpisode(string title, Guid token, TimeSpan time, string videoName, string? attachmentName, bool isActive, string englishTitle, bool isFree)
    {
        if (Episodes.Any(e => e.Title == title))
            throw new InvalidDomainDataException("title Is Exists");

        var episode = new Episode(title, token, time, videoName, attachmentName, isActive, englishTitle, isFree)
        {
            SectionId = Id
        };
        Episodes.Add(episode);
        return episode;
    }

    void Guard(string title, int DisplayOrder)
    {
        NullOrEmptyDomainDataException.CheckString((title, nameof(title)));
        if (DisplayOrder < 0)
            throw new InvalidDomainDataException("DisplayOrder Invalid");
    }
}
