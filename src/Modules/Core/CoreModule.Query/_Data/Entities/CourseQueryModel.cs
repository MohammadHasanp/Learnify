using Common.Domain;
using Common.Domain.ValueObjects;
using CoreModule.Domain.Courses.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreModule.Query._Data.Entities;

[Table("Courses", Schema = "dbo")]
class CourseQueryModel : Entity
{
    public Guid TeacherId { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid SubCategoryId { get; private set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; private set; } = null!;

    [Required]
    [MaxLength(200)]
    public string Slug { get; private set; } = null!;

    [Required]
    [MaxLength(1000)]
    public string Description { get; private set; } = null!;

    [Required]
    [MaxLength(100)]
    public string ImageName { get; private set; } = null!;

    [MaxLength(100)]
    public string? VideoName { get; private set; }
    public CourseStatus CourseStatus { get; private set; }
    public CourseActionStatus ActionStatus { get; private set; }
    public CourseLevel CourseLevel { get; private set; }
    public int Price { get; private set; }
    public DateTimeOffset LastUpdate { get; private set; }
    public SeoData? SeoData { get; private set; }

    public List<SectionQueryModel> Sections { get; private set; } = [];

    [ForeignKey("TeacherId")]
    public required TeacherQueryModel Teacher { get; set; }

    [ForeignKey("CategoryId")]
    public required CourseCategoryQueryModel Category { get; set; }

    [ForeignKey("SubCategoryId")]
    public required CourseCategoryQueryModel SubCategory { get; set; }

}
