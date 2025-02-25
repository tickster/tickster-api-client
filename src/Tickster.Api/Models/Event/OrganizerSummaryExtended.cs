using System.Text.Json.Serialization;

namespace Tickster.Api.Models.Event;

public class OrganizerSummaryExtended
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("_links")]
    public List<ResourceLink> Links { get; set; } = [];
    public string DefaultLanguage { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}