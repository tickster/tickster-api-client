namespace TicksterSampleApp.Domain.Models;

public class Event
{
    public Guid? VenueId { get; set; }
    public Venue? Venue { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public string TicksterEventId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime Start { get; set; } = new();
    public DateTime End { get; set; } = new();
    public DateTime LastUpdated { get; set; } = new();
    public string TicksterProductionId { get; set; } = string.Empty;
    public string ProductionName { get; set; } = string.Empty;
    public bool HasTableReservation { get; set; }
}
