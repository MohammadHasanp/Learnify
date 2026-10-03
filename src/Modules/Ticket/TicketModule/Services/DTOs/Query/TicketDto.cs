using TicketModule.Domain;

namespace TicketModule.Services.DTOs.Query;

public class TicketDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string OwnerFullName { get; set; } = null!;
    public string Mobile { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTimeOffset CreationDate { get; set; }
    public TicketStatus TicketStatus { get; set; }
    public List<TicketMessageDto> Messages { get; set; } = [];
}
