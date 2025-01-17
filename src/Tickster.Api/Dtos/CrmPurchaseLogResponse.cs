using Tickster.Api.Models.Crm;

namespace Tickster.Api.Dtos;
internal class CrmPurchaseLogResponse
{
    public IEnumerable<Purchase> Purchases { get; set; } = [];
}
