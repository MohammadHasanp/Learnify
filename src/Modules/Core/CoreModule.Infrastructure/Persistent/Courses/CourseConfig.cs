using CoreModule.Domain.Courses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreModule.Infrastructure.Persistent.Courses;

public class CourseConfig : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses", "dbo");
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Slug).IsUnique();

        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(c => c.Slug)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(300);

        builder.Property(c => c.ImageName)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.VideoName)
            .IsRequired(false)
            .HasMaxLength(200);

        builder.Property(c => c.Price);


        builder.OwnsOne(b => b.SeoData, config =>
        {
            config.Property(o => o.MetaDescription)
                .HasMaxLength(500)
                .HasColumnName("MetaDescription");

            config.Property(o => o.MetaTitle)
                .HasMaxLength(500)
                .HasColumnName("MetaTitle");

            config.Property(o => o.MetaKeyWords)
                .HasMaxLength(500)
                .HasColumnName("MetaKeyWords");

            config.Property(o => o.IndexPage)
                .HasColumnName("IndexPage");

            config.Property(o => o.Canonical)
                .HasMaxLength(500)
                .HasColumnName("Canonical");

            config.Property(o => o.Schema)
                .HasColumnName("Schema");
        });

        builder.OwnsMany(t => t.Sections, option =>
        {
            option.ToTable("Sections", "course");
            option.HasKey(n => n.Id);

            option.Property(n => n.Id)
                .ValueGeneratedNever();

            option.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(200);

            option.Property(n => n.DisplayOrder)
            .IsRequired();

            option.OwnsMany(p => p.Episodes, e =>
            {
                e.ToTable("Episodes", "course");
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.Token);

                e.Property(x => x.Id)
                    .ValueGeneratedNever();

                e.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

                e.Property(x => x.EnglishTitle)
                .IsRequired()
                .HasMaxLength(100);

                e.Property(x => x.VideoName)
                .IsRequired(false)
                .HasMaxLength(200);

                e.Property(x => x.AttachmentName)
                 .IsRequired(false)
                 .HasMaxLength(100);
            });
        });
    }
}
