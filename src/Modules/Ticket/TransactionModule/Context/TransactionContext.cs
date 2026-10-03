using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionModule.Domain;

namespace TransactionModule.Context;

public class TransactionContext(DbContextOptions<TransactionContext> options) : DbContext(options)
{
    public DbSet<Transaction> UserTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransactionMapping).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions", "dbo");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Authority)
                .IsRequired(false)
                .IsUnicode(false)
                .HasMaxLength(500);

            builder.Property(b => b.CardPan)
                .IsUnicode(false)
                .HasMaxLength(100);

            builder.Property(b => b.PaymentGateway)
                .HasMaxLength(400);
        }
    }
}

