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
            .HasColumnType("varchar(50)");

        modelBuilder.Entity<Purchase>()
            .Property(p => p.CustomerId)
            .HasColumnType("int");

        modelBuilder.Entity<Purchase>()
            .Property(p => p.CampaignId)
            .HasColumnType("int");

        modelBuilder.Entity<Purchase>()
            .Property(p => p.TicksterPurchaseRefNo)
            .HasColumnType("varchar(10)");

        modelBuilder.Entity<Purchase>()
            .Property(p => p.Created)
            .HasColumnType("datetime");

        modelBuilder.Entity<Purchase>()
            .Property(p => p.LastUpdated)
            .HasColumnType("datetime");

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
            .HasConversion(v => v.ToString(),
            v => (Status)Enum.Parse(typeof(Status), v));

        modelBuilder.Entity<Purchase>()
            .Property(p => p.Channel)
            .HasConversion(v => v.ToString(),
            v => (Channel)Enum.Parse(typeof(Channel), v));
    }
}
