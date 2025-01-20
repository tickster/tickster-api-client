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

        modelBuilder.Entity<Venue>()
            .Property(v => v.TicksterVenueId)
            .HasMaxLength(20);

        modelBuilder.Entity<Venue>()
            .Property(v => v.Name)
            .HasMaxLength(255);

        modelBuilder.Entity<Venue>()
            .Property(v => v.Address)
            .HasMaxLength(255);

        modelBuilder.Entity<Venue>()
            .Property(v => v.ZipCode)
            .HasMaxLength(20);

        modelBuilder.Entity<Venue>()
            .Property(v => v.City)
            .HasMaxLength(100);

        modelBuilder.Entity<Venue>()
            .Property(v => v.CountryCode)
            .HasMaxLength(2);

        modelBuilder.Entity<Venue>()
            .Property(v => v.Latitude)
            .HasPrecision(8, 6);

        modelBuilder.Entity<Venue>()
            .Property(v => v.Longitude)
            .HasPrecision(9, 6);
    }
}
