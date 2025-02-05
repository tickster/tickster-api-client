using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class RestaurantImporter(SampleAppContext dbContext, EventRestaurantImporter EventRestaurantImporter)
{
    public async Task Import(Guid eventId, List<Tickster.Api.Models.Crm.Restaurant> crmRestaurants)
    {
        foreach (var crmRestaurant in crmRestaurants)
        {
            var restaurant = await dbContext.Restaurants.SingleOrDefaultAsync(r => r.RestaurantId == crmRestaurant.RestaurantId);

            Restaurant mappedRestaurant;
            if (restaurant == null)
            {
                mappedRestaurant = Mapper.MapRestaurant(crmRestaurant);
                await dbContext.AddAsync(mappedRestaurant);
            }
            else
            {
                mappedRestaurant = Mapper.MapRestaurant(crmRestaurant, restaurant);
            }

            await EventRestaurantImporter.WriteToEventRestaurantLookUpTableAsync(eventId, mappedRestaurant.Id);

            await dbContext.SaveChangesAsync();
        }
    }
}
