using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class VenueImporter(ILogger<VenueImporter> _logger, SampleAppContext dbContext)
{
    public async Task Import(Tickster.Api.Models.Crm.Venue crmVenue, Event mappedEvent)
    {
        var mappedVenue = await AddOrUpdateVenue(crmVenue);
        mappedEvent.VenueId = mappedVenue.Id;

        await dbContext.SaveChangesAsync();
    }

    private async Task<Venue> AddOrUpdateVenue(Tickster.Api.Models.Crm.Venue crmVenue)
    {
        var dbVenue = await dbContext.Venues.SingleOrDefaultAsync(v => v.TicksterVenueId == crmVenue.Id);

        Venue mappedVenue;
        if (dbVenue == null)
        {
            _logger.LogDebug("New Venue ({VenueId}) - adding to DB", crmVenue.Id);
            mappedVenue = Mapper.MapVenue(crmVenue);
            await dbContext.AddAsync(mappedVenue);
        }
        else
        {
            _logger.LogDebug("Venue ({VenueId}) exists in DB - updating Venue", dbVenue.TicksterVenueId);
            mappedVenue = Mapper.MapVenue(crmVenue, dbVenue);
        }

        return mappedVenue;
    }
}
