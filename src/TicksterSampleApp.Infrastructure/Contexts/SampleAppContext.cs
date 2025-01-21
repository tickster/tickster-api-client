using Microsoft.EntityFrameworkCore;

namespace TicksterSampleApp.Infrastructure.Contexts;

public partial class SampleAppContext(DbContextOptions<SampleAppContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        OnModelCreatingCampaign(modelBuilder);
        OnModelCreatingCustomer(modelBuilder);
        OnModelCreatingEvent(modelBuilder);
        OnModelCreatingGoods(modelBuilder);
        OnModelCreatingImportLog(modelBuilder);
        OnModelCreatingPurchase(modelBuilder);
        OnModelCreatingRestaurant(modelBuilder);
        OnModelCreatingVenue(modelBuilder);
    }
}
