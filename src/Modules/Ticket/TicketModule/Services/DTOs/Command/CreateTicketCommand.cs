namespace TicketModule.Services.DTOs.Command;

public class CreateTicketCommand
{
    public Guid UserId { get; set; }
    public string OwnerFullName { get; set; } = null!;
    public string Mobile { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
}
