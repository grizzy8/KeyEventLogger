using KeyEventLogger.Core.Models;
using KeyEventLogger.Core.Services;

namespace KeyEventLogger.Tests;

public class LogFilterTests
{
    private static readonly DateTime Noon = new(2026, 11, 25, 12, 0, 0);

    private static KeyLogEntry At(int secondsAfterNoon, string key = "A") =>
        new(Noon.AddSeconds(secondsAfterNoon), KeyEventType.KeyDown, key);

    [Fact]
    public void ByTimeRange_NoBounds_ReturnsAllEntries()
    {
        KeyLogEntry[] entries = [At(0), At(10), At(20)];

        Assert.Equal(entries, LogFilter.ByTimeRange(entries, TimeRange.None));
    }

    [Fact]
    public void ByTimeRange_KeepsOnlyEntriesInsideRangeInOriginalOrder()
    {
        KeyLogEntry[] entries = [At(0, "A"), At(10, "B"), At(20, "C"), At(30, "D")];
        var range = new TimeRange(Noon.AddSeconds(10), Noon.AddSeconds(20));

        var result = LogFilter.ByTimeRange(entries, range);

        Assert.Equal(["B", "C"], result.Select(e => e.KeyName));
    }

    [Fact]
    public void ByTimeRange_BoundaryEntries_AreIncluded()
    {
        KeyLogEntry[] entries = [At(10), At(20)];
        var range = new TimeRange(Noon.AddSeconds(10), Noon.AddSeconds(20));

        Assert.Equal(2, LogFilter.ByTimeRange(entries, range).Count);
    }

    [Fact]
    public void ByTimeRange_NothingMatches_ReturnsEmpty()
    {
        KeyLogEntry[] entries = [At(0), At(10)];
        var range = new TimeRange(Noon.AddHours(1), null);

        Assert.Empty(LogFilter.ByTimeRange(entries, range));
    }

    [Fact]
    public void ByTimeRange_EmptyInput_ReturnsEmpty()
    {
        Assert.Empty(LogFilter.ByTimeRange([], new TimeRange(Noon, null)));
    }

    [Fact]
    public void ByTimeRange_DoesNotModifyInput()
    {
        List<KeyLogEntry> entries = [At(0), At(10)];

        LogFilter.ByTimeRange(entries, new TimeRange(Noon.AddSeconds(5), null));

        Assert.Equal(2, entries.Count);
    }

    [Fact]
    public void ByTimeRange_NullArguments_Throw()
    {
        Assert.Throws<ArgumentNullException>(() => LogFilter.ByTimeRange(null!, TimeRange.None));
        Assert.Throws<ArgumentNullException>(() => LogFilter.ByTimeRange([], null!));
    }
}
