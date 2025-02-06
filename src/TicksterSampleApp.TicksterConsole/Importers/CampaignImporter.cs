using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class CampaignImporter(SampleAppContext dbContext, PurchaseCampaignImporter PurchaseCampaignImporter)
{
    public async Task Import(Purchase mappedPurchase, List<Tickster.Api.Models.Crm.Campaign> crmCampaigns)
    {
        foreach (var crmCampaign in crmCampaigns)
        {
            var mappedCampaign = await AddOrUpdateCampaign(crmCampaign);

            await PurchaseCampaignImporter.CreatePurchaseCampaignLink(mappedPurchase, mappedCampaign);

            await dbContext.SaveChangesAsync();
        }
    }

    private async Task<Campaign> AddOrUpdateCampaign(Tickster.Api.Models.Crm.Campaign crmCampaign)
    {
        var dbCampaign = await dbContext.Campaigns.FindAsync(crmCampaign.Id, crmCampaign.CommunicationId);

        Campaign mappedCampaign;
        if (dbCampaign == null)
        {
            mappedCampaign = Mapper.MapCampaign(crmCampaign);
            await dbContext.AddAsync(mappedCampaign);
        }
        else
        {
            mappedCampaign = Mapper.MapCampaign(crmCampaign, dbCampaign);
        }

        return mappedCampaign;
    }
}
