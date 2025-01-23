namespace Tickster.Api.Models;
public class RateLimitInfo
{
    public int ConfiguredLimit { get; set; }
    public int RemainingRequests { get; set; }
    public DateTime? FirstRequestAtUtc { get; set; }
    public DateTime? LastRequestAtUtc { get; set; }
}
