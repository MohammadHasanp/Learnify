using Common.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogModule.Domain;

[Table("Categories", Schema = "dbo")]
class Category : Entity
{
    [Required]
    [MaxLength(80)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(80)]
    public string Slug { get; set; } = null!;
}
