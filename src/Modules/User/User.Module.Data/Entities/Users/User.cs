using System.ComponentModel.DataAnnotations;
using Common.Domain;

namespace User.Module.Data.Entities.Users;

public class User : Entity
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

    public List<UserRole> UserRoles { get; set; } = [];
}
