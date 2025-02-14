using Microsoft.Extensions.Logging;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class CampaignImporter(ILogger<CampaignImporter> _logger, SampleAppContext dbContext, PurchaseCampaignImporter PurchaseCampaignImporter)
{
    public async Task<ImportResult> Import(List<Tickster.Api.Models.Crm.Campaign> crmCampaigns, Purchase mappedPurchase)
    {
        var result = new ImportResult();

        PurchaseCampaignImporter.RemovePurchaseCampaignLinks(mappedPurchase);

        foreach (var crmCampaign in crmCampaigns)
        {
            var mappedCampaign = await AddOrUpdateCampaign(crmCampaign, result);

            await PurchaseCampaignImporter.CreatePurchaseCampaignLink(mappedPurchase, mappedCampaign);
        }

        return result;
    }

    private async Task<Campaign> AddOrUpdateCampaign(Tickster.Api.Models.Crm.Campaign crmCampaign, ImportResult result)
    {
        var dbCampaign = await dbContext.Campaigns.FindAsync(crmCampaign.Id, crmCampaign.CommunicationId);

        Campaign mappedCampaign;
        if (dbCampaign == null)
        {
            _logger.LogDebug("New Campaign ({CampaignId}{CommunicationId}) - adding to DB", crmCampaign.Id, crmCampaign.CommunicationId);
            mappedCampaign = Mapper.MapCampaign(crmCampaign);
            await dbContext.AddAsync(mappedCampaign);
            result.Campaigns.Created.Add(mappedCampaign.Id);
        }
        else
        {
            _logger.LogDebug("Campaign ({CampaignId}{CommunicationId}) exists in DB - updating Campaign", dbCampaign.TicksterCampaignId, dbCampaign.TicksterCommunicationId);
            mappedCampaign = Mapper.MapCampaign(crmCampaign, dbCampaign);
            result.Campaigns.Updated.Add(mappedCampaign.Id);
        }

        return mappedCampaign;
    }
}
