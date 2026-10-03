using Common.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreModule.Query._Data.Entities;

[Table("Users", Schema = "dbo")]
class UserQueryModel : Entity
{
    [MaxLength(50)]
    public string? Name { get; set; }

    [MaxLength(50)]
    public string? Family { get; set; }

    [MaxLength(11)]
    [Required]
    public string Mobile { get; set; } = null!;

    [MaxLength(50)]
    public string? Email { get; set; }

    [MaxLength(100)]
    [Required]
    public string Avatar { get; set; } = null!;

    [MaxLength(70)]
    [Required]
    public string Password { get; set; } = null!;

    public string? FullName
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(Family))
                return null;
            return Name + " " + Family;
        }
    }
}