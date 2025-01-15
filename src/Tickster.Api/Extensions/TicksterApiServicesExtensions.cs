using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Tickster.Api.Extensions;
public static class TicksterApiServicesExtensions
{
    public static IServiceCollection AddTicksterClient(this IServiceCollection services, Action<TicksterOptions> configureOptions)
    {
        services.Configure(configureOptions);
        services.AddSingleton<TicksterClientFactory>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<TicksterOptions>>();
            return new TicksterClientFactory(options);
        });
        services.AddSingleton<TicksterClient>(provider =>
        {
            var factory = provider.GetRequiredService<TicksterClientFactory>();
            var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
            return factory.Create(() => httpClientFactory.CreateClient());
        });

        return services;
    }
}
