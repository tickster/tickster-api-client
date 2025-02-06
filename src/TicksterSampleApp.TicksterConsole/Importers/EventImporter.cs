using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class EventImporter(SampleAppContext dbContext, RestaurantImporter RestaurantImporter, VenueImporter VenueImporter)
{
    public async Task Import(List<Tickster.Api.Models.Crm.Event> crmEvents)
    {
        foreach (var crmEvent in crmEvents)
        {
            var mappedEvent = await AddOrUpdateEvent(crmEvent);

            await RestaurantImporter.Import(crmEvent.Restaurants, mappedEvent);

            await VenueImporter.Import(crmEvent.Venue, mappedEvent);

            await dbContext.SaveChangesAsync();
        }
    }

    private async Task<Event> AddOrUpdateEvent(Tickster.Api.Models.Crm.Event crmEvent)
    {
        var dbEvent = await dbContext.Events.SingleOrDefaultAsync(e => e.TicksterEventId == crmEvent.Id);

        Event mappedEvent;
        if (dbEvent == null)
        {
            mappedEvent = Mapper.MapEvent(crmEvent);
            await dbContext.AddAsync(mappedEvent);
        }
        else
        {
            mappedEvent = Mapper.MapEvent(crmEvent, dbEvent);
        }

        return mappedEvent;
    }
}
