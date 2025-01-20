using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Enums;
using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Infrastructure.Contexts;

public partial class SampleAppContext : DbContext
{
    public DbSet<Purchase> Purchase { get; set; }

    public void OnModelCreatingPurchase(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Purchase>()
            .HasOne(p => p.Customer)
            .WithMany(c => c.Purchases)
            .HasForeignKey(p => p.CustomerId)
            .HasPrincipalKey(c => c.Id);

        modelBuilder.Entity<Purchase>()
            .HasOne(p => p.Campaign)
            .WithMany(c => c.Purchases)
            .HasForeignKey(p => p.CampaignId)
            .HasPrincipalKey(c => c.Id);

        modelBuilder.Entity<Purchase>()
            .HasMany(g => g.Goods)
            .WithOne(p => p.Purchase)
            .HasForeignKey(g => g.PurchaseId)
            .HasPrincipalKey(p => p.Id);

        modelBuilder.Entity<Purchase>()
            .Property(p => p.TicksterCrmId)
            .HasMaxLength(50);

        modelBuilder.Entity<Purchase>()
            .Property(p => p.TicksterPurchaseRefNo)
            .HasMaxLength(10);

        modelBuilder.Entity<Purchase>()
            .Property(p => p.DiscountCode)
            .HasMaxLength(255);

        modelBuilder.Entity<Purchase>()
            .Property(p => p.DiscountCodeName)
            .HasMaxLength(255);

        modelBuilder.Entity<Purchase>()
            .Property(p => p.Currency)
            .HasMaxLength(3);

        modelBuilder.Entity<Purchase>()
            .Property(p => p.EogRequestCode)
            .HasMaxLength(15);

        modelBuilder.Entity<Purchase>()
            .Property(p => p.PrivacyRefNo)
            .HasMaxLength(5);

        modelBuilder.Entity<Purchase>()
            .Property(p => p.TermsRefNo)
            .HasMaxLength(5);

        modelBuilder.Entity<Purchase>()
            .Property(p => p.Status)
            .HasConversion(v => v.ToString(),
            v => (Status)Enum.Parse(typeof(Status), v));

        modelBuilder.Entity<Purchase>()
            .Property(p => p.Channel)
            .HasConversion(v => v.ToString(),
            v => (Channel)Enum.Parse(typeof(Channel), v));
    }
}
