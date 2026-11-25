using KeyEventLogger.Core.Models;
using KeyEventLogger.Core.Services;

namespace KeyEventLogger.Tests;

public class KeyStatisticsTests
{
    private static readonly DateTime SampleTime = new(2026, 11, 25, 14, 0, 0);

    private static KeyLogEntry Down(string key) => new(SampleTime, KeyEventType.KeyDown, key);

    private static KeyLogEntry Up(string key) => new(SampleTime, KeyEventType.KeyUp, key);

    [Fact]
    public void TotalKeyPresses_EmptyList_IsZero()
    {
        Assert.Equal(0, KeyStatistics.TotalKeyPresses([]));
    }

    [Fact]
    public void TotalKeyPresses_CountsOnlyKeyDownEvents()
    {
        KeyLogEntry[] entries = [Down("A"), Up("A"), Down("B"), Up("B"), Down("A")];

        Assert.Equal(3, KeyStatistics.TotalKeyPresses(entries));
    }

    [Fact]
    public void TopKeys_OrdersByCountDescending()
    {
        KeyLogEntry[] entries = [Down("A"), Down("B"), Down("B"), Down("C"), Down("C"), Down("C")];

        var top = KeyStatistics.TopKeys(entries, 3);

        Assert.Equal([new KeyCount("C", 3), new KeyCount("B", 2), new KeyCount("A", 1)], top);
    }

    [Fact]
    public void TopKeys_LimitsResultToRequestedCount()
    {
        KeyLogEntry[] entries = [Down("A"), Down("B"), Down("C"), Down("D")];

        Assert.Equal(2, KeyStatistics.TopKeys(entries, 2).Count);
    }

    [Fact]
    public void TopKeys_FewerDistinctKeysThanRequested_ReturnsAllOfThem()
    {
        KeyLogEntry[] entries = [Down("A"), Down("A")];

        Assert.Single(KeyStatistics.TopKeys(entries, 5));
    }

    [Fact]
    public void TopKeys_Ties_AreOrderedByKeyNameForStableOutput()
    {
        KeyLogEntry[] entries = [Down("Z"), Down("M"), Down("A")];

        var names = KeyStatistics.TopKeys(entries, 3).Select(k => k.KeyName);

        Assert.Equal(["A", "M", "Z"], names);
    }

    [Fact]
    public void TopKeys_IgnoresKeyUpEvents()
    {
        KeyLogEntry[] entries = [Up("A"), Up("A"), Down("B")];

        Assert.Equal([new KeyCount("B", 1)], KeyStatistics.TopKeys(entries, 5));
    }

    [Fact]
    public void TopKeys_EmptyList_ReturnsEmpty()
    {
        Assert.Empty(KeyStatistics.TopKeys([], 5));
    }

    [Fact]
    public void TopKeys_ZeroCount_ReturnsEmpty()
    {
        Assert.Empty(KeyStatistics.TopKeys([Down("A")], 0));
    }

    [Fact]
    public void TopKeys_NegativeCount_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => KeyStatistics.TopKeys([Down("A")], -1));
    }

    [Fact]
    public void Methods_NullEntries_Throw()
    {
        Assert.Throws<ArgumentNullException>(() => KeyStatistics.TotalKeyPresses(null!));
        Assert.Throws<ArgumentNullException>(() => KeyStatistics.TopKeys(null!, 5));
    }
}
