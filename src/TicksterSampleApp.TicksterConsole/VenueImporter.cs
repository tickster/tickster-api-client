using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class VenueImporter(SampleAppContext dbContext)
{
    public async Task Import(Tickster.Api.Models.Crm.Venue crmVenue, Event dbEvent)
    {
        var dbVenue = await AddOrUpdateVenue(crmVenue);
        dbEvent.VenueId = dbVenue.Id;

        await dbContext.SaveChangesAsync();
    }

    private async Task<Venue> AddOrUpdateVenue(Tickster.Api.Models.Crm.Venue crmVenue)
    {
        var venue = await dbContext.Venues.SingleOrDefaultAsync(v => v.TicksterVenueId == crmVenue.Id);

        Venue dbVenue;
        if (venue == null)
        {
            dbVenue = Mapper.MapVenue(crmVenue);
            await dbContext.AddAsync(dbVenue);
        }
        else
        {
            dbVenue = Mapper.MapVenue(crmVenue, venue);
        }

        return dbVenue;
    }
}
