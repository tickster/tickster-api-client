using Moq;

namespace Tickster.Api.Test.Unit.Crm;

public class TestGetCrmRequest : MockAgentBase
{
    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    public async Task GetCrmPurchasesAfterId_ShouldRequestNextCrmId(int crmId, int expectedId)
    {
        // Arrange
        SetupCrmMockResponse("crm-purchases-empty.json");

        // Act
        await TicksterClient.GetCrmPurchasesAfterId(crmId);

        // Assert
        MockAgent.Verify(c => c.MakeCrmRequest(It.IsAny<string>(), expectedId, It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
    }
}
