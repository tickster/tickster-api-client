namespace TicksterSampleApp.Domain.Models;

public class Event
{
    public ICollection<Goods> Goods { get; set; } = [];
    public int Id { get; set; }
    public string TicksterEventId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime Start { get; set; } = new();
    public DateTime End { get; set; } = new();
    public DateTime LastUpdated { get; set; } = new();
    public string TicksterProductionId { get; set; } = string.Empty;
    public string ProductionName { get; set; } = string.Empty;
    public bool HasTableReservation { get; set; }
    public int VenueId { get; set; }
}
