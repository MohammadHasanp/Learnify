namespace Common.Query;

public class BaseDto
{
    public Guid Id { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public bool IsDelete { get; set; }
}
