using Microsoft.Extensions.Hosting;
using TicksterSampleApp.Infrastructure.Configuration;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);

using IHost host = builder.Build();

await host.RunAsync();
