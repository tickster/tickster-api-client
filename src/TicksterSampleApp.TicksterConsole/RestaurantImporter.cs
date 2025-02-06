using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class RestaurantImporter(SampleAppContext dbContext, EventRestaurantImporter EventRestaurantImporter)
{
    public async Task Import(Event dbEvent, List<Tickster.Api.Models.Crm.Restaurant> crmRestaurants)
    {
        foreach (var crmRestaurant in crmRestaurants)
        {
            var dbRestaurant = await AddOrUpdateRestaurant(crmRestaurant);

            await EventRestaurantImporter.CreateEventRestaurantLink(dbEvent, dbRestaurant);

            await dbContext.SaveChangesAsync();
        }
    }

    private async Task<Restaurant> AddOrUpdateRestaurant(Tickster.Api.Models.Crm.Restaurant crmRestaurant)
    {
        var restaurant = await dbContext.Restaurants.SingleOrDefaultAsync(r => r.RestaurantId == crmRestaurant.RestaurantId);

        Restaurant dbRestaurant;
        if (restaurant == null)
        {
            dbRestaurant = Mapper.MapRestaurant(crmRestaurant);
            await dbContext.AddAsync(dbRestaurant);
        }
        else
        {
            dbRestaurant = Mapper.MapRestaurant(crmRestaurant, restaurant);
        }

        return dbRestaurant;
    }
}
