using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CommentModule.Domain;

using Common.Domain;

[Index("Email", IsUnique = true)]
class User : Entity
{
    public string? Name { get; set; }
    public string? Family { get; set; }
    public string AvatarName { get; set; } = null!;
    public string? Email { get; set; }
}