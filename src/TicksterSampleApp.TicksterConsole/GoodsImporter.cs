using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class GoodsImporter(SampleAppContext dbContext)
{
    public async Task Import(Purchase dbPurchase, List<Tickster.Api.Models.Crm.GoodsItem> crmGoods)
    {
        dbContext.Goods.RemoveRange(dbContext.Goods.Where(g => g.PurchaseId == dbPurchase.Id));

        foreach (var crmGood in crmGoods)
        {
            var dbGoods = Mapper.MapGoods(crmGood);
            await dbContext.AddAsync(dbGoods);

            dbGoods.PurchaseId = dbPurchase.Id;
            dbGoods.EventId = await GetRelatedEventId(dbGoods.TicksterEventId);

            await dbContext.SaveChangesAsync();
        }
    }

    private async Task<Guid?> GetRelatedEventId(string ticksterEventId)
        => (await dbContext.Events.SingleOrDefaultAsync(e => e.TicksterEventId == ticksterEventId))?.Id;
}
