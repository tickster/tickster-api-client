namespace TicksterSampleApp.Domain.Models;

public class Goods
{
    public Purchase Purchase { get; set; } = new();
    public Restaurant Restaurant { get; set; } = new();
    public Event Event { get; set; } = new();
    public int Id { get; set; }
    public string GoodsId { get; set; } = string.Empty;
    public int PurchaseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ReceiptText { get; set; } = string.Empty;
    public int Type { get; set; }
    public string ArticleNumber { get; set; } = string.Empty;
    public decimal PriceIncVatAfterDiscount { get; set; }
    public decimal VatPortion { get; set; }
    public int VatPercent { get; set; }
    public int EventId { get; set; }
    public string Section { get; set; } = string.Empty;
    public int Seat { get; set; }
    public int Row { get; set; }
    public bool PartOfSeasonToken { get; set; }
    public string PartOfSeasonTokenGoodsId { get; set; } = string.Empty;
    public bool PartOfTableReservation { get; set; }
    public bool CanBePlacedAtTable { get; set; }
    public int RestaurantId { get; set; }
}
