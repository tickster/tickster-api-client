using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;
using Tickster.Api.Test.Utils;

namespace Tickster.Api.Test;
public abstract class MockHttpClientBase
{
    protected Mock<HttpMessageHandler> MockHandler { get; private set; }
    protected TicksterHttpAgent Agent { get; private set; }
    protected TicksterOptions TicksterOptions { get; private set; }
    protected TicksterClient TicksterClient { get; private set; }

    protected Action<HttpRequestMessage, CancellationToken> RequestCallback { get; set; } = (request, token) => { };
    protected Dictionary<string, string> ResponseHeaders { get; set; } = [];
    protected HttpStatusCode ResponseCode { get; set; } = HttpStatusCode.OK;
    protected string ResponseContent { get; set; } = "{}";


    public MockHttpClientBase()
    {
        var options = Options.Create(new TicksterOptions
        {
            ApiKey = "the-api-key",
            Endpoint = "https://api.example.com",
            DefaultLanguage = "sv",
            DefaultResultLimit = 500,
            EogRequestCode = "the-eog-code",
            Login = "the-login",
            Password = "the-password",
        });
        TicksterOptions = options.Value;
        MockHandler = new Mock<HttpMessageHandler>();

        var httpClient = new HttpClient(MockHandler.Object)
        {
            BaseAddress = new Uri(TicksterOptions.Endpoint),
            DefaultRequestHeaders =
            {
                { "x-api-key", TicksterOptions.ApiKey }
            }
        };

        Agent = new TicksterHttpAgent(httpClient, TicksterOptions.EogRequestCode);
        TicksterClient = new TicksterClient(options, Agent);
    }
    
    protected void SetupMockResponseFromFile(string fileName, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        ResponseContent = TestFileUtils.GetTestFileContent(fileName);
        ResponseCode = statusCode;

        SetupMockResponse();
    }

    protected void SetupMockResponse()
    {

        var response = new HttpResponseMessage(ResponseCode)
        {
            Content = new StringContent(ResponseContent, Encoding.UTF8, "application/json"),
            StatusCode = ResponseCode
        };

        foreach (var header in ResponseHeaders)
        {
            response.Headers.Add(header.Key, header.Value);
        }

        MockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .Callback(RequestCallback)
            .ReturnsAsync(response);
    }
}
