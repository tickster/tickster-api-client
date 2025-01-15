using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Infrastructure.Contexts;

public partial class SampleAppContext : DbContext
{
    public DbSet<Customer> Customer { get; set; }

    public void OnModelCreatingCustomer(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Purchases)
            .WithOne(p => p.Customer)
            .HasForeignKey(p => p.CustomerId)
            .HasPrincipalKey(c => c.Id);
    }
}
