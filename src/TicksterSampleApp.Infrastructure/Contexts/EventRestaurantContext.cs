using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Infrastructure.Contexts;

public partial class SampleAppContext : DbContext
{
    public DbSet<EventRestaurant> EventRestaurants{ get; set; }

    public void OnModelCreatingEventRestaurant(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventRestaurant>()
            .HasKey(er => new { er.EventId, er.RestaurantId });
    }
}
