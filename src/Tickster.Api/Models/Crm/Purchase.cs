namespace Tickster.Api.Models.Crm;
public class Purchase
{
    public int CrmId { get; set; }
    public string EogRequestCode { get; set; } = string.Empty;
    public string PurchaseRefno { get; set; } = string.Empty;
    public string ParentPurchaseRefno { get; set; } = string.Empty;
    public DateTime CreatedUtc { get; set; }
    public DateTime LastUpdatedUtc { get; set; }
    public PurchaseStatus Status { get; set; } = PurchaseStatus.Undefined;
    public string Currency { get; set; } = string.Empty;
    public Channel Channel { get; set; } = Channel.Undefined;
    public List<CartItem> CartItems { get; set; } = [];
    public List<Event> EventInfo { get; set; } = [];
    public string TpuUserRefno { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PostalAddressLineOne { get; set; } = string.Empty;
    public string PostalAddressLineTwo { get; set; } = string.Empty;
    public string Zipcode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string MobilePhoneNo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string IdNo { get; set; } = string.Empty;
    public bool IsCompany { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public bool AcceptInfo { get; set; }
    public string TermsAgreementRefno { get; set; } = string.Empty;
    public string PrivacyAgreementRefno { get; set; } = string.Empty;
    public string BookingPaymentUrl { get; set; } = string.Empty;
    public string CampaignCodeFriendlyName { get; set; } = string.Empty;
    public string CampaignCode { get; set; } = string.Empty;
    public List<Campaign> Campaigns { get; set; } = [];
    public List<AdditionalInputField> AdditionalInputFields { get; set; } = [];
    public bool ToBePaidInRestaurantSystem { get; set; }
}
