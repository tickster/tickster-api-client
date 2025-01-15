using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Infrastructure.Contexts;

public partial class SampleAppContext : DbContext
{
    public DbSet<Restaurant> Restaurant { get; set; }   

    public void OnModelCreatingRestaurant(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Goods)
            .WithOne(g => g.Restaurant)
            .HasForeignKey(p => p.RestaurantId)
            .HasPrincipalKey(r => r.Id);
    }
}
