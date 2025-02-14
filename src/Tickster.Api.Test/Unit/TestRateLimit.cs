using System.Net;
using Tickster.Api.Exceptions;
using Tickster.Api.Test.Utils;

namespace Tickster.Api.Test.Unit;
public class TestRateLimit : MockHttpClientBase
{
    [Fact]
    public void Test_HasAccessor()
    {
        var rateLimit = TicksterClient.RateLimitInfo;

        Assert.StrictEqual(rateLimit, Agent.RateLimitInfo);
        Assert.Equal(0, rateLimit.ConfiguredLimit);
        Assert.Equal(0, rateLimit.RemainingRequests);
        Assert.Null(rateLimit.FirstRequestAtUtc);
        Assert.Null(rateLimit.LastRequestAtUtc);
    }

    [Fact]
    public async Task Test_SetsValues()
    {
        ResponseHeaders = new()
        {
            { "X-RateLimit-Limit", "100" },
            { "X-RateLimit-Remaining", "50" },
        };
        SetupMockResponse();

        await TicksterClient.GetCrmPurchasesAtId(1);

        var rateLimit = TicksterClient.RateLimitInfo;

        Assert.Equal(100, rateLimit.ConfiguredLimit);
        Assert.Equal(50, rateLimit.RemainingRequests);
        Assert.NotNull(rateLimit.FirstRequestAtUtc);
        Assert.NotNull(rateLimit.LastRequestAtUtc);
        AssertDateTime.AlmostUtcNow(rateLimit.FirstRequestAtUtc.Value);
        AssertDateTime.AlmostUtcNow(rateLimit.LastRequestAtUtc.Value);
    }

    [Fact]
    public async Task Test_Accessor_Updated()
    {
        var rateLimit = TicksterClient.RateLimitInfo;
        rateLimit.ConfiguredLimit = 100;
        rateLimit.RemainingRequests = 99;

        ResponseHeaders = new()
        {
            { "X-RateLimit-Limit", "100" },
            { "X-RateLimit-Remaining", "98" },
        };

        SetupMockResponse();

        await TicksterClient.GetCrmPurchasesAtId(1);

        var updatedRateLimit = TicksterClient.RateLimitInfo;

        Assert.StrictEqual(rateLimit, updatedRateLimit);
        Assert.Equal(100, updatedRateLimit.ConfiguredLimit);
        Assert.Equal(98, updatedRateLimit.RemainingRequests);
    }

    [Fact]
    public async Task Test_Dates_Updated()
    {
        var originalTime = DateTime.UtcNow.AddHours(-1);
        var rateLimit = TicksterClient.RateLimitInfo;
        rateLimit.FirstRequestAtUtc = originalTime;
        rateLimit.LastRequestAtUtc = originalTime;

        ResponseHeaders = new()
        {
            { "X-RateLimit-Limit", "100" },
            { "X-RateLimit-Remaining", "98" },
        };

        SetupMockResponse();

        await TicksterClient.GetCrmPurchasesAtId(1);

        var updatedRateLimit = TicksterClient.RateLimitInfo;

        Assert.Equal(originalTime, updatedRateLimit.FirstRequestAtUtc);
        AssertDateTime.AlmostUtcNow(updatedRateLimit.LastRequestAtUtc!.Value);
    }

    [Fact]
    public async Task Test_Throws_RateLimitExceededError()
    {
        ResponseCode = HttpStatusCode.TooManyRequests;
        ResponseHeaders = new()
        {
            { "X-RateLimit-Limit", "100" },
            { "X-RateLimit-Remaining", "0" },
        };

        SetupMockResponse();

        var ex = await Assert.ThrowsAsync<RateLimitExceededError>(() => TicksterClient.GetCrmPurchasesAtId(1));
        Assert.NotNull(ex.RateLimitInfo);
        Assert.Equal(100, ex.RateLimitInfo.ConfiguredLimit);
        Assert.Equal(0, ex.RateLimitInfo.RemainingRequests);
        Assert.IsType<HttpRequestException>(ex.InnerException);
        Assert.Equal(HttpStatusCode.TooManyRequests, ((HttpRequestException)ex.InnerException).StatusCode);
    }
}
