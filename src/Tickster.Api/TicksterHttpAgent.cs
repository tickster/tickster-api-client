using System.Net;
using System.Text.Json;
using System.Web;
using Tickster.Api.Dtos;
using Tickster.Api.Exceptions;

namespace Tickster.Api;

public class TicksterHttpAgent(HttpClient client, string? eogRequestCode)
{
    public HttpClient HttpClient => client;
    private readonly string _eogRequestCode = eogRequestCode ?? string.Empty;

    public async Task<string> MakeCrmRequest(string endpoint, int fromPurchase, int resultLimit, string lang)
    {
        if (string.IsNullOrEmpty(_eogRequestCode))
        {
            throw new InvalidOperationException("EOG request code is not set.");
        }

        var crmRequestUrl = new UriBuilder(HttpClient.BaseAddress!)
        {
            Path = $"/api/{lang}/0.4/crm/{_eogRequestCode}",
            Query = $"key={GetApiKey()}"
        };

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

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            switch(ex.StatusCode)
            {
                case HttpStatusCode.TooManyRequests:
                    // FIXME: Handle backoff
                    break;
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.NotFound:
                    throw await CreateExceptionFromResponse(response, ex);

                default:
                    throw;
            }
        }

        return await response.Content.ReadAsStringAsync();
    }

    private string GetApiKey()
        => HttpClient.DefaultRequestHeaders.GetValues("x-api-key").FirstOrDefault("");

    private static async Task<TicksterApiError> CreateExceptionFromResponse(HttpResponseMessage response, Exception originalException)
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

    private static TicksterApiError BuildRequestException(ErrorResponse response, Exception e)
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

    private static TicksterApiError BuildRequestException(CrmErrorResponse response, Exception e)
        => new(response.Error, e)
        {
            Title = response.Error
        };
}

public class SearchQuery
{
    public int Take { get; set; } = 10;
    public int Skip { get; set; } = 0;
    public string? Search { get; set; }
    public string? Prefix { get; set; }
    public string? Filter { get; set; }

    public string ToQueryString()
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["take"] = Take.ToString();
        query["skip"] = Skip.ToString();

        if (!string.IsNullOrWhiteSpace(Search))
        {
            query["query"] = Search;

            if (!string.IsNullOrWhiteSpace(Prefix) && !string.IsNullOrWhiteSpace(Filter))
            {
                query["query"] += $" {Prefix}:{Filter}";
            }
        }

        return query.ToString() ?? string.Empty;
    }
}
