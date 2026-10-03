using Microsoft.EntityFrameworkCore;
using User.Module.Data.Entities.Roles;
using User.Module.Data.Entities.UserNotifications;
using User.Module.Data.Entities.Users;

namespace User.Module.Data.Context;

public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
{
    public DbSet<Entities.Users.User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserNotification> Notifications { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
}
