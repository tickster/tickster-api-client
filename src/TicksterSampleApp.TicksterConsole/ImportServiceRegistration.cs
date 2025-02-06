using Microsoft.Extensions.DependencyInjection;

namespace TicksterSampleApp.Importer;

public static class ImportServiceRegistration
{
    public static IServiceCollection AddImporterServices(this IServiceCollection services)
    {
        services.AddTransient<Importer>()
        .AddTransient<ImportLogHandler>()
        .AddScoped<PurchaseImporter>()
        .AddScoped<EventImporter>()
        .AddScoped<RestaurantImporter>()
        .AddScoped<VenueImporter>()
        .AddScoped<GoodsImporter>()
        .AddScoped<CampaignImporter>()
        .AddScoped<PurchaseCampaignImporter>()
        .AddScoped<CustomerImporter>()
        .AddScoped<EventRestaurantImporter>();

        return services;
    }
}
