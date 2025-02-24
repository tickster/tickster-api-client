namespace Tickster.Api.Models.Event;

public enum State
{
    Undefined,
    NotReleased,
    ReleasedForSale,
    ReleasedForNonPublicSale,
    SalePaused,
    SaleEnded,
    Cancelled
}