using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Infrastructure;

internal abstract class Repository(SampleAppContext context)
{
    public void Add(Purchase newPurchase)
        => context.Add(newPurchase);

    public async Task SaveChangesAsync()
        => await context.SaveChangesAsync();
}
