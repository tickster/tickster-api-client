using Moq;
using Tickster.Api.Dtos;
using Tickster.Api.Models.Event;

namespace Tickster.Api.Test.Unit.Event;

public class TestOrganizersRequest : MockAgentBase
{
    [Fact]
    public async Task Organizers_ShouldUseDefaultValuesIfNull()
    {
        // Arrange
        SetupMockResponse("organizers-ok.json");

        // Act
        await TicksterClient.Organizers();

        // Assert
        MockAgent.Verify(c => c.MakeApiRequest(It.IsAny<string>(), It.IsAny<string>(), TicksterOptions.DefaultApiVersion, TicksterOptions.DefaultLanguage, It.IsAny<Pagination>()), Times.Once);
    }

    [Fact]
    public async Task Organizers_ReturnsListOfOrganizerSummaries()
    {
        // Arrange
        SetupMockResponse("organizers-ok.json");

        // Act
        var result = await TicksterClient.Organizers();

        // Assert
        Assert.IsType<List<OrganizerSummary>>(result);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(9, 99)]
    public async Task Organizers_PassesPaginationParameter(int take, int skip)
    {
        // Arrange
        SetupMockResponse("organizers-ok.json");
        var pagination = new Pagination { Take = take, Skip = skip };

        // Act
        var result = await TicksterClient.Organizers(pagination: pagination);

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
        SetupMockResponse("organizers-ok.json");

        // Act
        var result = await TicksterClient.Organizers(lang);

        // Assert
        MockAgent.Verify(c => c.MakeApiRequest("event", "organizers", TicksterOptions.DefaultApiVersion, lang, It.IsAny<Pagination>()), Times.Once);
    }
}
