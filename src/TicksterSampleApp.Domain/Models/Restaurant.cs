namespace TicksterSampleApp.Domain.Models;

public class Restaurant
{
    public ICollection<Goods> Goods { get; set; } = [];
    public Venue Venue { get; set; } = new();
    public int Id { get; set; }
    public int VenueId { get; set; }
    public string RestaurantName { get; set; } = string.Empty;
}
