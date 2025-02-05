using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Enums;
using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Infrastructure.Contexts;

public partial class SampleAppContext : DbContext
{
    public DbSet<Purchase> Purchases { get; set; }

    public void OnModelCreatingPurchase(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Purchase>()
            .Property(p => p.TicksterPurchaseRefNo)
            .HasColumnType("varchar(10)");

        modelBuilder.Entity<Purchase>()
            .Property(p => p.Currency)
            .HasColumnType("char(3)");

        modelBuilder.Entity<Purchase>()
            .Property(p => p.DiscountCode)
            .HasColumnType("varchar(255)");

        modelBuilder.Entity<Purchase>()
            .Property(p => p.DiscountCodeName)
            .HasColumnType("varchar(255)");

        modelBuilder.Entity<Purchase>()
            .Property(p => p.EogRequestCode)
            .HasColumnType("varchar(20)");

        modelBuilder.Entity<Purchase>()
            .Property(p => p.PrivacyRefNo)
            .HasColumnType("varchar(5)");

        modelBuilder.Entity<Purchase>()
            .Property(p => p.TermsRefNo)
            .HasColumnType("varchar(5)");

        modelBuilder.Entity<Purchase>()
            .Property(p => p.Status)
            .HasColumnType("varchar(20)")
            .HasConversion(v => v.ToString(),
            v => (Status)Enum.Parse(typeof(Status), v));

        modelBuilder.Entity<Purchase>()
            .Property(p => p.Channel)
            .HasColumnType("varchar(20)")
            .HasConversion(v => v.ToString(),
            v => (Channel)Enum.Parse(typeof(Channel), v));
    }
}
