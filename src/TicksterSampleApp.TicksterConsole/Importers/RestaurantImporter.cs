using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class RestaurantImporter(ILogger<RestaurantImporter> _logger, SampleAppContext dbContext, EventRestaurantImporter EventRestaurantImporter)
{
    public async Task<ImportResult> Import(List<Tickster.Api.Models.Crm.Restaurant> crmRestaurants, Event mappedEvent)
    {
        var result = new ImportResult();

        foreach (var crmRestaurant in crmRestaurants)
        {
            if (IsRestaurantTracked(crmRestaurant))
            {
                continue;
            }

            var mappedRestaurant = await AddOrUpdateRestaurant(crmRestaurant, result);

            await EventRestaurantImporter.CreateEventRestaurantLink(mappedEvent, mappedRestaurant);
        }

        return result;
    }

    private async Task<Restaurant> AddOrUpdateRestaurant(Tickster.Api.Models.Crm.Restaurant crmRestaurant, ImportResult result)
    {
        var dbRestaurant = await dbContext.Restaurants.SingleOrDefaultAsync(r => r.RestaurantId == crmRestaurant.RestaurantId);

        Restaurant mappedRestaurant;
        if (dbRestaurant == null)
        {
            _logger.LogDebug("New Restaurant ({RestaurantId}) - adding to DB", crmRestaurant.RestaurantId);
            mappedRestaurant = Mapper.MapRestaurant(crmRestaurant);
            await dbContext.AddAsync(mappedRestaurant);
            result.Restaurants.Created.Add(mappedRestaurant.Id);
        }
        else
        {
            _logger.LogDebug("Restaurant ({RestaurantId}) exists in DB - updating Restaurant", dbRestaurant.RestaurantId);
            mappedRestaurant = Mapper.MapRestaurant(crmRestaurant, dbRestaurant);
            result.Restaurants.Updated.Add(mappedRestaurant.Id);
        }

        return mappedRestaurant;
    }

    private bool IsRestaurantTracked(Tickster.Api.Models.Crm.Restaurant crmRestaurant)
        => dbContext.ChangeTracker
            .Entries<Restaurant>()
            .Any(e => e.Entity.RestaurantId == crmRestaurant.RestaurantId && e.State == EntityState.Added);
}
