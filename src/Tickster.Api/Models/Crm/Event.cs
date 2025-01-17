namespace Tickster.Api.Models.Crm;
public class Event
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public DateTime LastUpdated { get; set; }
    public string ProductionId { get; set; } = string.Empty;
    public string ProductionName { get; set; } = string.Empty;
    public bool HasTableReservation { get; set; }
    public List<string> Tags { get; set; } = [];
    public Venue Venue { get; set; } = new();
    public List<Restaurant> Restaurants { get; set; } = [];
}
