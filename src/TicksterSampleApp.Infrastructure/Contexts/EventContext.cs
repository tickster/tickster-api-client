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
            .HasColumnType("varchar(20)");

        modelBuilder.Entity<Event>()
            .Property(e => e.Name)
            .HasColumnType("varchar(255)");

        modelBuilder.Entity<Event>()
            .Property(e => e.Start)
            .HasColumnType("datetime");

        modelBuilder.Entity<Event>()
            .Property(e => e.End)
            .HasColumnType("datetime");

        modelBuilder.Entity<Event>()
            .Property(e => e.LastUpdated)
            .HasColumnType("datetime");

        modelBuilder.Entity<Event>()
            .Property(e => e.TicksterProductionId)
            .HasColumnType("varchar(20)");

        modelBuilder.Entity<Event>()
            .Property(e => e.ProductionName)
            .HasColumnType("varchar(255)");

        modelBuilder.Entity<Event>()
            .Property(e => e.VenueId)
            .HasColumnType("int");
    }
}
