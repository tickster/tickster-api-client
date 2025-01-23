namespace TicksterSampleApp.Domain.Enums;

public enum Status
{
    Unknown = 0,
    Completed = 1,
    AwaitingPayment = 2,
    Cancelled = 3,
    Refunded = 4
}

public static class StatusExtensions
{
    public static Status FromString(string status)
    {
        status = status.Trim().ToLower();

        return status switch
        {
            "completed" => Status.Completed,
            "awaitingpayment" => Status.AwaitingPayment,
            "cancelled" => Status.Cancelled,
            "refunded" => Status.Refunded,
            _ => Status.Unknown
        };
    }         
}