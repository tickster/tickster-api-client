﻿using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class GoodsImporter(SampleAppContext dbContext)
{
    public async Task Import(List<Tickster.Api.Models.Crm.GoodsItem> crmGoods, Purchase mappedPurchase)
    {
        dbContext.Goods.RemoveRange(dbContext.Goods.Where(g => g.PurchaseId == mappedPurchase.Id));

        foreach (var crmGood in crmGoods)
        {
            var mappedGoods = Mapper.MapGoods(crmGood);
            await dbContext.AddAsync(mappedGoods);

            mappedGoods.PurchaseId = mappedPurchase.Id;
            mappedGoods.EventId = await GetRelatedEventId(mappedGoods.TicksterEventId);
        }
    }

    private async Task<Guid?> GetRelatedEventId(string ticksterEventId)
        => (await dbContext.Events.SingleOrDefaultAsync(e => e.TicksterEventId == ticksterEventId))?.Id;
}
