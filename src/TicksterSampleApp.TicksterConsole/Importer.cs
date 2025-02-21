using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Tickster.Api;
using TicksterSampleApp.Importer.Importers;

namespace TicksterSampleApp.Importer;

public class Importer(IConfiguration configuration, ILogger<Importer> logger, TicksterClient client, ImportLogHandler importLogHandler, PurchaseImporter purchaseImporter)
{
    private readonly int maxDepth = configuration.GetValue<int>("MaxDepth");
    private readonly ImportResult result = new();

    public async Task RunImport()
    {
        await Import(await importLogHandler.GetLastCrmId());
    }

    public async Task Import(int crmId, int? depthCounter = 0)
    {
        if (depthCounter == maxDepth)
        {
            result.LogResultSummary(logger);
            logger.LogInformation("Max depth ({MaxDepth}) reached. Exiting...", maxDepth);
            return;
        }

        var crmPurchases = await client.GetCrmPurchasesAfterId(crmId);
        logger.LogInformation("Fetched {CrmPurchasesCount} purchases", crmPurchases.Count());

        if (crmPurchases.Any())
        {
            foreach (var crmPurchase in crmPurchases)
            {
                logger.LogInformation("Importing Purchase with CrmId {crmId}", crmPurchase.CrmId);
                result.Merge(await purchaseImporter.Import(crmPurchase));
                await importLogHandler.WriteToImportLog(crmPurchase.CrmId);
            }

            await Import(crmPurchases.Last().CrmId, ++depthCounter);
            return;
        }

        logger.LogInformation("No new purchases to import");
        result.LogResultSummary(logger);
        logger.LogInformation("Import finished");
    }
}
