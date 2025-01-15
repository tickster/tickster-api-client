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
    }
}
