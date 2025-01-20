using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Infrastructure.Contexts;

public partial class SampleAppContext : DbContext
{
    public DbSet<ImportLog> ImportLog { get; set; }

    private void OnModelCreatingImportLog(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ImportLog>()
            .HasKey(il => new { il.LastTicksterCrmId, il.Date });
    }
}
