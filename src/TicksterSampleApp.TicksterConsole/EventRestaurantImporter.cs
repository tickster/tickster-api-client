using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class EventRestaurantImporter(SampleAppContext dbContext)
{
    public async Task CreateEventRestaurantLink(Event dbEvent, Restaurant dbRestaurant)
    {
        var eventRestaurant = await dbContext.EventRestaurants.FindAsync(dbEvent.Id, dbRestaurant.Id);

        if (eventRestaurant == null)
        {
            var dbEventRestaurant = Mapper.MapEventRestaurant(dbEvent, dbRestaurant);

            await dbContext.AddAsync(dbEventRestaurant);
        }
    }
}
