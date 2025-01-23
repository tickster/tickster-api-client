using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class PurchaseCampaignImporter(SampleAppContext dbContext)
{
    public async Task WriteToPurchaseCampaignLookUpTableAsync(Guid purchaseId, string campaignId)
    {
        var purchaseCampaign = await dbContext.PurchaseCampaignLookup.FindAsync(purchaseId, campaignId);

        if (purchaseCampaign == null)
        {
            var mappedPurchaseCampaign = Mapper.MapPurchaseCampaign(purchaseId, campaignId);
            await dbContext.AddAsync(mappedPurchaseCampaign);
        }
    }
}
