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
}
