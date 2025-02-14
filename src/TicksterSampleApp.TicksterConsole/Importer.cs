using Microsoft.Extensions.Logging;
using Tickster.Api;
using TicksterSampleApp.Importer.Importers;

namespace TicksterSampleApp.Importer;

public class Importer(ILogger<Importer> _logger, TicksterClient client, ImportLogHandler ImportLogHandler, PurchaseImporter PurchaseImporter)
{
    private readonly ImportResult result = new();

    public async Task Import(int crmId)
    {
        _logger.LogInformation("Fetching CrmPurchases starting from CrmId: {crmId}", crmId);
        var crmPurchases = await client.GetCrmPurchasesAfterId(crmId);
        _logger.LogInformation("Fetched {CrmPurchasesCount} purchases", crmPurchases.Count());

        if (crmPurchases.Any())
        {
            await ImportPurchases(crmPurchases);
            await Import(await ImportLogHandler.GetLastCrmId());
            return;
        }

        _logger.LogInformation("No new purchases to import");
        result.LogResultSummary(_logger);
        _logger.LogInformation("Import finished");
    }

    public async Task ImportPurchases(IEnumerable<Tickster.Api.Models.Crm.Purchase> crmPurchases)
    {
        foreach (var crmPurchase in crmPurchases)
        {
            _logger.LogInformation("Importing Purchase with CrmId {crmId}", crmPurchase.CrmId);
            result.Merge(await PurchaseImporter.Import(crmPurchase));

            await ImportLogHandler.WriteToImportLog(crmPurchase.CrmId);
        }
    } 
}
