using Moq.Protected;
using Moq;
using System.Net;
using Tickster.Api;
using Tickster.Api.Test.Utils;

namespace Tickster.Api.Test.Unit;
public class TestTicksterHttpAgent : MockHttpClientBase
{
    [Fact]
    public async Task MakeCrmRequest_BuildsCrmEndpoint()
    {
        // Arrange
        RequestCallback = (request, cancel) =>
        {
            // Assert
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://api.example.com/xx/api/0.4/crm/the-eog-code/10/15?key=the-api-key", request.RequestUri?.ToString());
        };

        SetupMockResponse();

        // Act  
        await Agent.MakeCrmRequest(string.Empty, 10, 15, "xx");
    }

    [Fact]
    public async Task MakeCrmRequest_CanSetLoadChildEogFlag()
    {
        // Arrange
        RequestCallback = (request, cancel) =>
        {
            // Assert
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://api.example.com/xx/api/0.4/crm/the-eog-code/10/15?key=the-api-key&loadChildEogData=false", request.RequestUri?.ToString());
        };

        SetupMockResponse();

        // Act  
        await Agent.MakeCrmRequest(string.Empty, 10, 15, "xx", false);
    }

    [Fact]
    public async Task MakeCrmRequest_ReturnsResponseContent()
    {
        // Arrange
        ResponseContent = "the-response-content";

        SetupMockResponse();

        // Act
        var response = await Agent.MakeCrmRequest(string.Empty, 1, 10, "sv");

        // Assert
        Assert.Equal("the-response-content", response);
    }

    //[Theory]
    //[InlineData(HttpStatusCode.TooManyRequests, "Too many requests in the last hour, limit is 0")]
    //public async Task MakeCrmRequest_ThrowsApiError(HttpStatusCode statusCode, string description)
    //{
    //    // Arrange
    //    var handlerMock = CreateMockResponseFromFile($"crm-error-{(int)statusCode}.json", statusCode);
    //    var httpClient = CreateMockHttpClient(handlerMock);
    //    var agent = new TicksterHttpAgent(httpClient, "the-eog-req-code");

    //    // Act & Assert
    //    var exception = await Assert.ThrowsAsync<TicksterApiError>(() => agent.MakeCrmRequest(string.Empty, 1, 10, "sv"));
    //    Assert.Equal(description, exception.Message);
    //    Assert.Equal((int)statusCode, exception.Status);
    //    Assert.IsType<HttpRequestException>(exception.InnerException);
    //    Assert.Equal(statusCode, ((HttpRequestException)exception.InnerException).StatusCode);
    //}

    [Fact]
    public async Task MakeCrmRequest_SetsRateLimit()
    {
        // Arrange
        ResponseHeaders = new()
        {
            { "x-ratelimit-limit", "200" },
            { "x-ratelimit-remaining", "198" }
        };

        SetupMockResponse();

        // Act & Assert

        var rateLimit = Agent.RateLimitInfo;

        Assert.Equal(0, rateLimit.ConfiguredLimit);
        Assert.Equal(0, rateLimit.RemainingRequests);
        Assert.Null(rateLimit.LastRequestAtUtc);
        Assert.Null(rateLimit.FirstRequestAtUtc);

        await Agent.MakeCrmRequest(string.Empty, 10, 15, "xx");

        var now = DateTime.UtcNow;
        rateLimit = Agent.RateLimitInfo;

        Assert.Equal(200, rateLimit.ConfiguredLimit);
        Assert.Equal(198, rateLimit.RemainingRequests);
        Assert.NotNull(rateLimit.LastRequestAtUtc);
        Assert.NotNull(rateLimit.FirstRequestAtUtc);
        Assert.InRange((DateTime)rateLimit.LastRequestAtUtc, now.AddSeconds(-1), now.AddSeconds(1));
        Assert.InRange((DateTime)rateLimit.FirstRequestAtUtc, now.AddSeconds(-1), now.AddSeconds(1));
    }
}
