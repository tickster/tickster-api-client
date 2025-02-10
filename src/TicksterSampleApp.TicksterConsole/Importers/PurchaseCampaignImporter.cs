using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class PurchaseCampaignImporter(SampleAppContext dbContext)
{
    public async Task CreatePurchaseCampaignLink(Purchase dbPurchase, Campaign mappedCampaign)
    {
        var campaignKey = BuildCampaignKey(mappedCampaign);

        var mappedPurchaseCampaign = Mapper.MapPurchaseCampaign(dbPurchase, campaignKey);

        await dbContext.AddAsync(mappedPurchaseCampaign);
    }

    private string BuildCampaignKey(Campaign mappedCampaign)
        => mappedCampaign.TicksterCampaignId + mappedCampaign.TicksterCommunicationId;

    public void RemovePurchaseCampaignLinks(Purchase dbPurchase)
    {
        dbContext.PurchaseCampaign.RemoveRange(dbContext.PurchaseCampaign.Where(pc => pc.PurchaseId == dbPurchase.Id));
    }
}
