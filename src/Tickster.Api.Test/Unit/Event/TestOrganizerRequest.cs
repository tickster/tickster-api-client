using Moq;
using Tickster.Api.Dtos;
using Tickster.Api.Models.Event;

namespace Tickster.Api.Test.Unit.Event;

public class TestOrganizerRequest : MockAgentBase
{
    [Fact]
    public async Task Organizer_ShouldUseDefaultValuesIfNull()
    {
        // Arrange
        SetupMockResponse("organizer-ok.json");

        // Act
        var result = await TicksterClient.Organizer("abc123xyz789");

        // Assert
        MockAgent.Verify(c => c.MakeApiRequest(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), TicksterOptions.DefaultLanguage, It.IsAny<Pagination>()), Times.Once);
    }

    [Fact]
    public async Task Organizer_ReturnsOrganizer()
    {
        // Arrange
        SetupMockResponse("organizer-ok.json");

        // Act
        var result = await TicksterClient.Organizer("abc123xyz789");

        // Assert
        Assert.IsType<Organizer>(result);
    }

    [Theory]
    [InlineData("abc1234", "se")]
    [InlineData("cba4321", "dk")]
    [InlineData("bca4132", "no")]
    public async Task Organizer_CallsRequestWithExpectedParams(string id, string lang)
    {
        // Arrange
        SetupMockResponse("organizer-ok.json");

        // Act
        await TicksterClient.Organizer(id, lang);

        // Assert
        MockAgent.Verify(c => c.MakeApiRequest("event", $"organizers/{id}", TicksterOptions.DefaultApiVersion, lang, It.IsAny<Pagination>()), Times.Once);
    }
}
