using Common.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreModule.Query._Data.Entities;

[Table("Episodes", Schema = "course")]
class EpisodeQueryModel : Entity
{
    public Guid SectionId { get; internal set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; private set; } = null!;

    [Required]
    [MaxLength(100)]
    public string EnglishTitle { get; private set; } = null!;
    public Guid Token { get; private set; }
    public TimeSpan Time { get; private set; }

    [Required]
    [MaxLength(100)]
    public string VideoName { get; private set; } = null!;

    [MaxLength(50)]
    public string? AttachmentName { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsFree { get; set; }


    [ForeignKey("SectionId")]
    public required SectionQueryModel Section { get; set; }
}
