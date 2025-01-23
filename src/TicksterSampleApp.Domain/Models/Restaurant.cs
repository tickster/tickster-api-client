namespace TicksterSampleApp.Domain.Models;

public class Restaurant
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; } = string.Empty;
}
