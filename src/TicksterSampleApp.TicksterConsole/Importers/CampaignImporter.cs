using Microsoft.Extensions.Logging;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class CampaignImporter(ILogger<CampaignImporter> _logger, SampleAppContext dbContext, PurchaseCampaignImporter PurchaseCampaignImporter)
{
    public async Task Import(List<Tickster.Api.Models.Crm.Campaign> crmCampaigns, Purchase mappedPurchase)
    {
        PurchaseCampaignImporter.RemovePurchaseCampaignLinks(mappedPurchase);

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
            _logger.LogDebug("New Campaign ({CampaignId}{CommunicationId}) - adding to DB", crmCampaign.Id, crmCampaign.CommunicationId);
            mappedCampaign = Mapper.MapCampaign(crmCampaign);
            await dbContext.AddAsync(mappedCampaign);
        }
        else
        {
            _logger.LogDebug("Campaign ({CampaignId}{CommunicationId}) exists in DB - updating Campaign", dbCampaign.TicksterCampaignId, dbCampaign.TicksterCommunicationId);
            mappedCampaign = Mapper.MapCampaign(crmCampaign, dbCampaign);
        }

        return mappedCampaign;
    }
}
