using Moq;
using Tickster.Api.Dtos;
using EventModel = Tickster.Api.Models.Event.Event;

namespace Tickster.Api.Test.Unit.Event;

public class TestEventRequest : MockAgentBase
{
    [Fact]
    public async Task Event_ShouldUseDefaultValuesIfNull()
    {
        // Arrange
        SetupMockResponse("event-ok.json");

        // Act
        await TicksterClient.Event("4ny1d");

        // Assert
        MockAgent.Verify(c => c.MakeApiRequest(It.IsAny<string>(), It.IsAny<string>(), TicksterOptions.DefaultApiVersion, TicksterOptions.DefaultLanguage, It.IsAny<Pagination>()), Times.Once);
    }    

    [Theory]
    [InlineData("1234", "1.0", "se")]
    [InlineData("4321", "0.7", "dk")]
    [InlineData("4132", "10.0", "no")]
    public async Task Event_CallsRequestWithExpectedParams(string id, string version, string lang)
    {
        // Arrange
        SetupMockResponse("event-ok.json");

        // Act
        await TicksterClient.Event(id, version, lang);

        // Assert
        MockAgent.Verify(c => c.MakeApiRequest("event", $"events/{id}", version, lang, It.IsAny<Pagination>()), Times.Once);
    }

    [Fact]
    public async Task Event_ReturnsEvent()
    {
        // Arrange
        SetupMockResponse("event-ok.json");

        // Act
        var result = await TicksterClient.Event("4ny1d");

        // Assert
        Assert.IsType<EventModel>(result);
    }
}
