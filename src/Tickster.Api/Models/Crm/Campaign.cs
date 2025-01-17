namespace Tickster.Api.Models.Crm;
public class Campaign
{
    public int PurchId { get; set; }
    public string CampaignRequestCode { get; set; } = string.Empty;
    public string CampaignCommunicationRequestCode { get; set; } = string.Empty;
    public string CampaignActivationCode { get; set; } = string.Empty;
    public string EogInternalRefNo { get; set; } = string.Empty;
}