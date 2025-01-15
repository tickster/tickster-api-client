namespace TicksterSampleApp.Domain.Models;

public class Purchase
{
    public Customer Customer { get; set; } = new();
    public Campaign Campaign { get; set; } = new();
    public ICollection<Goods> Goods { get; set; } = [];
    public int Id { get; set; }
    public string TicksterCrmId { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public int CampaignId { get; set; }
    public string TicksterPurchaseRefNo { get; set; } = string.Empty;
    public int Status { get; set; }
    public DateTime Created { get; set; } = new();
    public DateTime LastUpdated { get; set; } = new();
    public string Currency { get; set; } = string.Empty;
    public int Channel { get; set; }
    public bool ToBePaidInRestaurantSystem { get; set; }
    public string DiscountCodeName { get; set; } = string.Empty;
    public string DiscountCode { get; set; } = string.Empty;
    public string EogRequestCode { get; set; } = string.Empty;
    public string PrivacyRefNo { get; set; } = string.Empty;
    public string TermsRefNo { get; set; } = string.Empty;
}
