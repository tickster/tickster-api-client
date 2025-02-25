using Microsoft.Extensions.Options;
using Moq;
using Tickster.Api.Dtos;
using Tickster.Api.Test.Utils;

namespace Tickster.Api.Test;
public abstract class MockAgentBase
{
    protected Mock<ITicksterHttpAgent> MockAgent { get; private set; }
    protected TicksterOptions TicksterOptions { get; private set; }
    protected TicksterClient TicksterClient { get; private set; }

    public MockAgentBase()
    {
        var options = Options.Create(new TicksterOptions
        {
            ApiKey = "test",
            CrmBaseUrl = "https://test.com",
            EventBaseUrl = "https://event.test.com",
            DefaultLanguage = "sv",
            DefaultResultLimit = 500,
            EogRequestCode = "test",
            Login = "test",
            Password = "test",
            DefaultApiVersion = "1.0"
        });

        TicksterOptions = options.Value;
        MockAgent = new Mock<ITicksterHttpAgent>();

        TicksterClient = new TicksterClient(options, MockAgent.Object);
    }

    protected void SetupCrmMockResponse(string fileName)
    {
        var rawJson = TestFileUtils.GetTestFileContent(fileName);
        
        MockAgent
            .Setup(a => a.MakeCrmRequest(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>()))
            .ReturnsAsync(rawJson);
    }

    protected void SetupMockResponse(string fileName)
    {
        var rawJson = TestFileUtils.GetTestFileContent(fileName);

        MockAgent
            .Setup(a => a.MakeApiRequest(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Pagination>()))
            .ReturnsAsync(rawJson);
    }
}
