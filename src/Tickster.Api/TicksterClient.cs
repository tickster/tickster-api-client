using Microsoft.Extensions.Options;
using System.Text.Json;
using Tickster.Api.Models.Crm;
using System.Text.Json.Serialization;
using Tickster.Api.Dtos;
using Tickster.Api.Models;

namespace Tickster.Api;
public class TicksterClient(IOptions<TicksterOptions> options, ITicksterHttpAgent agent)
{
    private readonly TicksterOptions _options = options.Value;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() },
    };

    public ITicksterHttpAgent Agent => agent;
    public RateLimitInfo RateLimitInfo => Agent.RateLimitInfo;

    public async Task<IEnumerable<Purchase>> GetCrmPurchasesAsync(
        int purchaseId, 
        int? limit = null, 
        string? lang = null, 
        bool includeAllAccounts = true)
    {
        lang ??= _options.DefaultLanguage;
        limit ??= _options.DefaultResultLimit;

        var json = await Agent.MakeCrmRequest(
            endpoint: string.Empty, 
            fromPurchase: purchaseId,
            resultLimit: (int)limit,
            lang: lang,
            loadChildEogData: includeAllAccounts);

        var result = ParseJsonResponse<CrmPurchaseLogResponse>(json);

        return result.Purchases.Select(p => p.Purchase);
    }

    private T ParseJsonResponse<T>(string json)
        => JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions)!;
}
