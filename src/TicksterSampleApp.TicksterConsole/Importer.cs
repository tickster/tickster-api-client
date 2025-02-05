using Tickster.Api;

namespace TicksterSampleApp.Importer;

internal class Importer(TicksterClient client, ImportLogHandler ImportLogHandler, PurchaseImporter PurchaseImporter)
{    
    public async Task Import(int crmId)
    {
        var purchases = await client.GetCrmPurchasesAsync(crmId);

        foreach (var purchase in purchases)
        {
            await PurchaseImporter.Import(purchase);
            await ImportLogHandler.WriteToImportLogAsync(purchase.CrmId);
        }        
    }
}
