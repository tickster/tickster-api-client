using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class CustomerImporter(ILogger<CustomerImporter> _logger, SampleAppContext dbContext)
{
    public async Task<ImportResult> Import(Tickster.Api.Models.Crm.Purchase crmPurchase, Purchase mappedPurchase)
    {
        if (!IsValidCustomer(crmPurchase))
        {
            return new ImportResult();
        }

        var result = new ImportResult();

        var mappedCustomer = await AddOrUpdateCustomer(crmPurchase, result);
        mappedPurchase.CustomerId = mappedCustomer.Id;

        return result;
    }

    private async Task<Customer> AddOrUpdateCustomer(Tickster.Api.Models.Crm.Purchase crmPurchase, ImportResult result)
    {
        var dbCustomer = await dbContext.Customers
            .SingleOrDefaultAsync(c => (c.TicksterUserRefNo == crmPurchase.UserRefNo && !string.IsNullOrEmpty(crmPurchase.UserRefNo)) || c.Email == crmPurchase.Email);

        Customer mappedCustomer;
        if (dbCustomer == null)
        {
            _logger.LogDebug("New Customer ({UserRefNo}) - adding to DB", crmPurchase.UserRefNo);
            mappedCustomer = Mapper.MapCustomer(crmPurchase);
            await dbContext.AddAsync(mappedCustomer);
            result.Customers.Created.Add(mappedCustomer.Id);
        }
        else
        {
            _logger.LogDebug("Customer ({UserRefNo}) exists in DB - updating Customer", dbCustomer.TicksterUserRefNo);
            mappedCustomer = Mapper.MapCustomer(crmPurchase, dbCustomer);
            result.Customers.Updated.Add(mappedCustomer.Id);
        }

        return mappedCustomer;
    }

    private static bool IsValidCustomer(Tickster.Api.Models.Crm.Purchase crmPurchase)
        => !string.IsNullOrEmpty(crmPurchase.UserRefNo) || !string.IsNullOrEmpty(crmPurchase.Email);
}
