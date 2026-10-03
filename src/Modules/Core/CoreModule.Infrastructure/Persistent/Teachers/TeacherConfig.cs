using CoreModule.Domain.Teachers;
using CoreModule.Infrastructure.Persistent.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreModule.Infrastructure.Persistent.Teachers;

public class TeacherConfig : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("Teachers", "dbo");
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => t.UserName).IsUnique();

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(b => b.UserName)
          .IsRequired()
          .IsUnicode(false)
          .HasMaxLength(50);

        builder.HasOne<User>()
        .WithOne()
        .HasForeignKey<Teacher>(f => f.UserId);
    }
}
