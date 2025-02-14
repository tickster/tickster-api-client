using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class EventImporter(ILogger<EventImporter> _logger, SampleAppContext dbContext, RestaurantImporter RestaurantImporter, VenueImporter VenueImporter)
{
    public async Task<ImportResult> Import(List<Tickster.Api.Models.Crm.Event> crmEvents)
    {
        var result = new ImportResult();

        foreach (var crmEvent in crmEvents)
        {
            var mappedEvent = await AddOrUpdateEvent(crmEvent, result);

            result.Merge(await RestaurantImporter.Import(crmEvent.Restaurants, mappedEvent));

            result.Merge(await VenueImporter.Import(crmEvent.Venue, mappedEvent));
        }

        return result;
    }

    private async Task<Event> AddOrUpdateEvent(Tickster.Api.Models.Crm.Event crmEvent, ImportResult result)
    {
        var dbEvent = await dbContext.Events.SingleOrDefaultAsync(e => e.TicksterEventId == crmEvent.Id);

        Event mappedEvent;
        if (dbEvent == null)
        {
            _logger.LogDebug("New Event ({EventId}) - adding to DB", crmEvent.Id);
            mappedEvent = Mapper.MapEvent(crmEvent);
            await dbContext.AddAsync(mappedEvent);
            result.Events.Created.Add(mappedEvent.Id);
        }
        else
        {
            _logger.LogDebug("Event ({EventId}) exists in DB - updating Event", dbEvent.TicksterEventId);
            mappedEvent = Mapper.MapEvent(crmEvent, dbEvent);
            result.Events.Updated.Add(mappedEvent.Id);
        }

        return mappedEvent;
    }
}
