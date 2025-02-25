using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;

namespace Tickster.Api;

public class TicksterClientFactory(IOptions<TicksterOptions> options)
{
    private readonly TicksterOptions _options = options.Value;

    public TicksterClient Create(Func<HttpClient> httpClientFactory)
    {
        var httpClient = httpClientFactory();

        httpClient.BaseAddress = new Uri(_options.ApiBaseUrl);

        if (!string.IsNullOrEmpty(_options.ApiKey))
        {
            var encodedAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_options.Login}:{_options.Password}"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedAuth);
            httpClient.DefaultRequestHeaders.Add("x-api-key", _options.ApiKey);
        }

        var agent = new TicksterHttpAgent(httpClient, _options.EogRequestCode);
        return new TicksterClient(Options.Create(_options), agent);
    }
}
