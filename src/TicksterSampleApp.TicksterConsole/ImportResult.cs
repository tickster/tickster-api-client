using Microsoft.Extensions.Logging;
using System.Reflection;

namespace TicksterSampleApp.Importer;

public class ImportResult()
{
    public OperationResult<Guid> Purchases { get; set; } = new();
    public OperationResult<Guid> Customers { get; set; } = new();
    public OperationResult<Guid> Events { get; set; } = new();
    public OperationResult<Guid> Restaurants { get; set; } = new();
    public OperationResult<string> Campaigns { get; set; } = new();
    public OperationResult<Guid> Venues { get; set; } = new();

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

    public void LogResultSummary(ILogger logger)
    {
        foreach (var property in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var instance = property.GetValue(this);
            if (instance == null) continue;

            if (instance.GetType().IsGenericType && instance.GetType().GetGenericTypeDefinition() == typeof(OperationResult<>))
            {
                dynamic operationResult = instance;
                                
                var createdCount = operationResult.Created.Count;
                var updatedCount = operationResult.Updated.Count;

                logger.LogInformation("{PropertyName} - Created: {CreatedCount}, Updated: {UpdatedCount}", property.Name, (int)createdCount, (int)updatedCount);
            }
        }
    }
}
