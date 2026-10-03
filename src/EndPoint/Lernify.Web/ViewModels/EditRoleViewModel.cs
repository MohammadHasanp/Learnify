using User.Module.Data.Entities._Enum;

namespace Learnify.Web.ViewModels;

public class EditRoleViewModel
{
    public string Title { get; set; } = null!;
    public Guid RoleId { get; set; }
    public List<Permission> Permissions { get; set; } = [];
}