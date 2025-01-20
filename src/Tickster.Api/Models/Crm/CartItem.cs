using System.Text.Json.Serialization;
using Tickster.Api.JsonConverters;

namespace Tickster.Api.Models.Crm;

public class CartItem
{
    public string GoodsId { get; set; } = string.Empty;
    public string ArtNo { get; set; } = string.Empty;
    public string FriendlyName { get; set; } = string.Empty;
    public string ReceiptText { get; set; } = string.Empty;
    [JsonConverter(typeof(StringToIntegerConverter))]
    public int RestaurantId { get; set; }
    public CartItemType Type { get; set; } = CartItemType.Undefined;
    public bool CanBePlacedAtTable { get; set; }
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal PriceIncVatAfterDiscount { get; set; }
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal VatPortion { get; set; }
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal VatPercent { get; set; }
    public string PackageId { get; set; } = string.Empty;
    public bool PartOfSeasonToken { get; set; }
    public bool PartOfTableReservation { get; set; }
    public string LocRow { get; set; } = string.Empty;
    public string LocSeat { get; set; } = string.Empty;
    public string LocSection { get; set; } = string.Empty;
    public string EventErc { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = [];
}
