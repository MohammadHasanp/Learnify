using CoreModule.Domain.Courses.Models;
using CoreModule.Domain.Orders.Models;
using CoreModule.Infrastructure.Persistent.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreModule.Infrastructure.Orders;

internal class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.Property(b => b.DiscountCode)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.OwnsMany(b => b.OrderItems, (t) =>
        {
            t.ToTable("OrderItems");
            t.HasOne<Course>()
                .WithMany()
                .HasForeignKey(b => b.CourseId);
        });

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(b => b.UserId);
    }
}