using Microsoft.Extensions.Options;

namespace Tickster.Api;

public class TicksterClientFactory(IOptions<TicksterOptions> options)
{
    private readonly TicksterOptions _options = options.Value;

    public TicksterClient Create(Func<HttpClient> httpClientFactory)
    {
        var httpClient = httpClientFactory();

        httpClient.BaseAddress = new Uri(_options.Endpoint);
        if (!string.IsNullOrEmpty(_options.ApiKey))
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_options.ApiKey}");
        }

        var agent = new TicksterHttpAgent(httpClient);
        return new TicksterClient(Options.Create(_options), agent);
    }
}
