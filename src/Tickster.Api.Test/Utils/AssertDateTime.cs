
using Xunit.Sdk;

namespace Tickster.Api.Test.Utils;
internal static class AssertDateTime
{

    public static void AlmostUtcNow(DateTime actual, int deltaSeconds = 1)
        => AlmostEqual(actual, DateTime.UtcNow, deltaSeconds);

    public static void AlmostEqual(DateTime actual, DateTime expected, int deltaSeconds = 1)
    {
        var delta = TimeSpan.FromSeconds(deltaSeconds);

        if (actual < expected - delta || actual > expected + delta)
        {
            throw new XunitException($"Expected {actual} to be within {deltaSeconds} seconds of {expected}");
        }
    }
}
