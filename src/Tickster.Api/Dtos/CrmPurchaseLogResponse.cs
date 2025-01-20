using Tickster.Api.Models.Crm;

namespace Tickster.Api.Dtos;
internal class CrmPurchaseLogResponse
{
    public IEnumerable<PurchaseWrapper> Purchases { get; set; } = [];
}

internal class PurchaseWrapper
{
    public Purchase Purchase { get; set; } = new();
}