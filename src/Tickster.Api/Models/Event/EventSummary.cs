using System.Text.Json.Serialization;

namespace Tickster.Api.Models.Event;

public class EventSummary
{
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public FormattedText Description { get; set; } = new();
    public DateTime StartUtc { get; set; }
    public DateTime EndUtc { get; set; }
    public DateTime LastUpdatedUtc { get; set; }
    public State State { get; set; }
    public string InfoUrl { get; set; } = string.Empty;
    public string ShopUrl { get; set; } = string.Empty;
    public EventHierarchyType EventHierarchyType { get; set; }
    public string ParentEventId { get; set; } = string.Empty;
    public OrganizerSummary Organizer { get; set; } = new();
    public Venue Venue { get; set; } = new();
    [JsonPropertyName("_links")]
    public List<ResourceLink> Links { get; set; } = [];
}