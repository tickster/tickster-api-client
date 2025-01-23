namespace Tickster.Api.Models.Crm;
public class Campaign
{
    public string Id { get; set; } = string.Empty;
    // Can be empty for "automatic campaign"
    public string CommunicationId { get; set; } = string.Empty;
    public string ActivationCode { get; set; } = string.Empty;
    public string InternalReference { get; set; } = string.Empty; 
}