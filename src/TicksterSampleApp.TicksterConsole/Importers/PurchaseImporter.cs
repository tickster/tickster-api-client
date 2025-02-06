using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class PurchaseImporter(SampleAppContext dbContext, CustomerImporter CustomerImporter, EventImporter EventImporter, GoodsImporter GoodsImporter, CampaignImporter CampaignImporter)
{
    public async Task Import(Tickster.Api.Models.Crm.Purchase crmPurchase)
    {
        var mappedPurchase = await AddOrUpdatePurchase(crmPurchase);

        await CustomerImporter.Import(crmPurchase, mappedPurchase);

        await EventImporter.Import(crmPurchase.Events);

        await GoodsImporter.Import(mappedPurchase, crmPurchase.Goods);

        await CampaignImporter.Import(mappedPurchase, crmPurchase.Campaigns);

        await dbContext.SaveChangesAsync();
    }

    private async Task<Purchase> AddOrUpdatePurchase(Tickster.Api.Models.Crm.Purchase crmPurchase)
    {
        var dbPurchase = await dbContext.Purchases
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.TicksterPurchaseRefNo == crmPurchase.PurchaseRefno);

        Purchase mappedPurchase;
        if (dbPurchase == null)
        {
            mappedPurchase = Mapper.MapPurchase(crmPurchase);
            await dbContext.AddAsync(mappedPurchase);
        }
        else
        {
            mappedPurchase = Mapper.MapPurchase(crmPurchase, dbPurchase);
            await dbContext.SaveChangesAsync();
        }

        return mappedPurchase;
    }
}
