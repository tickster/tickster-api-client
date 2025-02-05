using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class CustomerImporter(SampleAppContext dbContext)
{
    public async Task<Guid> Import(Tickster.Api.Models.Crm.Purchase crmPurchase)
    {
        var customer = await dbContext.Customers
            .SingleOrDefaultAsync(c => c.TicksterUserRefNo == crmPurchase.UserRefNo);

        Customer mappedCustomer;
        if (customer == null)
        {
            mappedCustomer = Mapper.MapCustomer(crmPurchase);
            await dbContext.AddAsync(mappedCustomer);
        }
        else
        {
            mappedCustomer = Mapper.MapCustomer(crmPurchase, customer);
        }

        await dbContext.SaveChangesAsync();

        return mappedCustomer.Id;
    }
}
