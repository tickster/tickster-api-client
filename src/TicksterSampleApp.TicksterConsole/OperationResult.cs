using Microsoft.Extensions.Logging;

namespace TicksterSampleApp.Importer;

public class OperationResult<T>
{
    public HashSet<T> Created { get; set; } = [];
    public HashSet<T> Updated { get; set; } = [];

    public OperationResult<T> Merge(OperationResult<T> other)
    {
        Created.UnionWith(other.Created);
        Updated.UnionWith(other.Updated);

        return this;
    }

    public void LogResultSummary(ILogger _logger, string type)
    {
        _logger.LogInformation("{Type} - created {Count}", type, Created.Count);
        _logger.LogInformation("{Type} - updated {Count}", type, Updated.Count);
    }
}
