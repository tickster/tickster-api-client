using System.Text.Json.Serialization;
using Tickster.Api.JsonConverters;

namespace Tickster.Api.Models.Crm;

public class GoodsItem
{
    public string GoodsId { get; set; } = string.Empty;
    public string ArtNo { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public GoodsItemType Type { get; set; } = GoodsItemType.Undefined;
    public string ReceiptText { get; set; } = string.Empty;
    public string? RestaurantId { get; set; } = string.Empty;
    public bool CanBePlacedAtTable { get; set; }
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal PriceIncVatAfterDiscount { get; set; }
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal VatPortion { get; set; }
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal VatPercent { get; set; }
    public string? PackageId { get; set; } = string.Empty;
    public bool PartOfSeasonToken { get; set; }
    public string? PartOfSeasonTokenGoodsId { get; set; } = string.Empty;
    public bool PartOfTableReservation { get; set; }
    public string? Row { get; set; } = string.Empty;
    public string? Seat { get; set; } = string.Empty;
    public string? Section { get; set; } = string.Empty;
    public string EventId { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = [];
}
