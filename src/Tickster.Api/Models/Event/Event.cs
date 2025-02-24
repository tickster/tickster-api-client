namespace Tickster.Api.Models.Event;

public class Event
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public FormattedText Description { get; set; } = new();
    public string AgeLimit { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string AccessibilityInfo { get; set; } = string.Empty;
    public DateTime StartUtc { get; set; }
    public DateTime EndUtc { get; set; }
    public DateTime DoorsOpenUtc { get; set; }
    public DateTime CurfewUtc { get; set; }
    public DateTime LastUpdatedUtc { get; set; }
    public State State { get; set; }
    public StockLevel StockLevel { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string InfoUrl { get; set; } = string.Empty;
    public string ShopUrl { get; set; } = string.Empty;
    public Dictionary<string, string> LocalizedShopUrls { get; set; } = [];
    public EventHierarchyType EvenHierarchyType { get; set; }
    public string ParentEventId { get; set; } = string.Empty;
    public OrganizerSummaryExtended Organizer { get; set; } = new();
    public Venue Venue { get; set; } = new();
    public List<string> Performers { get; set; } = [];
    public List<ExternalArtistReference> SpotifyArtists { get; set; } = [];
    public List<WebLink> WebLinks { get; set; } = [];
    public List<string> Tags { get; set; } = [];
    public List<Product> Products { get; set; } = [];
    public List<EventSummary> ChildEvents { get; set; } = [];
}