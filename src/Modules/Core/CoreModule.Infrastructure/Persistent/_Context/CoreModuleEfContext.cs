using Common.Domain;
using CoreModule.Domain.Categories;
using CoreModule.Domain.Courses.Models;
using CoreModule.Domain.EntityHelpers.CourseStudents;
using CoreModule.Domain.Orders.Models;
using CoreModule.Domain.Teachers;
using CoreModule.Infrastructure.Persistent.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Infrastructure.Persistent._Context;

public class CoreModuleEfContext(DbContextOptions<CoreModuleEfContext> option, IMediator publisher) : DbContext(option)
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<CourseCategory> CourseCategories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<CourseStudent> CourseStudents { get; set; }
    public DbSet<Order> Orders { get; set; }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var modifiedEntities = GetModifiedEntities();
        await PublishEvents(modifiedEntities);
        return await base.SaveChangesAsync(cancellationToken);
    }
    private List<AggregateRoot> GetModifiedEntities() =>
        ChangeTracker.Entries<AggregateRoot>()
            .Where(x => x.State != EntityState.Detached)
            .Select(c => c.Entity)
            .Where(c => c.BaseDomainEvent.Any()).ToList();

    private async Task PublishEvents(List<AggregateRoot> modifiedEntities)
    {
        foreach (var entity in modifiedEntities)
        {
            var events = entity.BaseDomainEvent;
            foreach (var domainEvent in events)
            {
                await publisher.Publish(domainEvent);
            }
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoreModuleEfContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
