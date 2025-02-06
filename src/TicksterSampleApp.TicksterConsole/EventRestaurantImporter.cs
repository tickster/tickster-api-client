using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class EventRestaurantImporter(SampleAppContext dbContext)
{
    public async Task CreateEventRestaurantLink(Event mappedEvent, Restaurant mappedRestaurant)
    {
        var dbEventRestaurant = await dbContext.EventRestaurants.FindAsync(mappedEvent.Id, mappedRestaurant.Id);

        if (dbEventRestaurant == null)
        {
            var mappedEventRestaurant = Mapper.MapEventRestaurant(mappedEvent, mappedRestaurant);

            await dbContext.AddAsync(mappedEventRestaurant);
        }
    }
}
