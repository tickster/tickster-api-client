using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class GoodsImporter(SampleAppContext dbContext)
{
    public async Task ProcessGoodsAsync(Guid purchaseId, List<Tickster.Api.Models.Crm.GoodsItem> crmGoods)
    {
        foreach (var crmGood in crmGoods)
        {
            var mappedGoods = Mapper.MapGoods(crmGood);
            await dbContext.AddAsync(mappedGoods);

            mappedGoods.PurchaseId = purchaseId;
            mappedGoods.EventId = await GetRelatedEventId(mappedGoods.TicksterEventId);

            await dbContext.SaveChangesAsync();
        }
    }

    private async Task<Guid?> GetRelatedEventId(string ticksterEventId)
    {
        return (await dbContext.Events.SingleOrDefaultAsync(e => e.TicksterEventId == ticksterEventId))?.Id;
    }
}
