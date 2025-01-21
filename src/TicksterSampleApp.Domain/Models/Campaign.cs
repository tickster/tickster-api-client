namespace TicksterSampleApp.Domain.Models;

public class Campaign
{
    public ICollection<Purchase> Purchases { get; set; } = [];
    public int Id { get; set; }
    public string TicksterCampaignId { get; set; } = string.Empty;
    public string TicksterCommunicationId { get; set; } = string.Empty;
    public string ActivationCode { get; set; } = string.Empty;
    public string TicksterInternalReference { get; set; } = string.Empty;
}
