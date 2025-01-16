using Microsoft.Extensions.Options;

namespace Tickster.Api.Test.Unit;
public class TestSetup
{
    private readonly TicksterClient _client;

    public TestSetup()
    {
        var options = new TicksterOptions
        {
            Endpoint = "https://api.tickster.com",
            ApiKey = "test-api-key"
        };
        var factory = new TicksterClientFactory(Options.Create(options));

        _client = factory.Create(() => new HttpClient());
    }
    [Fact]
    public void Should_Make_Agent_Accessible()
    {
        Assert.NotNull(_client.Agent);
        Assert.IsType<TicksterHttpAgent>(_client.Agent);
    }

    [Fact]
    public void Should_Make_HttpClient_Accessible()
    {
        Assert.NotNull(_client.Agent.HttpClient);
        Assert.IsType<HttpClient>(_client.Agent.HttpClient);
    }
}
