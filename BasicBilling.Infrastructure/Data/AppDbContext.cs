using BasicBilling.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicBilling.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Bill> Bills => Set<Bill>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();

            entity.HasMany(e => e.Bills)
                  .WithOne(b => b.Client)
                  .HasForeignKey(b => b.ClientId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.BillingPeriod).IsRequired();
            entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Status).HasConversion<string>().IsRequired();
            entity.Property(e => e.ServiceType).HasConversion<string>().IsRequired();

            entity.HasOne(e => e.Payment)
                  .WithOne(p => p.Bill)
                  .HasForeignKey<Payment>(p => p.BillId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AmountPaid).HasColumnType("decimal(18,2)");
        });
    }
}
