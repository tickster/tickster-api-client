using Microsoft.Extensions.Options;
using Moq;

namespace Tickster.Api.Test;
public abstract class RequestTestBase
{
    protected Mock<ITicksterHttpAgent> MockAgent { get; private set; }
    protected TicksterOptions TicksterOptions { get; private set; }
    protected TicksterClient TicksterClient { get; private set; }

    public RequestTestBase()
    {
        var options = Options.Create(new TicksterOptions
        {
            ApiKey = "test",
            Endpoint = "https://test.com",
            DefaultLanguage = "sv",
            DefaultResultLimit = 500,
            EogRequestCode = "test",
            Login = "test",
            Password = "test"
        });

        TicksterOptions = options.Value;
        MockAgent = new Mock<ITicksterHttpAgent>();

        TicksterClient = new TicksterClient(options, MockAgent.Object);
    }

    protected void SetupMockResponse(string fileName)
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestResponses", fileName);
        var rawJson = File.ReadAllText(filePath);

        MockAgent
            .Setup(a => a.MakeCrmRequest(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(rawJson);
    }
}
