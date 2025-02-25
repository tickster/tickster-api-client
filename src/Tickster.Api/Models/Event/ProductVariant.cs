namespace Tickster.Api.Models.Event;

public class ProductVariant
{
    public string Name { get; set; } = string.Empty;
    public Price Price { get; set; } = new();
}