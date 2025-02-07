using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Infrastructure.Contexts;

public partial class SampleAppContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public void OnModelCreatingCustomer(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .Property(c => c.TicksterUserRefNo)
            .HasColumnType("varchar(10)");

        modelBuilder.Entity<Customer>()
            .Property(c => c.IdNumber)
            .HasColumnType("varchar(50)");

        modelBuilder.Entity<Customer>()
            .Property(c => c.FirstName)
            .HasColumnType("varchar(100)");

        modelBuilder.Entity<Customer>()
            .Property(c => c.LastName)
            .HasColumnType("varchar(100)");

        modelBuilder.Entity<Customer>()
            .Property(c => c.PostalAddressLineOne)
            .HasColumnType("varchar(255)");

        modelBuilder.Entity<Customer>()
            .Property(c => c.PostalAddressLineTwo)
            .HasColumnType("varchar(255)");

        modelBuilder.Entity<Customer>()
            .Property(c => c.ZipCode)
            .HasColumnType("varchar(20)");

        modelBuilder.Entity<Customer>()
            .Property(c => c.City)
            .HasColumnType("varchar(100)");

        modelBuilder.Entity<Customer>()
            .Property(c => c.CountryCode)
            .HasColumnType("char(2)");

        modelBuilder.Entity<Customer>()
            .Property(c => c.CompanyName)
            .HasColumnType("varchar(200)");

        modelBuilder.Entity<Customer>()
            .Property(c => c.MobilePhone)
            .HasColumnType("varchar(20)");

        modelBuilder.Entity<Customer>()
            .Property(c => c.Email)
            .HasColumnType("varchar(255)");
    }
}
