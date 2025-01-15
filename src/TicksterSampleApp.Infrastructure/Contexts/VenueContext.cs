using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Infrastructure.Contexts;

public partial class SampleAppContext : DbContext
{
    public DbSet<Venue> Venue { get; set; }

    public void OnModelCreatingVenue(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Venue>()
            .HasMany(v => v.Restaurants)
            .WithOne(r => r.Venue)
            .HasForeignKey(r => r.VenueId)
            .HasPrincipalKey(v => v.Id);
    }
}
