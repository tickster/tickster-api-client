using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicksterSampleApp.Importer;
using TicksterSampleApp.Infrastructure.Configuration;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddTransient<Importer>()
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

using IHost host = builder.Build();

int lastCrmId = await host.Services.GetRequiredService<ImportLogHandler>().GetLastCrmId();
await host.Services.GetRequiredService<Importer>().ImportPurchases(lastCrmId);

await host.RunAsync();
