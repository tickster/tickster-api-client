using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class PurchaseImporter(ILogger<PurchaseImporter> logger, SampleAppContext dbContext, CustomerImporter customerImporter, EventImporter eventImporter, GoodsImporter goodsImporter, CampaignImporter campaignImporter)
{
    public async Task<ImportResult> Import(Tickster.Api.Models.Crm.Purchase crmPurchase)
    {
        var result = new ImportResult();
        var mappedPurchase = await AddOrUpdatePurchase(crmPurchase, result);

        result.Merge(await customerImporter.Import(crmPurchase, mappedPurchase));

        result.Merge(await eventImporter.Import(crmPurchase.Events));

        await goodsImporter.Import(crmPurchase.Goods, mappedPurchase);

        result.Merge(await campaignImporter.Import(crmPurchase.Campaigns, mappedPurchase));

        return result;
    }

    private async Task<Purchase> AddOrUpdatePurchase(Tickster.Api.Models.Crm.Purchase crmPurchase, ImportResult result)
    {
        var dbPurchase = await dbContext.Purchases
            .SingleOrDefaultAsync(p => p.TicksterPurchaseRefNo == crmPurchase.PurchaseRefno);

        Purchase mappedPurchase;
        if (dbPurchase == null)
        {
            logger.LogDebug("New PurchaseRefNo ({PurchaseRefNo}) - adding to DB", crmPurchase.PurchaseRefno);
            mappedPurchase = Mapper.MapPurchase(crmPurchase);
            await dbContext.AddAsync(mappedPurchase);
            result.Purchases.Created.Add(mappedPurchase.Id);
        }
        else
        {
            logger.LogDebug("PurchaseRefNo ({PurchaseRefNo}) exists in DB - updating Purchase", dbPurchase.TicksterPurchaseRefNo);
            mappedPurchase = Mapper.MapPurchase(crmPurchase, dbPurchase);
            result.Purchases.Updated.Add(mappedPurchase.Id);
        }

        return mappedPurchase;
    }
}
