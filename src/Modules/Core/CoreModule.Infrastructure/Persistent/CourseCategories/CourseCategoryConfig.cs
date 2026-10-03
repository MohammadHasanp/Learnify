using CoreModule.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreModule.Infrastructure.Persistent.CourseCategories;

public class CourseCategoryConfig : IEntityTypeConfiguration<CourseCategory>
{
    public void Configure(EntityTypeBuilder<CourseCategory> builder)
    {
        builder.ToTable("Categories", "course");
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Slug).IsUnique();

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.Slug)
            .HasMaxLength(500);

        builder.HasMany<CourseCategory>()
            .WithOne().OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(c => c.ParentId);
    }
}
