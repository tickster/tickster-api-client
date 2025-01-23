using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class EventRestaurantImporter(SampleAppContext dbContext)
{
    public async Task WriteToEventRestaurantLookUpTableAsync(Guid eventId, Guid restaurantId)
    {
        var eventRestaurant = await dbContext.EventRestaurants.FindAsync(eventId, restaurantId);

        if (eventRestaurant == null)
        {
            var mappedEventRestaurant = new EventRestaurant()
            {
                EventId = eventId,
                RestaurantId = restaurantId
            };

            await dbContext.AddAsync(mappedEventRestaurant);
        }
    }
}
