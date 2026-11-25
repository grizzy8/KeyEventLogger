using KeyEventLogger.Core.Models;

namespace KeyEventLogger.Tests;

public class TimeRangeTests
{
    private static readonly DateTime Noon = new(2026, 11, 25, 12, 0, 0);

    [Fact]
    public void None_IsNotActiveAndContainsEverything()
    {
        Assert.False(TimeRange.None.IsActive);
        Assert.True(TimeRange.None.Contains(DateTime.MinValue));
        Assert.True(TimeRange.None.Contains(DateTime.MaxValue));
    }

    [Fact]
    public void Contains_BothBounds_AreInclusive()
    {
        var range = new TimeRange(Noon, Noon.AddHours(1));

        Assert.True(range.Contains(Noon));
        Assert.True(range.Contains(Noon.AddMinutes(30)));
        Assert.True(range.Contains(Noon.AddHours(1)));
        Assert.False(range.Contains(Noon.AddTicks(-1)));
        Assert.False(range.Contains(Noon.AddHours(1).AddTicks(1)));
    }

    [Fact]
    public void Contains_OnlyFrom_IsOpenEndedUpwards()
    {
        var range = new TimeRange(Noon, null);

        Assert.True(range.IsActive);
        Assert.False(range.Contains(Noon.AddSeconds(-1)));
        Assert.True(range.Contains(DateTime.MaxValue));
    }

    [Fact]
    public void Contains_OnlyTo_IsOpenEndedDownwards()
    {
        var range = new TimeRange(null, Noon);

        Assert.True(range.IsActive);
        Assert.True(range.Contains(DateTime.MinValue));
        Assert.False(range.Contains(Noon.AddSeconds(1)));
    }

    [Fact]
    public void Constructor_FromAfterTo_Throws()
    {
        Assert.Throws<ArgumentException>(() => new TimeRange(Noon.AddSeconds(1), Noon));
    }

    [Fact]
    public void Constructor_FromEqualsTo_IsAllowed()
    {
        var range = new TimeRange(Noon, Noon);

        Assert.True(range.Contains(Noon));
    }

    [Fact]
    public void FromWholeSeconds_TruncatesFromToStartOfSecond()
    {
        var range = TimeRange.FromWholeSeconds(Noon.AddMilliseconds(750), null);

        Assert.Equal(Noon, range.From);
        Assert.True(range.Contains(Noon.AddMilliseconds(1)));
    }

    [Fact]
    public void FromWholeSeconds_ExtendsToToEndOfSecond()
    {
        var range = TimeRange.FromWholeSeconds(null, Noon.AddMilliseconds(120));

        Assert.True(range.Contains(Noon.AddMilliseconds(999)));
        Assert.False(range.Contains(Noon.AddSeconds(1)));
    }

    [Fact]
    public void FromWholeSeconds_NoBounds_IsNotActive()
    {
        Assert.False(TimeRange.FromWholeSeconds(null, null).IsActive);
    }

    [Fact]
    public void FromWholeSeconds_FromSecondAfterToSecond_Throws()
    {
        Assert.Throws<ArgumentException>(
            () => TimeRange.FromWholeSeconds(Noon.AddSeconds(5), Noon.AddSeconds(3)));
    }

    [Fact]
    public void FromWholeSeconds_SameSecondDifferentMilliseconds_IsValid()
    {
        var range = TimeRange.FromWholeSeconds(Noon.AddMilliseconds(900), Noon.AddMilliseconds(100));

        Assert.True(range.Contains(Noon.AddMilliseconds(500)));
    }
}
