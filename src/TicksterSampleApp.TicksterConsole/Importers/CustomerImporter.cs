using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class CustomerImporter(SampleAppContext dbContext)
{
    public async Task Import(Tickster.Api.Models.Crm.Purchase crmPurchase, Purchase mappedPurchase)
    {
        var mappedCustomer = await AddOrUpdateCustomer(crmPurchase);
        mappedPurchase.CustomerId = mappedCustomer.Id;

        await dbContext.SaveChangesAsync();
    }

    private async Task<Customer> AddOrUpdateCustomer(Tickster.Api.Models.Crm.Purchase crmPurchase)
    {
        var dbCustomer = await dbContext.Customers
            .SingleOrDefaultAsync(c => c.TicksterUserRefNo == crmPurchase.UserRefNo);

        Customer mappedCustomer;
        if (dbCustomer == null)
        {
            mappedCustomer = Mapper.MapCustomer(crmPurchase);
            await dbContext.AddAsync(mappedCustomer);
        }
        else
        {
            mappedCustomer = Mapper.MapCustomer(crmPurchase, dbCustomer);
        }

        return mappedCustomer;
    }
}
