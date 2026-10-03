using CommentModule.Domain;
using Microsoft.EntityFrameworkCore;

namespace CommentModule.Context;

class CommentContext(DbContextOptions<CommentContext> option) : DbContext(option)
{
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }
}