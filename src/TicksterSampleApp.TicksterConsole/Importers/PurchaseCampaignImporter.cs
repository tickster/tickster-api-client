using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class PurchaseCampaignImporter(SampleAppContext dbContext)
{
    public async Task CreatePurchaseCampaignLink(Purchase dbPurchase, Campaign mappedCampaign)
    {
        var mappedPurchaseCampaign = Mapper.MapPurchaseCampaign(dbPurchase, mappedCampaign.Id);
    }

    public void RemovePurchaseCampaignLinks(Purchase dbPurchase)
    {
        dbContext.PurchaseCampaign.RemoveRange(dbContext.PurchaseCampaign.Where(pc => pc.PurchaseId == dbPurchase.Id));
    }
}
