using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Infrastructure.Configuration;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddSqliteDbContext(services, configuration["SqliteDbName"]);

        return services;
    }

    private static void AddSqliteDbContext(IServiceCollection services, string? dbName = "sampleapp.db")
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Join(path, dbName);

        services.AddDbContext<SampleAppContext>(options =>
            options.UseSqlite($"Data Source={dbPath}")
        );
    }
}
