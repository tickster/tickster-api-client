using TicksterSampleApp.Domain.Enums;

namespace TicksterSampleApp.Domain.Models;

public class Purchase
{
    public ICollection<Goods> Goods { get; set; } = [];
    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public int TicksterCrmId { get; set; }
    public string TicksterPurchaseRefNo { get; set; } = string.Empty;
    public Status Status { get; set; }
    public DateTime Created { get; set; } = new();
    public DateTime LastUpdated { get; set; } = new();
    public string Currency { get; set; } = string.Empty;
    public Channel Channel { get; set; }
    public bool ToBePaidInRestaurantSystem { get; set; }
    public string DiscountCodeName { get; set; } = string.Empty;
    public string DiscountCode { get; set; } = string.Empty;
    public string EogRequestCode { get; set; } = string.Empty;
    public string PrivacyRefNo { get; set; } = string.Empty;
    public string TermsRefNo { get; set; } = string.Empty;
}
