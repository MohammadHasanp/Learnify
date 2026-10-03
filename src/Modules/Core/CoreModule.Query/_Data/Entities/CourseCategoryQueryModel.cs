using Common.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreModule.Query._Data.Entities;

[Table("Categories", Schema = "Course")]
class CourseCategoryQueryModel : Entity
{
    [Required]
    [MaxLength(100)]
    public string Title { get; private set; } = null!;

    [Required]
    [MaxLength(200)]
    public string Slug { get; private set; } = null!;
    public Guid? ParentId { get; private set; }

    [ForeignKey("ParentId")]
    public List<CourseCategoryQueryModel> Childs { get; set; } = [];
}
