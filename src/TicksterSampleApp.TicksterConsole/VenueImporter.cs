using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class VenueImporter(SampleAppContext dbContext)
{
    public async Task Import(Tickster.Api.Models.Crm.Venue crmVenue, Event mappedEvent)
    {
        var mappedVenue = await AddOrUpdateVenue(crmVenue);
        mappedEvent.VenueId = mappedVenue.Id;

        await dbContext.SaveChangesAsync();
    }

    private async Task<Venue> AddOrUpdateVenue(Tickster.Api.Models.Crm.Venue crmVenue)
    {
        var dbVenue = await dbContext.Venues.SingleOrDefaultAsync(v => v.TicksterVenueId == crmVenue.Id);

        Venue mappedVenue;
        if (dbVenue == null)
        {
            mappedVenue = Mapper.MapVenue(crmVenue);
            await dbContext.AddAsync(mappedVenue);
        }
        else
        {
            mappedVenue = Mapper.MapVenue(crmVenue, dbVenue);
        }

        return mappedVenue;
    }
}
