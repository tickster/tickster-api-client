using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Infrastructure.Contexts;

public partial class SampleAppContext : DbContext
{
    public DbSet<Event> Event { get; set; }

    public void OnModelCreatingEvent(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>()
            .HasMany(e => e.Goods)
            .WithOne(g => g.Event)
            .HasForeignKey(g => g.EventId)
            .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Event>()
            .Property(e => e.TicksterEventId)
            .HasMaxLength(20);

        modelBuilder.Entity<Event>()
            .Property(e => e.Name)
            .HasMaxLength(255);

        modelBuilder.Entity<Event>()
            .Property(e => e.TicksterProductionId)
            .HasMaxLength(20);

        modelBuilder.Entity<Event>()
            .Property(e => e.ProductionName)
            .HasMaxLength(255);
    }
}
