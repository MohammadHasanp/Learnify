using Common.Query;

namespace CoreModule.Query.DTOs;

public class CoreModuleUserDto : BaseDto
{
    public string? Name { get; set; }
    public string? Family { get; set; }
    public string? Email { get; set; }
    public string Avatar { get; set; } = null!;
    public string Mobile { get; set; } = null!;
}