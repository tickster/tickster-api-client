using Microsoft.Extensions.Options;
using System.Text.Json;
using Tickster.Api.Models.Crm;
using System.Text.Json.Serialization;
using Tickster.Api.Dtos;

namespace Tickster.Api;
public class TicksterClient(IOptions<TicksterOptions> options, TicksterHttpAgent agent)
{
    private readonly TicksterOptions _options = options.Value;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { 
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() },
    };

    public TicksterHttpAgent Agent => agent;

    public async Task<IEnumerable<Purchase>> GetCrmPurchasesAsync(int purchaseId, int? limit, string? lang)
    {
        lang ??= _options.DefaultLanguage;
        limit ??= _options.DefaultResultLimit;

        var json = await Agent.MakeCrmRequest(string.Empty, purchaseId, (int)limit, lang);
        var result = ParseJsonResponse<CrmPurchaseLogResponse>(json);

        return result.Purchases;
    }

    private T ParseJsonResponse<T>(string json)
        => JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions)!;
}
