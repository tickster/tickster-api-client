using Microsoft.Extensions.Options;
using Moq;

namespace Tickster.Api.Test.Unit.Crm;
public class TestCrmOptions : MockAgentBase
{
    [Fact]
    public async Task GetCrmPurchasesAsync_ShouldInvokeAgentWithLoadChildEogDataTrue_ByDefault()
    {

        // Arrange
        var purchaseId = 123;
        SetupMockResponse("crm-purchases-ok.json");

        // Act
        await TicksterClient.GetCrmPurchasesAsync(purchaseId);

        // Assert
        MockAgent.Verify(agent => agent.MakeCrmRequest(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), true), Times.Once);
    }

    [Fact]
    public async Task GetCrmPurchasesAsync_ShouldInvokeAgentWithLoadChildEogDataFalse_WhenIncludeAllAccountsIsFalse()
    {

        // Arrange
        var purchaseId = 123;
        var options = new GetCrmPurchasesOptions { IncludeAllAccounts = false };
        SetupMockResponse("crm-purchases-ok.json");

        // Act
        await TicksterClient.GetCrmPurchasesAsync(purchaseId, options: options);

        // Assert
        MockAgent.Verify(agent => agent.MakeCrmRequest(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), false), Times.Once);
    }

    [Fact]
    public async Task GetCrmPurchasesAsync_ShouldFilterOutUnsyncedRefunds_ByDefault()
    {
        // Arrange
        var purchaseId = 123;
        SetupMockResponse("crm-purchases-unsynced-refund.json");
        // Act
        var result = await TicksterClient.GetCrmPurchasesAsync(purchaseId);
        // Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task GetCrmPurchasesAsync_ShouldNotFilterOutUnsyncedRefunds_WhenSuppressUnsyncedRefundsIsFalse()
    {
        // Arrange
        var purchaseId = 123;
        var options = new GetCrmPurchasesOptions { SuppressUnsyncedRefunds = false };
        SetupMockResponse("crm-purchases-unsynced-refund.json");
        // Act
        var result = await TicksterClient.GetCrmPurchasesAsync(purchaseId, options: options);
        // Assert
        Assert.Equal(2, result.Count());
    }
}
