using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class CustomerImporter(SampleAppContext dbContext)
{
    public async Task Import(Tickster.Api.Models.Crm.Purchase crmPurchase, Purchase dbPurchase)
    {
        var dbCustomer = await AddOrUpdateCustomer(crmPurchase);
        dbPurchase.CustomerId = dbCustomer.Id;

        await dbContext.SaveChangesAsync();
    }

    private async Task<Customer> AddOrUpdateCustomer(Tickster.Api.Models.Crm.Purchase crmPurchase)
    {
        var customer = await dbContext.Customers
            .SingleOrDefaultAsync(c => c.TicksterUserRefNo == crmPurchase.UserRefNo);

        Customer dbCustomer;
        if (customer == null)
        {
            dbCustomer = Mapper.MapCustomer(crmPurchase);
            await dbContext.AddAsync(dbCustomer);
        }
        else
        {
            dbCustomer = Mapper.MapCustomer(crmPurchase, customer);
        }

        return dbCustomer;
    }
}
