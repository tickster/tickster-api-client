using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Infrastructure.Contexts;

public partial class SampleAppContext : DbContext
{
    public DbSet<Restaurant> Restaurants { get; set; }   

    public void OnModelCreatingRestaurant(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Restaurant>()
        //    .HasMany(r => r.Goods)
        //    .WithOne(g => g.Restaurant)
        //    .IsRequired(false);

        modelBuilder.Entity<Restaurant>()
            .Property(r => r.RestaurantName)
            .HasColumnType("varchar(255)");
    }
}
