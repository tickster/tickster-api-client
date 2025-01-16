using Microsoft.Extensions.Options;

namespace Tickster.Api;
public class TicksterClient(IOptions<TicksterOptions> options, TicksterHttpAgent agent)
{
    private readonly TicksterOptions _options = options.Value;
    public TicksterHttpAgent Agent => agent;
}
