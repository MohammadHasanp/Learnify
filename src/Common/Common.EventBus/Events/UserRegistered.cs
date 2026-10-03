using Common.EventBus.Abstractions;

namespace Common.EventBus.Events;

public class UserRegistered : IntegrationEvent
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public string? Family { get; set; }

    public string Mobile { get; set; } = null!;

    public string? Email { get; set; }

    public string Avatar { get; set; } = null!;

    public string Password { get; set; } = null!;
}

public class UserEdited : IntegrationEvent
{
    public Guid UserId { get; set; }
    public string? Email { get; set; }
    public string Mobile { get; set; } = null!;
    public string? Name { get; set; }
    public string? Family { get; set; }
}

public class NewNotificationIntegrationEvent : IntegrationEvent
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}