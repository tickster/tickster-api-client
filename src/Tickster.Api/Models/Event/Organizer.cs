using System.Text.Json.Serialization;

namespace Tickster.Api.Models.Event;

public class Organizer
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string OrganizationalId { get; set; } = string.Empty;
    public string SegmentName { get; set; } = string.Empty;
    public List<string> Address { get; set; } = [];
    public Dictionary<string, string> SocialMedia { get; set; } = [];
    public string DefaultLanguage { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    [JsonPropertyName("_links")]
    public List<ResourceLink> Links { get; set; } = [];
}
