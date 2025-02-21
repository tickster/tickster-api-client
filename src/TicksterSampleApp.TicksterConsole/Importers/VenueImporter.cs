using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class VenueImporter(ILogger<VenueImporter> logger, SampleAppContext dbContext)
{
    public async Task<ImportResult> Import(Tickster.Api.Models.Crm.Venue crmVenue, Event mappedEvent)
    {
        var result = new ImportResult();

        var mappedVenue = await AddOrUpdateVenue(crmVenue, result);
        mappedEvent.VenueId = mappedVenue.Id;

        return result;
    }

    private async Task<Venue> AddOrUpdateVenue(Tickster.Api.Models.Crm.Venue crmVenue, ImportResult result)
    {
        var dbVenue = await dbContext.Venues.SingleOrDefaultAsync(v => v.TicksterVenueId == crmVenue.Id);

        Venue mappedVenue;
        if (dbVenue == null)
        {
            logger.LogDebug("New Venue ({VenueId}) - adding to DB", crmVenue.Id);
            mappedVenue = Mapper.MapVenue(crmVenue);
            await dbContext.AddAsync(mappedVenue);
            result.Venues.Created.Add(mappedVenue.Id);
        }
        else
        {
            logger.LogDebug("Venue ({VenueId}) exists in DB - updating Venue", dbVenue.TicksterVenueId);
            mappedVenue = Mapper.MapVenue(crmVenue, dbVenue);
            result.Venues.Updated.Add(mappedVenue.Id);
        }

        return mappedVenue;
    }
}
