using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class RestaurantImporter(ILogger<RestaurantImporter> _logger, SampleAppContext dbContext, EventRestaurantImporter EventRestaurantImporter)
{
    public async Task Import(List<Tickster.Api.Models.Crm.Restaurant> crmRestaurants, Event mappedEvent)
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
            _logger.LogDebug("New Restaurant ({RestaurantId}) - adding to DB", crmRestaurant.RestaurantId);
            mappedRestaurant = Mapper.MapRestaurant(crmRestaurant);
            await dbContext.AddAsync(mappedRestaurant);
        }
        else
        {
            _logger.LogDebug("Restaurant ({RestaurantId}) exists in DB - updating Restaurant", dbRestaurant.RestaurantId);
            mappedRestaurant = Mapper.MapRestaurant(crmRestaurant, dbRestaurant);
        }

        return mappedRestaurant;
    }
}
