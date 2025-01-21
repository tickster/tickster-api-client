
using Tickster.Api.Models;

namespace Tickster.Api;

public interface ITicksterHttpAgent
{
    HttpClient HttpClient { get; }
    RateLimitInfo RateLimitInfo { get; }

    Task<string> MakeCrmRequest(string endpoint, int fromPurchase, int resultLimit, string lang, bool loadChildEogData = true);
}