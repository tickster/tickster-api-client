using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class RestaurantImporter(SampleAppContext dbContext, EventRestaurantImporter EventRestaurantImporter)
{
    public async Task Import(Event mappedEvent, List<Tickster.Api.Models.Crm.Restaurant> crmRestaurants)
    {
        foreach (var crmRestaurant in crmRestaurants)
        {
            var mappedRestaurant = await AddOrUpdateRestaurant(crmRestaurant);

            await EventRestaurantImporter.CreateEventRestaurantLink(mappedEvent, mappedRestaurant);

            await dbContext.SaveChangesAsync();
        }
    }

    private async Task<Restaurant> AddOrUpdateRestaurant(Tickster.Api.Models.Crm.Restaurant crmRestaurant)
    {
        var dbRestaurant = await dbContext.Restaurants.SingleOrDefaultAsync(r => r.RestaurantId == crmRestaurant.RestaurantId);

        Restaurant mappedRestaurant;
        if (dbRestaurant == null)
        {
            mappedRestaurant = Mapper.MapRestaurant(crmRestaurant);
            await dbContext.AddAsync(mappedRestaurant);
        }
        else
        {
            mappedRestaurant = Mapper.MapRestaurant(crmRestaurant, dbRestaurant);
        }

        return mappedRestaurant;
    }
}
