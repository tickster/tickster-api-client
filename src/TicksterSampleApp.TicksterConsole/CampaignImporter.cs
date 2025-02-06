using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class CampaignImporter(SampleAppContext dbContext, PurchaseCampaignImporter PurchaseCampaignImporter)
{
    public async Task Import(Purchase dbPurchase, List<Tickster.Api.Models.Crm.Campaign> crmCampaigns)
    {
        foreach (var crmCampaign in crmCampaigns)
        {
            var dbCampaign = await AddOrUpdateCampaign(crmCampaign);

            await PurchaseCampaignImporter.CreatePurchaseCampaignLink(dbPurchase, dbCampaign);

            await dbContext.SaveChangesAsync();
        }
    }

    private async Task<Campaign> AddOrUpdateCampaign(Tickster.Api.Models.Crm.Campaign crmCampaign)
    {
        var campaign = await dbContext.Campaigns.FindAsync(crmCampaign.Id, crmCampaign.CommunicationId);

        Campaign dbCampaign;
        if (campaign == null)
        {
            dbCampaign = Mapper.MapCampaign(crmCampaign);
            await dbContext.AddAsync(dbCampaign);
        }
        else
        {
            dbCampaign = Mapper.MapCampaign(crmCampaign, campaign);
        }

        return dbCampaign;
    }
}
