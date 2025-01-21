namespace TicksterSampleApp.Domain.Models;

public class Venue
{
    public ICollection<Restaurant> Restaurants { get; set; } = [];
    public int Id { get; set; }
    public string TicksterVenueId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}
