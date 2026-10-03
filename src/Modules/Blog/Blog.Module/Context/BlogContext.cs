using BlogModule.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogModule.Context;

class BlogContext(DbContextOptions<BlogContext> options) : DbContext(options)
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasIndex(p => p.Slug).IsUnique(true);
        base.OnModelCreating(modelBuilder);
    }
}
