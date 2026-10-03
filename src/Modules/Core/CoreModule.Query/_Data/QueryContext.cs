using CoreModule.Query._Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query._Data;

class QueryContext(DbContextOptions<QueryContext> option) : DbContext(option)
{
    public DbSet<UserQueryModel> Users { get; set; }
    public DbSet<TeacherQueryModel> Teachers { get; set; }
    public DbSet<CourseCategoryQueryModel> CourseCategories { get; set; }
    public DbSet<CourseQueryModel> Courses { get; set; }
    public DbSet<EpisodeQueryModel> Episodes { get; set; }
    public DbSet<SectionQueryModel> Sections { get; set; }
    public DbSet<OrderQueryModel> Orders { get; set; }
    public DbSet<OrderItemQueryModel> OrderItems { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseQueryModel>(builder =>
        {
            builder.OwnsOne(b => b.SeoData, config =>
            {
                config.Property(b => b.MetaDescription)
                    .HasMaxLength(500)
                    .HasColumnName("MetaDescription");

                config.Property(b => b.MetaTitle)
                    .HasMaxLength(500)
                    .HasColumnName("MetaTitle");

                config.Property(b => b.MetaKeyWords)
                    .HasMaxLength(500)
                    .HasColumnName("MetaKeyWords");

                config.Property(b => b.IndexPage)
                    .HasColumnName("IndexPage");

                config.Property(b => b.Canonical)
                    .HasMaxLength(500)
                    .HasColumnName("Canonical");

                config.Property(b => b.Schema)
                    .HasColumnName("Schema");
            });
        });

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        throw new NotSupportedException();
    }
}
