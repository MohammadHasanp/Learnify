using Common.Domain;
using System.ComponentModel.DataAnnotations;

namespace TicketModule.Domain;

class Ticket : Entity
{
    public Guid UserId { get; set; }

    [MaxLength(80)]
    [Required]
    public string OwnerFullName { get; set; } = null!;


    [MaxLength(11)]
    [Required]
    public string Mobile { get; set; } = null!;

    [MaxLength(100)]
    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Text { get; set; } = null!;
    public TicketStatus TicketStatus { get; set; }
    public List<TicketMessage> Messages { get; set; } = [];
}

public enum TicketStatus
{
    Pending,
    Answered,
    Closed,
}
