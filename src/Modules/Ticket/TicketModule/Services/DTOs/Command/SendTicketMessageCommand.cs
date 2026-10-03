namespace TicketModule.Services.DTOs.Command;

public record SendTicketMessageCommand
{
    public Guid TicketId { get; set; }
    public Guid UserId { get; set; }
    public string OwnerFullName { get; set; } = null!;
    public string Text { get; set; } = null!;
}