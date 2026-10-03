namespace Common.Domain;

public class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;
    public bool IsDelete { get; set; } = false;
}
