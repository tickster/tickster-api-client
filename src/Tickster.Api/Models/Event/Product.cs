namespace Tickster.Api.Models.Event;

public class Product
{
    public string Name { get; set; } = string.Empty;
    public ProductType ProductType { get; set; }
    public string Description { get; set; } = string.Empty;
    public string MainImageUrl { get; set; } = string.Empty;
    public Price Price { get; set; } = new();
    public List<ProductVariant> Variants { get; set; } = [];
}