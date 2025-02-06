using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class PurchaseCampaignImporter(SampleAppContext dbContext)
{
    public async Task CreatePurchaseCampaignLink(Purchase dbPurchase, Campaign dbCampaign)
    {
        var campaignKey = BuildCampaignKey(dbCampaign);
        var purchaseCampaign = await dbContext.PurchaseCampaignLookup.FindAsync(dbPurchase.Id, campaignKey);

        if (purchaseCampaign == null)
        {
            var dbPurchaseCampaign = Mapper.MapPurchaseCampaign(dbPurchase, campaignKey);
            await dbContext.AddAsync(dbPurchaseCampaign);
        }
    }

    private string BuildCampaignKey(Campaign dbCampaign)
        => dbCampaign.TicksterCampaignId + dbCampaign.TicksterCommunicationId;
}
