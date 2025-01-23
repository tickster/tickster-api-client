using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class PurchaseImporter(SampleAppContext dbContext, CustomerImporter CustomerImporter, EventImporter EventImporter, GoodsImporter GoodsImporter, CampaignImporter CampaignImporter)
{
    public async Task ProcessPurchaseAsync(Tickster.Api.Models.Crm.Purchase crmPurchase)
    {
        var purchase = await dbContext.Purchases
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.TicksterPurchaseRefNo == crmPurchase.PurchaseRefno);

        Purchase mappedPurchase;
        if (purchase == null)
        {
            mappedPurchase = Mapper.MapPurchase(crmPurchase);
            await dbContext.AddAsync(mappedPurchase);
        }
        else
        {
            mappedPurchase = Mapper.MapPurchase(crmPurchase, purchase);
            dbContext.Goods.RemoveRange(dbContext.Goods.Where(g => g.PurchaseId == mappedPurchase.Id));
            await dbContext.SaveChangesAsync();
        }

        mappedPurchase.CustomerId = await CustomerImporter.ProcessCustomerAsync(crmPurchase);

        await EventImporter.ProcessEventAsync(crmPurchase.Events);

        await GoodsImporter.ProcessGoodsAsync(mappedPurchase.Id, crmPurchase.Goods);

        await CampaignImporter.ProcessCampaignAsync(mappedPurchase.Id, crmPurchase.Campaigns);

        await dbContext.SaveChangesAsync();
    }
}
