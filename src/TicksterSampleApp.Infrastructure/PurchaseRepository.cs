using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Infrastructure;

internal class PurchaseRepository(SampleAppContext context) : Repository(context)
{
}
