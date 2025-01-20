
namespace Tickster.Api;

public interface ITicksterHttpAgent
{
    HttpClient HttpClient { get; }

    Task<string> MakeCrmRequest(string endpoint, int fromPurchase, int resultLimit, string lang);
}