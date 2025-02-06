using Tickster.Api;
using TicksterSampleApp.Importer.Importers;

namespace TicksterSampleApp.Importer;

public class Importer(TicksterClient client, ImportLogHandler ImportLogHandler, PurchaseImporter PurchaseImporter)
{    
    public async Task Import(int crmId)
    {
        var crmPurchases = await client.GetCrmPurchasesAsync(crmId);

        foreach (var crmPurchase in crmPurchases)
        {
            await PurchaseImporter.Import(crmPurchase);
            await ImportLogHandler.WriteToImportLog(crmPurchase.CrmId);
        }        
    }
}
