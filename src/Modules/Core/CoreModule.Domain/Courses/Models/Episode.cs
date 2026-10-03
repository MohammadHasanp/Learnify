using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utilities;

namespace CoreModule.Domain.Courses.Models;

public class Episode : Entity
{
    public Guid SectionId { get; internal set; }
    public string Title { get; private set; }
    public string EnglishTitle { get; private set; }
    public Guid Token { get; private set; }
    public TimeSpan Time { get; private set; }
    public string VideoName { get; private set; }
    public string? AttachmentName { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsFree { get; set; }

    public Episode(string title, Guid token, TimeSpan time, string videoName, string? attachmentName, bool isActive, string englishTitle,
        bool isFree)
    {
        Guard(videoName, title, englishTitle);
        Title = title;
        Token = token;
        IsFree = isFree;
        Time = time;
        VideoName = videoName;
        AttachmentName = attachmentName;
        IsActive = isActive;
        EnglishTitle = englishTitle;
    }

    public void ToggleStatus()
    {
        IsActive = !IsActive;
    }

    private static void Guard(string videoName, string title, string englishTitle)
    {
        NullOrEmptyDomainDataException.CheckString((videoName, nameof(videoName)), (title, nameof(title)),
            (englishTitle, nameof(englishTitle)));

        if (englishTitle.IsUniCode())
            throw new InvalidDomainDataException("englishTitle Invalid");
    }

    public void Edit(string title, bool isActive, TimeSpan timeSpan, string? attachmentName, bool isFree)
    {
        NullOrEmptyDomainDataException.CheckString((title, nameof(title)));
        Title = title;
        IsActive = isActive;
        IsFree = isFree;
        Time = timeSpan;
        if (!string.IsNullOrWhiteSpace(attachmentName))
        {
            AttachmentName = attachmentName;
        }
    }
}
