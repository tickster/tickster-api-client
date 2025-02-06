using Tickster.Api;

namespace TicksterSampleApp.Importer;

public class Importer(TicksterClient client, ImportLogHandler ImportLogHandler, PurchaseImporter PurchaseImporter)
{    
    public async Task Import(int crmId)
    {
        var purchases = await client.GetCrmPurchasesAsync(crmId);

        foreach (var purchase in purchases)
        {
            await PurchaseImporter.Import(purchase);
            await ImportLogHandler.WriteToImportLog(purchase.CrmId);
        }        
    }
}
