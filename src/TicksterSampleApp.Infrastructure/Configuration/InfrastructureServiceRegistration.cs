using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Infrastructure.Configuration;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Join(path, $"{configuration["SqliteDbName"]}");

        services.AddDbContext<SampleAppContext>(options =>
            options.UseSqlite($"Data Source={dbPath}")
        );

        return services;
    }
}
