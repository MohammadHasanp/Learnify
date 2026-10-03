namespace BlogModule.Services.DTOs.Command;

public class CreateCategoryCommand
{
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
}

public class EditCategoryCommand
{
    public Guid CategoryId { get; set; }
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
}