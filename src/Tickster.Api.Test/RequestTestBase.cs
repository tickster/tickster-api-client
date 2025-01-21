using Microsoft.Extensions.Options;
using Moq;
using Tickster.Api.Test.Utils;

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
        var rawJson = TestFileUtils.GetTestFileContent(fileName);
        
        MockAgent
            .Setup(a => a.MakeCrmRequest(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>()))
            .ReturnsAsync(rawJson);
    }
}
