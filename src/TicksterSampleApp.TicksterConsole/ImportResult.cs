using Microsoft.Extensions.Logging;

namespace TicksterSampleApp.Importer;

public class ImportResult()
{
    public CreatedUpdatedResult<Guid> Purchases { get; set; } = new();
    public CreatedUpdatedResult<Guid> Customers { get; set; } = new();
    public CreatedUpdatedResult<Guid> Events { get; set; } = new();
    public CreatedUpdatedResult<Guid> Restaurants { get; set; } = new();
    public CreatedUpdatedResult<string> Campaigns { get; set; } = new();
    public CreatedUpdatedResult<Guid> Venues { get; set; } = new();

    public ImportResult Merge(ImportResult other)
    {
        Purchases.Merge(other.Purchases);
        Customers.Merge(other.Customers);
        Events.Merge(other.Events);
        Restaurants.Merge(other.Restaurants);
        Campaigns.Merge(other.Campaigns);
        Venues.Merge(other.Venues);

        return this;
    }

    public void LogResultSummary(ILogger _logger)
    {
        Purchases.LogResultSummary(_logger, "Purchases");
        Customers.LogResultSummary(_logger, "Customers");
        Events.LogResultSummary(_logger, "Events");
        Restaurants.LogResultSummary(_logger, "Restaurants");
        Campaigns.LogResultSummary(_logger, "Campaigns");
        Venues.LogResultSummary(_logger, "Venues");
    }
}
