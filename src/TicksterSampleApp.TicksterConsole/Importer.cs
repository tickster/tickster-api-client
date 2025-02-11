using Microsoft.Extensions.Logging;
using Tickster.Api;
using TicksterSampleApp.Importer.Importers;

namespace TicksterSampleApp.Importer;

public class Importer(ILogger<Importer> _logger, TicksterClient client, ImportLogHandler ImportLogHandler, PurchaseImporter PurchaseImporter)
{    
    public async Task Import(int crmId)
    {
        _logger.LogInformation("Fetching CrmPurchases starting from CrmId: {crmId}", crmId);
        var crmPurchases = await client.GetCrmPurchasesAsync(crmId);
        _logger.LogInformation("Fetched {CrmPurchasesCount} purchases", crmPurchases.Count());

        foreach (var crmPurchase in crmPurchases)
        {
            _logger.LogInformation("Importing Purchase with CrmId {crmId}", crmPurchase.CrmId);
            await PurchaseImporter.Import(crmPurchase);

            _logger.LogInformation("Writing last imported Purchase (CrmId: {crmId}) to ImportLog", crmPurchase.CrmId);
            await ImportLogHandler.WriteToImportLog(crmPurchase.CrmId);
        }

        _logger.LogInformation("Import finished");
    }
}
