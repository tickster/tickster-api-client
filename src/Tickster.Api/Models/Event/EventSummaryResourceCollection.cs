namespace Tickster.Api.Models.Event;

public class EventSummaryResourceCollection
{
    public int TotalItems { get; set; }
    public int Skipped { get; set; }
    public List<EventSummary> Items { get; set; } = [];
    public List<ResourceLink> Links { get; set; } = [];
}
