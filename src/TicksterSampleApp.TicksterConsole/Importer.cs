using Tickster.Api;

namespace TicksterSampleApp.Importer;

internal class Importer(TicksterClient client, ImportLogHandler ImportLogHandler, PurchaseImporter PurchaseImporter)
{    
    public async Task ImportPurchases(int crmId)
    {
        var purchases = await client.GetCrmPurchasesAsync(crmId);

        foreach (var purchase in purchases)
        {
            await PurchaseImporter.ProcessPurchaseAsync(purchase);
            await ImportLogHandler.WriteToImportLogAsync(purchase.CrmId);
        }        
    }
}
