using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Infrastructure.Contexts;

public partial class SampleAppContext : DbContext
{
    public DbSet<PurchaseCampaign> PurchaseCampaignLookup { get; set; }

    private void OnModelCreatingPurchaseCampaign(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PurchaseCampaign>()
            .HasKey(pc => new { pc.PurchaseId, pc.CampaignId });
    }
}
