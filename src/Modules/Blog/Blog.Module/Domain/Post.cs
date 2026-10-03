using Common.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogModule.Domain;

[Table("Posts", Schema = "dbo")]
class Post : Entity
{
    [MaxLength(80)]
    [Required]
    public string Title { get; set; } = null!;
    public Guid UserId { get; set; }

    [MaxLength(80)]
    [Required]
    public string WriterName { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [MaxLength(80)]
    [Required]
    public string Slug { get; set; } = null!;
    public long Visit { get; set; }

    [Required]
    public string ImageName { get; set; } = null!;
    public Guid CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; } = null!;
}
