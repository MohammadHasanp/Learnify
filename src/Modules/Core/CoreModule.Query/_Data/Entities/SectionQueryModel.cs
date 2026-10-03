using Common.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreModule.Query._Data.Entities;

[Table("Sections", Schema = "course")]
class SectionQueryModel : Entity
{
    public Guid CourseId { get; internal set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; private set; } = null!;
    public int DisplayOrder { get; private set; }

    public List<EpisodeQueryModel> Episodes { get; private set; } = [];

    [ForeignKey("CourseId")]
    public required CourseQueryModel Course { get; set; }
}
