using Common.Domain.ValueObjects;
using Common.Query;
using CoreModule.Domain.Courses.Enums;
using CoreModule.Query.CourseCategories.DTOs;
using CoreModule.Query.Teachers.DTOs;

namespace CoreModule.Query.Courses.DTOs;

public class CourseDto : BaseDto
{
    public Guid TeacherId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SubCategoryId { get; set; }
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageName { get; set; } = null!;
    public string? VideoName { get; set; }
    public CourseLevel CourseLevel { get; set; }
    public CourseStatus CourseStatus { get; set; }
    public CourseActionStatus ActionStatus { get; set; }
    public int Price { get; set; }
    public DateTimeOffset LastUpdate { get; set; }
    public SeoData? SeoData { get; set; }
    public TeacherDto Teacher { get; set; } = null!;
    public List<CourseSectionDto> Sections { get; set; } = [];
    public CourseCategoryDto MainCategory { get; set; } = new();
    public CourseCategoryDto SubCategory { get; set; } = new();
    public string GetDuration()
    {
        int totalSeconds = 0;
        int totalMinutes = 0;
        int totalHours = 0;

        foreach (var section in Sections)
        {
            foreach (var item in section.Episodes)
            {
                totalHours += item.Time.Hours;
                totalMinutes += item.Time.Minutes;
                totalSeconds += item.Time.Seconds;
            }
            while (totalSeconds > 60)
            {
                totalMinutes += 1;
                totalSeconds -= 60;
            }

            while (totalMinutes >= 60)
            {
                totalHours += 1;
                totalMinutes -= 60;
            }

        }

        return $"{totalHours:00} : {totalMinutes:00} : {totalSeconds:00}";
    }

    public string GetCourseStatus()
    {
        switch (CourseStatus)
        {
            case CourseStatus.Completed:
                return "به اتمام رسیده";

            case CourseStatus.StartSoon:
                return "شروع به زودی";

            case CourseStatus.InProgress:
                return "درحال برگزاری";

            default:
                return "";
        }
    }

    public string GetCourseLevel()
    {
        switch (CourseLevel)
        {
            case CourseLevel.Expert:
                return "پیشرفته";

            case CourseLevel.Intermediate:
                return "از مقدماتی تا پیشرفته";

            case CourseLevel.Beginner:
                return "مقدماتی";

            default:
                return "";
        }
    }
}

public class CourseSectionDto : BaseDto
{
    public Guid CourseId { get; set; }
    public string Title { get; set; } = null!;
    public int DisplayOrder { get; set; }
    public List<CourseEpisodeDto> Episodes { get; set; } = [];
}

public class CourseEpisodeDto : BaseDto
{
    public Guid SectionId { get; set; }
    public string Title { get; set; } = null!;
    public string EnglishTitle { get; set; } = null!;
    public Guid Token { get; set; }
    public TimeSpan Time { get; set; }
    public string VideoName { get; set; } = null!;
    public string? AttachmentName { get; set; }
    public bool IsActive { get; set; }
    public bool IsFree { get; set; }
}

public class CourseFilterData : BaseDto
{
    public string ImageName { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public int Price { get; set; }
    public SearchByPrice SearchByPrice { get; set; } = SearchByPrice.All;
    public CourseStatus CourseStatus { get; set; }
    public CourseActionStatus ActionStatus { get; set; }
    public string TeacherName { get; set; } = null!;
    public List<CourseSectionDto> Sections { get; set; } = [];

    public string GetDuration()
    {
        int totalSeconds = 0;
        int totalMinutes = 0;
        int totalHours = 0;

        foreach (var section in Sections)
        {
            foreach (var item in section.Episodes)
            {
                totalHours += item.Time.Hours;
                totalMinutes += item.Time.Minutes;
                totalSeconds += item.Time.Seconds;
            }
            while (totalSeconds > 60)
            {
                totalMinutes += 1;
                totalSeconds -= 60;
            }

            while (totalMinutes >= 60)
            {
                totalHours += 1;
                totalMinutes -= 60;
            }

        }

        return $"{totalHours:00} : {totalMinutes:00} : {totalSeconds:00}";
    }
    public int EpisodeCount => Sections.Sum(s => s.Episodes.Count);
}

public class CourseFilterParams : BaseFilterParam
{
    public Guid? TeacherId { get; set; }
    public string? Search { get; set; }
    public string? CategorySlug { get; set; } = null!;
    public SearchByPrice SearchByPrice { get; set; } = SearchByPrice.All;
    public CourseStatus? CourseStatus { get; set; }
    public CourseLevel? CourseLevel { get; set; }
    public CourseActionStatus? ActionStatus { get; set; }
    public CourseFilterSort FilterSort { get; set; } = CourseFilterSort.Latest;
}

public class CourseFilterResult : BaseFilter<CourseFilterData>
{

}

public enum CourseFilterSort
{
    Latest,
    Oldest,
    Expensive
}

public enum SearchByPrice
{
    Free,
    NotFree,
    All,
}