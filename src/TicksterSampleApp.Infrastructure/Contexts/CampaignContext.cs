using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Infrastructure.Contexts;

public partial class SampleAppContext : DbContext
{
    public DbSet<Campaign> Campaign { get; set; }

    public void OnModelCreatingCampaign(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campaign>()
            .HasMany(p => p.Purchases)
            .WithOne(c => c.Campaign)
            .HasForeignKey(p => p.CampaignId)
            .HasPrincipalKey(c => c.Id);

        modelBuilder.Entity<Campaign>()
            .Property(c => c.TicksterCampaignId)
            .HasColumnType("varchar(10)");

        modelBuilder.Entity<Campaign>()
            .Property(c => c.TicksterCommunicationId)
            .HasColumnType("varchar(10)");

        modelBuilder.Entity<Campaign>()
            .Property(c => c.ActivationCode)
            .HasColumnType("varchar(10)");

        modelBuilder.Entity<Campaign>()
            .Property(c => c.TicksterInternalReference)
            .HasColumnType("varchar(255)");
    }
}
