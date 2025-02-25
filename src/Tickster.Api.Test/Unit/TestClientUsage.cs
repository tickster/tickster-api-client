using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tickster.Api.Extensions;

namespace Tickster.Api.Test.Unit;
public class TestClientUsage
{
    [Fact]
    public void Test_DI_Usage()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddTicksterClient(options =>
        {
            options.ApiKey = "test-api-key";
        });

        var serviceProvider = services.BuildServiceProvider();

        // Act
        var apiClient = serviceProvider.GetRequiredService<TicksterClient>();

        // Assert
        Assert.NotNull(apiClient);
    }

    [Fact]
    public void Test_Factory_Usage()
    {
        // Arrange
        var options = new TicksterOptions
        {
            ApiKey = "test-api-key"
        };

        var factory = new TicksterClientFactory(Options.Create(options));

        // Act
        var apiClient = factory.Create(() => new HttpClient());

        // Assert
        Assert.NotNull(apiClient);
    }
}