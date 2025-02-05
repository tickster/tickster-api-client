using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicksterSampleApp.Infrastructure.Contexts;
using Tickster.Api.Extensions;

namespace TicksterSampleApp.Infrastructure.Configuration;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddSqliteDbContext(services, configuration["SqliteDbName"]);

        services.Configure<TicksterCrmConfig>(
            configuration.GetSection("Tickster:CrmApi")
        );

        services.AddTicksterClient(options =>
        {
            var crmConfig = configuration.GetSection("Tickster:CrmApi").Get<TicksterCrmConfig>();
            options.Endpoint = crmConfig.Endpoint;
            options.ApiKey = crmConfig.ApiKey;
            options.EogRequestCode = crmConfig.EogRequestCode;
            options.Login = crmConfig.Login;
            options.Password = crmConfig.Password;
        });

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
