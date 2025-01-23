using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class CampaignImporter(SampleAppContext dbContext, PurchaseCampaignImporter PurchaseCampaignImporter)
{
    public async Task ProcessCampaignAsync(Guid purchaseId, List<Tickster.Api.Models.Crm.Campaign> crmCampaigns)
    {
        foreach (var crmCampaign in crmCampaigns)
        {
            var campaign = await dbContext.Campaigns.FindAsync(crmCampaign.Id, crmCampaign.CommunicationId);

            Campaign mappedCampaign;
            if (campaign == null)
            {
                mappedCampaign = Mapper.MapCampaign(crmCampaign);
                await dbContext.AddAsync(mappedCampaign);
            }
            else
            {
                mappedCampaign = Mapper.MapCampaign(crmCampaign, campaign);
            }

            await PurchaseCampaignImporter.WriteToPurchaseCampaignLookUpTableAsync(purchaseId, mappedCampaign.TicksterCampaignId + mappedCampaign.TicksterCommunicationId);

            await dbContext.SaveChangesAsync();
        }
    }
}
