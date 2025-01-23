using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class VenueImporter(SampleAppContext dbContext)
{
    public async Task<Guid> ProcessVenueAsync(Tickster.Api.Models.Crm.Venue crmVenue)
    {
        var venue = await dbContext.Venues.SingleOrDefaultAsync(v => v.TicksterVenueId == crmVenue.Id);

        Venue mappedVenue;
        if (venue == null)
        {
            mappedVenue = Mapper.MapVenue(crmVenue);
            await dbContext.AddAsync(mappedVenue);
        }
        else
        {
            mappedVenue = Mapper.MapVenue(crmVenue, venue);
        }

        await dbContext.SaveChangesAsync();

        return mappedVenue.Id;
    }
}
