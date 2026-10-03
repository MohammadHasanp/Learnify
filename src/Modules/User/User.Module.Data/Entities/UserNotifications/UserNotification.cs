using System.ComponentModel.DataAnnotations;
using Common.Domain;

namespace User.Module.Data.Entities.UserNotifications;

public class UserNotification : Entity
{
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(2000)]
    public string Text { get; set; } = null!;
    public bool IsSeen { get; set; }

}
