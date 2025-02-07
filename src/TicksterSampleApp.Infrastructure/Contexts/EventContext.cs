using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Infrastructure.Contexts;

public partial class SampleAppContext : DbContext
{
    public DbSet<Event> Events { get; set; }

    public void OnModelCreatingEvent(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>()
            .Property(e => e.TicksterEventId)
            .HasColumnType("varchar(20)");

        modelBuilder.Entity<Event>()
            .Property(e => e.Name)
            .HasColumnType("varchar(255)");

        modelBuilder.Entity<Event>()
            .Property(e => e.TicksterProductionId)
            .HasColumnType("varchar(20)");

        modelBuilder.Entity<Event>()
            .Property(e => e.ProductionName)
            .HasColumnType("varchar(255)");
    }
}
