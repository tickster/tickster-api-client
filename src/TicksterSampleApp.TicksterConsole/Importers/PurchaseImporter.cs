using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class PurchaseImporter(ILogger<PurchaseImporter> _logger, SampleAppContext dbContext, CustomerImporter CustomerImporter, EventImporter EventImporter, GoodsImporter GoodsImporter, CampaignImporter CampaignImporter)
{
    public async Task<ImportResult> Import(Tickster.Api.Models.Crm.Purchase crmPurchase)
    {
        var result = new ImportResult();
        var mappedPurchase = await AddOrUpdatePurchase(crmPurchase, result);

        result.Merge(await CustomerImporter.Import(crmPurchase, mappedPurchase));

        result.Merge(await EventImporter.Import(crmPurchase.Events));

        await GoodsImporter.Import(crmPurchase.Goods, mappedPurchase);

        result.Merge(await CampaignImporter.Import(crmPurchase.Campaigns, mappedPurchase));

        await dbContext.SaveChangesAsync();

        return result;
    }

    private async Task<Purchase> AddOrUpdatePurchase(Tickster.Api.Models.Crm.Purchase crmPurchase, ImportResult result)
    {
        var dbPurchase = await dbContext.Purchases
            .SingleOrDefaultAsync(p => p.TicksterPurchaseRefNo == crmPurchase.PurchaseRefno);

        Purchase mappedPurchase;
        if (dbPurchase == null)
        {
            _logger.LogDebug("New PurchaseRefNo ({PurchaseRefNo}) - adding to DB", crmPurchase.PurchaseRefno);
            mappedPurchase = Mapper.MapPurchase(crmPurchase);
            await dbContext.AddAsync(mappedPurchase);
            result.Purchases.Created.Add(mappedPurchase.Id);
        }
        else
        {
            _logger.LogDebug("PurchaseRefNo ({PurchaseRefNo}) exists in DB - updating Purchase", dbPurchase.TicksterPurchaseRefNo);
            mappedPurchase = Mapper.MapPurchase(crmPurchase, dbPurchase);
            await dbContext.SaveChangesAsync();
            result.Purchases.Updated.Add(mappedPurchase.Id);
        }

        return mappedPurchase;
    }
}
