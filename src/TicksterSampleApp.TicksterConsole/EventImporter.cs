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
            var ev = await dbContext.Events.SingleOrDefaultAsync(e => e.TicksterEventId == crmEvent.Id);

            Event mappedEvent;
            if (ev == null)
            {
                mappedEvent = Mapper.MapEvent(crmEvent);
                await dbContext.AddAsync(mappedEvent);            
            }
            else
            {
                mappedEvent = Mapper.MapEvent(crmEvent, ev);
            }

            await RestaurantImporter.Import(mappedEvent.Id, crmEvent.Restaurants);     
            
            mappedEvent.VenueId = await VenueImporter.Import(crmEvent.Venue);

            await dbContext.SaveChangesAsync();
        }
    }
}
