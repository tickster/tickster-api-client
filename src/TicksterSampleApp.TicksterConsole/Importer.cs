using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Tickster.Api;
using TicksterSampleApp.Importer.Importers;

namespace TicksterSampleApp.Importer;

public class Importer(IConfiguration configuration, ILogger<Importer> _logger, TicksterClient client, ImportLogHandler ImportLogHandler, PurchaseImporter PurchaseImporter)
{
    private readonly int maxDepth = configuration.GetValue<int>("MaxDepth");
    private readonly ImportResult result = new();

    public async Task RunImport()
    {
        await Import(await ImportLogHandler.GetLastCrmId());
    }

    public async Task Import(int crmId, int? depthCounter = 0)
    {
        if (depthCounter == maxDepth)
        {
            result.LogResultSummary(_logger);
            _logger.LogInformation("Max depth ({MaxDepth}) reached. Exiting...", maxDepth);
            return;
        }

        var crmPurchases = await client.GetCrmPurchasesAfterId(crmId);
        _logger.LogInformation("Fetched {CrmPurchasesCount} purchases", crmPurchases.Count());

        if (crmPurchases.Any())
        {
            foreach (var crmPurchase in crmPurchases)
            {
                _logger.LogInformation("Importing Purchase with CrmId {crmId}", crmPurchase.CrmId);
                result.Merge(await PurchaseImporter.Import(crmPurchase));
                await ImportLogHandler.WriteToImportLog(crmPurchase.CrmId);
            }

            await Import(crmPurchases.Last().CrmId, ++depthCounter);
            return;
        }

        _logger.LogInformation("No new purchases to import");
        result.LogResultSummary(_logger);
        _logger.LogInformation("Import finished");
    }
}
