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

        var crmConfig = configuration.GetSection("Tickster:CrmApi").Get<TicksterCrmConfig>();

        services.Configure<TicksterCrmConfig>(
                configuration.GetSection("Tickster:CrmApi")
            );

        services.AddTicksterClient(clientOptions =>
        {

            clientOptions.Endpoint = crmConfig!.Endpoint;
            clientOptions.ApiKey = crmConfig!.ApiKey;
            clientOptions.EogRequestCode = crmConfig!.EogRequestCode;
            clientOptions.Login = crmConfig!.Login;
            clientOptions.Password = crmConfig!.Password;
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
