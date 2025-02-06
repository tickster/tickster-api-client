using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class PurchaseImporter(SampleAppContext dbContext, CustomerImporter CustomerImporter, EventImporter EventImporter, GoodsImporter GoodsImporter, CampaignImporter CampaignImporter)
{
    public async Task Import(Tickster.Api.Models.Crm.Purchase crmPurchase)
    {
        var dbPurchase = await AddOrUpdatePurchase(crmPurchase);

        await CustomerImporter.Import(crmPurchase, dbPurchase);

        await EventImporter.Import(crmPurchase.Events);

        await GoodsImporter.Import(dbPurchase, crmPurchase.Goods);

        await CampaignImporter.Import(dbPurchase, crmPurchase.Campaigns);

        await dbContext.SaveChangesAsync();
    }

    private async Task<Purchase> AddOrUpdatePurchase(Tickster.Api.Models.Crm.Purchase crmPurchase)
    {
        var purchase = await dbContext.Purchases
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.TicksterPurchaseRefNo == crmPurchase.PurchaseRefno);

        Purchase dbPurchase;
        if (purchase == null)
        {
            dbPurchase = Mapper.MapPurchase(crmPurchase);
            await dbContext.AddAsync(dbPurchase);
        }
        else
        {
            dbPurchase = Mapper.MapPurchase(crmPurchase, purchase);
            await dbContext.SaveChangesAsync();
        }

        return dbPurchase;
    }
}
