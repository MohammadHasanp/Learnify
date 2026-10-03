namespace TicketModule.Services.DTOs.Query;

public class TicketMessageDto
{
    public Guid UserId { get; set; }
    public string OwnerFullName { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTimeOffset CreationDate { get; set; }
}
