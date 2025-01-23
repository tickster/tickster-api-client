using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Infrastructure.Contexts;

public partial class SampleAppContext : DbContext
{
    public DbSet<Venue> Venues { get; set; }

    public void OnModelCreatingVenue(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Venue>()
        //    .HasMany(v => v.Restaurants)
        //    .WithOne(r => r.Venue)
        //    .IsRequired(false);

        modelBuilder.Entity<Venue>()
            .Property(v => v.TicksterVenueId)
            .HasColumnType("varchar(20)");

        modelBuilder.Entity<Venue>()
            .Property(v => v.Name)
            .HasColumnType("varchar(255)");

        modelBuilder.Entity<Venue>()
            .Property(v => v.Address)
            .HasColumnType("varchar(255)");

        modelBuilder.Entity<Venue>()
            .Property(v => v.ZipCode)
            .HasColumnType("varchar(20)");

        modelBuilder.Entity<Venue>()
            .Property(v => v.City)
            .HasColumnType("varchar(100)");

        modelBuilder.Entity<Venue>()
            .Property(v => v.CountryCode)
            .HasColumnType("char(2)");

        modelBuilder.Entity<Venue>()
            .Property(v => v.Latitude)
            .HasColumnType("decimal(6, 2)");

        modelBuilder.Entity<Venue>()
            .Property(v => v.Longitude)
            .HasColumnType("decimal(6, 2)");
    }
}
