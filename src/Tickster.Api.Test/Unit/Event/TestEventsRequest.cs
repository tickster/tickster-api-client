using Moq;
using Tickster.Api.Dtos;
using Tickster.Api.Models.Event;

namespace Tickster.Api.Test.Unit.Event;

public class TestEventsRequest : MockAgentBase
{
    [Fact]
    public async Task Events_ShouldUseDefaultValuesIfNull()
    {
        // Arrange
        SetupMockResponse("events-empty.json");

        // Act
        await TicksterClient.Events();

        // Assert
        MockAgent.Verify(c => c.MakeApiRequest(It.IsAny<string>(), It.IsAny<string>(), TicksterOptions.DefaultApiVersion, TicksterOptions.DefaultLanguage, It.IsAny<Pagination>()), Times.Once);
    }

    [Fact]
    public async Task Events_ReturnsEventSummaryResourceCollection()
    {
        // Arrange
        SetupMockResponse("events-empty.json");

        // Act
        var result = await TicksterClient.Events();

        // Assert
        Assert.IsType<EventSummaryResourceCollection>(result);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(9, 99)]
    public async Task Events_PassesPaginationParameter(int take, int skip)
    {
        // Arrange
        SetupMockResponse("events-empty.json");
        var pagination = new Pagination { Take = take, Skip = skip };

        // Act
        var result = await TicksterClient.Events(pagination: pagination);

        // Assert
        MockAgent.Verify(c => c.MakeApiRequest(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), pagination), Times.Once);
    }

    [Theory]
    [InlineData("se")]
    [InlineData("dk")]
    [InlineData("no")]
    public async Task Events_CallsRequestWithExpectedParams(string lang)
    {
        // Arrange
        SetupMockResponse("events-empty.json");

        // Act
        var result = await TicksterClient.Events(lang: lang);

        // Assert
        MockAgent.Verify(c => c.MakeApiRequest("event", "events", TicksterOptions.DefaultApiVersion, lang, It.IsAny<Pagination>()), Times.Once);
    }
}
