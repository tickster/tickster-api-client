using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TicksterSampleApp.Importer;
using TicksterSampleApp.Importer.Importers;
using TicksterSampleApp.Infrastructure.Configuration;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddImporterServices()
    .AddSerilog((services, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(builder.Configuration));

using IHost host = builder.Build();

var lastCrmId = await host.Services.GetRequiredService<ImportLogHandler>().GetLastCrmId();
await host.Services.GetRequiredService<Importer>().Import(lastCrmId);
