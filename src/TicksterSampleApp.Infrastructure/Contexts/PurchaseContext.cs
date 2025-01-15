using Microsoft.EntityFrameworkCore;
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
    }
}
