using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Infrastructure;

internal interface IRepository
{
    public void Add(Purchase newPurchase);
    public Task SaveChangesAsync();
}
