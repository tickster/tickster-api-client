using Microsoft.Extensions.Options;
using System.Text.Json;
using Tickster.Api.Models.Crm;
using System.Text.Json.Serialization;
using Tickster.Api.Dtos;
using Tickster.Api.Models;
using Event = Tickster.Api.Models.Event.Event;
using Tickster.Api.Models.Event;
using System;

namespace Tickster.Api;

public class TicksterClient(IOptions<TicksterOptions> options, ITicksterHttpAgent agent)
{
    private readonly TicksterOptions _options = options.Value;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() },
    };

    public ITicksterHttpAgent Agent => agent;
    public RateLimitInfo RateLimitInfo => Agent.RateLimitInfo;

    public async Task<IEnumerable<Purchase>> GetCrmPurchasesAtId(
        int purchaseId,
        int? limit = null,
        string? lang = null,
        GetCrmPurchasesOptions? options = null)
    {
        lang ??= _options.DefaultLanguage;
        limit ??= _options.DefaultResultLimit;
        options ??= new();

        var json = await Agent.MakeCrmRequest(
            endpoint: string.Empty,
            fromPurchase: purchaseId,
            resultLimit: (int)limit,
            lang: lang,
            loadChildEogData: options.IncludeAllAccounts);

        var purchaseResponse = ParseJsonResponse<CrmPurchaseLogResponse>(json);

        ProcessPurchaseResponse(purchaseResponse, options);

        return purchaseResponse.Purchases.Select(p => p.Purchase);
    }

    public async Task<IEnumerable<Purchase>> GetCrmPurchasesAfterId(
        int purchaseId,
        int? limit = null,
        string? lang = null,
        GetCrmPurchasesOptions? options = null)
        => await GetCrmPurchasesAtId(purchaseId + 1, limit, lang, options);

    public async Task<Event> Event(string id, string? version = null, string? lang = null)
    {
        version ??= _options.DefaultApiVersion;
        lang ??= _options.DefaultLanguage;

        var json = await Agent.MakeApiRequest(
            baseUrl: _options.EventBaseUrl,
            endpoint: $"events/{id}",
            version:  version,
            lang: lang
            );

        var eventResponse = ParseJsonResponse<Event>(json);

        return eventResponse;
    }

    public async Task<EventSummaryResourceCollection> Events(string? version = null, string? lang = null, Pagination? pagination = null)
    {
        version ??= _options.DefaultApiVersion;
        lang ??= _options.DefaultLanguage;
        pagination ??= new() { Skip = 0, Take = _options.DefaultResultLimit };

        var json = await Agent.MakeApiRequest(
            baseUrl: _options.EventBaseUrl,
            endpoint: "events",
            version: version,
            lang: lang,
            pagination: pagination
            );

        var eventsResponse = ParseJsonResponse<EventSummaryResourceCollection>(json);

        return eventsResponse;
    }

    private T ParseJsonResponse<T>(string json)
        => JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions)!;

    private static void ProcessPurchaseResponse(CrmPurchaseLogResponse purchaseResponse, GetCrmPurchasesOptions options)
    {
        if (options.SuppressUnsyncedRefunds)
        {
            // FIXME: If we are to perform other operations on the purchases before returning them:
            //  - Mind the order in which thse operations are applied
            //  - Consider using materialising Purchases to a List<Purchase> to avoid multiple enumerations
            purchaseResponse.Purchases = purchaseResponse.Purchases
                .Where(w => !(w.Purchase.Status == PurchaseStatus.Completed && w.Purchase.Goods.Count == 0));
        }
    }
}

public class GetCrmPurchasesOptions
{
    public bool IncludeAllAccounts { get; set; } = true;
    public bool SuppressUnsyncedRefunds { get; set; } = true;

}