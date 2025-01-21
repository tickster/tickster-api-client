using Tickster.Api.Models;

namespace Tickster.Api.Exceptions;
public class RateLimitExceededError : TicksterApiError
{
    public RateLimitInfo? RateLimitInfo { get; set; }

    public RateLimitExceededError(RateLimitInfo rateLimit) : base($"Rate limit of {rateLimit.ConfiguredLimit} exceeded")
    {
        RateLimitInfo = rateLimit;
    }

    public RateLimitExceededError(RateLimitInfo rateLimit, Exception innerException) : base($"Rate limit of {rateLimit.ConfiguredLimit} exceeded", innerException)
    {
        RateLimitInfo = rateLimit;
    }
}
