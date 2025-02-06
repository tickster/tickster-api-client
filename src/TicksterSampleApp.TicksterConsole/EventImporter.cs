using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class EventImporter(SampleAppContext dbContext, RestaurantImporter RestaurantImporter, VenueImporter VenueImporter)
{
    public async Task Import(List<Tickster.Api.Models.Crm.Event> crmEvents)
    {
        foreach (var crmEvent in crmEvents)
        {
            var dbEvent = await AddOrUpdateEvent(crmEvent);

            await RestaurantImporter.Import(dbEvent, crmEvent.Restaurants);

            await VenueImporter.Import(crmEvent.Venue, dbEvent);

            await dbContext.SaveChangesAsync();
        }
    }

    private async Task<Event> AddOrUpdateEvent(Tickster.Api.Models.Crm.Event crmEvent)
    {
        var ev = await dbContext.Events.SingleOrDefaultAsync(e => e.TicksterEventId == crmEvent.Id);

        Event dbEvent;
        if (ev == null)
        {
            dbEvent = Mapper.MapEvent(crmEvent);
            await dbContext.AddAsync(dbEvent);
        }
        else
        {
            dbEvent = Mapper.MapEvent(crmEvent, ev);
        }

        return dbEvent;
    }
}
