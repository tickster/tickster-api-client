using System.Net;
using System.Text.Json;
using Tickster.Api.Dtos;
using Tickster.Api.Exceptions;
using Tickster.Api.Models;

namespace Tickster.Api;

public class TicksterHttpAgent(HttpClient client, string? eogRequestCode = null) : ITicksterHttpAgent
{
    public HttpClient HttpClient => client;
    private readonly string _eogRequestCode = eogRequestCode ?? string.Empty;

    public RateLimitInfo RateLimitInfo { get; private set; } = new();

    public async Task<string> MakeApiRequest(
        string baseUrl, 
        string endpoint, 
        string version, 
        string lang,
        Pagination pagination)
    {
        var requestUrl = new UriBuilder(baseUrl)
        {
            Path = $"api/v{version}/{lang}/{endpoint}",
            Query = pagination.ToString()
        };

        return await MakeRequest(requestUrl.ToString());
    }

    public async Task<string> MakeCrmRequest(
        string endpoint, 
        int fromPurchase, 
        int resultLimit, 
        string lang,
        bool loadChildEogData = true)
    {
        if (string.IsNullOrEmpty(_eogRequestCode))
        {
            throw new InvalidOperationException("EOG request code is not set.");
        }

        var crmRequestUrl = new UriBuilder(HttpClient.BaseAddress!)
        {
            Path = $"{lang}/api/0.4/crm/{_eogRequestCode}",
            Query = $"key={GetApiKey()}"
        };

        if (!loadChildEogData)
        {
            crmRequestUrl.Query += "&loadChildEogData=false";
        }

        if (!string.IsNullOrWhiteSpace(endpoint))
        {
            crmRequestUrl.Path += $"/{endpoint}";
        }

        crmRequestUrl.Path += $"/{fromPurchase}/{resultLimit}";

        return await MakeRequest(crmRequestUrl.ToString());
    }

    private async Task<string> MakeRequest(string url)
    {
        var response = await HttpClient.GetAsync(url);

        TryUpdateRateLimit(response);

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            switch (ex.StatusCode)
            {
                case HttpStatusCode.TooManyRequests:
                    throw new RateLimitExceededError(RateLimitInfo, ex);

                case HttpStatusCode.BadRequest:
                case HttpStatusCode.NotFound:
                    throw await CreateExceptionFromResponse(response, ex);

                default:
                    throw;
            }
        }

        return await response.Content.ReadAsStringAsync();
    }

    private void TryUpdateRateLimit(HttpResponseMessage response)
    {
        var limit = GetRateLimitHeader(response, "limit");
        var remaining = GetRateLimitHeader(response, "remaining");

        if (limit == null || remaining  == null)
            return;

        RateLimitInfo.FirstRequestAtUtc ??= DateTime.UtcNow;
        RateLimitInfo.LastRequestAtUtc = DateTime.UtcNow;
        RateLimitInfo.ConfiguredLimit = limit.Value;
        RateLimitInfo.RemainingRequests = remaining.Value;
    }

    private static int? GetRateLimitHeader(HttpResponseMessage response, string header)
    {
        if (!response.Headers.TryGetValues($"x-ratelimit-{header}", out var values))
            return null;

        if (int.TryParse(values.FirstOrDefault(), out int value))
            return value;

        return null;
    }

    private string GetApiKey()
        => HttpClient.DefaultRequestHeaders.GetValues("x-api-key").FirstOrDefault("");

    private static async Task<TicksterApiError> CreateExceptionFromResponse(HttpResponseMessage response, HttpRequestException originalException)
    {
        var content = await response.Content.ReadAsStringAsync();

        try
        {
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(content)
                ?? throw new JsonException();

            return BuildRequestException(errorResponse, originalException);
        }
        catch (JsonException)
        {
            try
            {
                var crmErrorResponse = JsonSerializer.Deserialize<CrmErrorResponse>(content)
                    ?? throw new JsonException();

                return BuildRequestException(crmErrorResponse, originalException);
            }
            catch (JsonException)
            {
                return new TicksterApiError("Unknown response format", originalException);
            }
        }
    }

    private static TicksterApiError BuildRequestException(ErrorResponse response, HttpRequestException e)
        => new(response.Title, e)
        {
            Type = response.Type,
            Title = response.Title,
            Detail = response.Detail,
            Instance = response.Instance,
            Status = response.Status,
            AdditionalProperties = [
                response.AdditionalProp1,
                response.AdditionalProp2,
                response.AdditionalProp3]
        };

    private static TicksterApiError BuildRequestException(CrmErrorResponse response, HttpRequestException e)
        => new(response.Error, e)
        {
            Title = response.Error,
            Status = (int?)e.StatusCode ?? 0
        };
}
