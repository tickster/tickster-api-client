using Moq.Protected;
using Moq;
using System.Net;
using Tickster.Api;
using Tickster.Api.Test.Utils;
using Tickster.Api.Exceptions;

namespace Tickster.Api.Test.Unit;
public class TestTicksterHttpAgent
{
    private HttpClient HttpClient { get; set; } = new();

    [Fact]
    public async Task MakeCrmRequest_BuildsCrmEndpoint()
    {
        // Arrange
        var handlerMock = CreateMockResponse((request, cancel) =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://api.tickster.com/api/xx/0.4/crm/the-eog-req-code/10/15?key=the-api-key", request.RequestUri?.ToString());
        });
        var httpClient = CreateMockHttpClient(handlerMock);
        var agent = new TicksterHttpAgent(httpClient, "the-eog-req-code");

        // Act  
        await agent.MakeCrmRequest(string.Empty, 10, 15, "xx");
    }

    [Fact]
    public async Task MakeCrmRequest_LoadChildEogFlag()
    {
        // Arrange
        var handlerMock = CreateMockResponse((request, cancel) =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://api.tickster.com/api/xx/0.4/crm/the-eog-req-code/10/15?key=the-api-key&loadChildEogData=false", request.RequestUri?.ToString());
        });
        var httpClient = CreateMockHttpClient(handlerMock);
        var agent = new TicksterHttpAgent(httpClient, "the-eog-req-code");

        // Act  
        await agent.MakeCrmRequest(string.Empty, 10, 15, "xx", false);

    }

    [Fact]
    public async Task MakeCrmRequest_ReturnsResponseContent()
    {
        // Arrange
        var handlerMock = CreateMockResponse("the-response-content");
        var httpClient = CreateMockHttpClient(handlerMock);
        var agent = new TicksterHttpAgent(httpClient, "the-eog-req-code");

        // Act
        var response = await agent.MakeCrmRequest(string.Empty, 1, 10, "sv");

        // Assert
        Assert.Equal("the-response-content", response);
    }

    [Theory]
    [InlineData(HttpStatusCode.TooManyRequests, "Too many requests in the last hour, limit is 0")]
    public async Task MakeCrmRequest_ThrowApiError(HttpStatusCode statusCode, string description)
    {
        // Arrange
        var handlerMock = CreateMockResponseFromFile($"crm-error-{(int)statusCode}.json", statusCode);
        var httpClient = CreateMockHttpClient(handlerMock);
        var agent = new TicksterHttpAgent(httpClient, "the-eog-req-code");

        // Act & Assert
        var exception = await Assert.ThrowsAsync<TicksterApiError>(() => agent.MakeCrmRequest(string.Empty, 1, 10, "sv"));
        Assert.Equal(description, exception.Message);
        Assert.Equal((int)statusCode, exception.Status);
        Assert.IsType<HttpRequestException>(exception.InnerException);
        Assert.Equal(statusCode, ((HttpRequestException)exception.InnerException).StatusCode);
    }

    private static HttpClient CreateMockHttpClient(Mock<HttpMessageHandler> handlerMock)
        => new(handlerMock.Object)
        {
            BaseAddress = new Uri("https://api.tickster.com"),
            DefaultRequestHeaders =
            {
                { "x-api-key", "the-api-key" }
            }
        };

    private static Mock<HttpMessageHandler> CreateMockResponseFromFile(string fileName, HttpStatusCode statusCode = HttpStatusCode.OK)
        => CreateMockResponse(TestFileUtils.GetTestFileContent(fileName), (a, b) => { }, statusCode);

    private static Mock<HttpMessageHandler> CreateMockResponse(string responseContent, HttpStatusCode statusCode = HttpStatusCode.OK)
        => CreateMockResponse(responseContent, (a, b) => { }, statusCode);

    private static Mock<HttpMessageHandler> CreateMockResponse(Action<HttpRequestMessage, CancellationToken> requestAssertions, HttpStatusCode statusCode = HttpStatusCode.OK)
        => CreateMockResponse("", requestAssertions, statusCode);

    private static Mock<HttpMessageHandler> CreateMockResponse(string responseContent, Action<HttpRequestMessage, CancellationToken> requestAssertions, HttpStatusCode statusCode)
    {
        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .Callback<HttpRequestMessage, CancellationToken>(requestAssertions)
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(responseContent),
            });

        return handlerMock;
    }
}
