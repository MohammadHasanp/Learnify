using Common.Domain;
using System.ComponentModel.DataAnnotations;

namespace TicketModule.Domain;

class TicketMessage : Entity
{
    public Guid UserId { get; set; }
    public Guid TicketId { get; set; }

    [MaxLength(80)]
    [Required]
    public string OwnerFullName { get; set; } = null!;

    [Required]
    public string Text { get; set; } = null!;
}