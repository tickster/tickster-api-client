using Microsoft.Extensions.Logging;
using Tickster.Api;
using TicksterSampleApp.Importer.Importers;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class Importer(ILogger<Importer> logger, SampleAppContext dbContext, TicksterClient client, ImportLogHandler importLogHandler, PurchaseImporter purchaseImporter)
{    
    public async Task Import(int crmId)
    {
        logger.LogInformation("Fetching CrmPurchases starting from CrmId: {crmId}", crmId + 1);
        var crmPurchases = await client.GetCrmPurchasesAfterId(crmId);
        logger.LogInformation("Fetched {CrmPurchasesCount} purchases", crmPurchases.Count());

        if (!crmPurchases.Any())
        {
            logger.LogInformation("There are no purchases to import");
            return;
        }

        var result = new ImportResult();

        foreach (var crmPurchase in crmPurchases)
        {
            logger.LogInformation("Importing Purchase with CrmId {crmId}", crmPurchase.CrmId);
            result.Merge(await purchaseImporter.Import(crmPurchase));

            logger.LogInformation("Writing last imported Purchase (CrmId: {crmId}) to ImportLog", crmPurchase.CrmId);
            await importLogHandler.WriteToImportLog(crmPurchase.CrmId);

            await dbContext.SaveChangesAsync();
        }

        result.LogResultSummary(logger);
        logger.LogInformation("Import finished");
    }
}
