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

        modelBuilder.Entity<Customer>()
            .Property(c => c.TicksterUserRefNo)
            .HasMaxLength(10);

        modelBuilder.Entity<Customer>()
            .Property(c => c.IdNumber)
            .HasMaxLength(50);

        modelBuilder.Entity<Customer>()
            .Property(c => c.FirstName)
            .HasMaxLength(100);

        modelBuilder.Entity<Customer>()
            .Property(c => c.LastName)
            .HasMaxLength(100);

        modelBuilder.Entity<Customer>()
            .Property(c => c.PostalAddressLineOne)
            .HasMaxLength(255);

        modelBuilder.Entity<Customer>()
            .Property(c => c.PostalAddressLineTwo)
            .HasMaxLength(255);

        modelBuilder.Entity<Customer>()
            .Property(c => c.ZipCode)
            .HasMaxLength(20);

        modelBuilder.Entity<Customer>()
            .Property(c => c.City)
            .HasMaxLength(100);

        modelBuilder.Entity<Customer>()
            .Property(c => c.CountryCode)
            .HasMaxLength(2);

        modelBuilder.Entity<Customer>()
            .Property(c => c.CompanyName)
            .HasMaxLength(200);

        modelBuilder.Entity<Customer>()
            .Property(c => c.MobilePhone)
            .HasMaxLength(20);

        modelBuilder.Entity<Customer>()
            .Property(c => c.Email)
            .HasMaxLength(255);
    }
}
