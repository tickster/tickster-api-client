using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class PurchaseImporter(ILogger<PurchaseImporter> _logger, SampleAppContext dbContext, CustomerImporter CustomerImporter, EventImporter EventImporter, GoodsImporter GoodsImporter, CampaignImporter CampaignImporter)
{
    public async Task Import(Tickster.Api.Models.Crm.Purchase crmPurchase)
    {
        var mappedPurchase = await AddOrUpdatePurchase(crmPurchase);

        await CustomerImporter.Import(crmPurchase, mappedPurchase);

        await EventImporter.Import(crmPurchase.Events);

        await GoodsImporter.Import(crmPurchase.Goods, mappedPurchase);

        await CampaignImporter.Import(crmPurchase.Campaigns, mappedPurchase);

        await dbContext.SaveChangesAsync();
    }

    private async Task<Purchase> AddOrUpdatePurchase(Tickster.Api.Models.Crm.Purchase crmPurchase)
    {
        var dbPurchase = await dbContext.Purchases
            .SingleOrDefaultAsync(p => p.TicksterPurchaseRefNo == crmPurchase.PurchaseRefno);

        Purchase mappedPurchase;
        if (dbPurchase == null)
        {
            _logger.LogDebug("New PurchaseRefNo ({PurchaseRefNo}) - adding to DB", crmPurchase.PurchaseRefno);
            mappedPurchase = Mapper.MapPurchase(crmPurchase);
            await dbContext.AddAsync(mappedPurchase);
        }
        else
        {
            _logger.LogDebug("PurchaseRefNo ({PurchaseRefNo}) exists in DB - updating Purchase", dbPurchase.TicksterPurchaseRefNo);
            mappedPurchase = Mapper.MapPurchase(crmPurchase, dbPurchase);
            await dbContext.SaveChangesAsync();
        }

        return mappedPurchase;
    }
}
