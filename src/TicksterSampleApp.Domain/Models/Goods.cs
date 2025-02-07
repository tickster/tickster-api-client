using TicksterSampleApp.Domain.Enums;

namespace TicksterSampleApp.Domain.Models;

public class Goods
{
    public Guid? PurchaseId { get; set; }
    public Purchase Purchase { get; set; } = null!;
    public Guid? EventId { get; set; }
    public Event? Event { get; set; }
    public string TicksterEventId { get; set; } = string.Empty;
    public Guid Id { get; set; } = Guid.NewGuid();
    public string TicksterGoodsId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ReceiptText { get; set; } = string.Empty;
    public GoodsType Type { get; set; }
    public string ArticleNumber { get; set; } = string.Empty;
    public decimal PriceIncVatAfterDiscount { get; set; }
    public decimal VatPortion { get; set; }
    public decimal VatPercent { get; set; }
    public string Section { get; set; } = string.Empty;
    public int Seat { get; set; }
    public int Row { get; set; }
    public bool PartOfSeasonToken { get; set; }
    public string PartOfSeasonTokenGoodsId { get; set; } = string.Empty;
    public bool PartOfTableReservation { get; set; }
    public bool CanBePlacedAtTable { get; set; }
    public Guid? RestaurantId { get; set; }
}
