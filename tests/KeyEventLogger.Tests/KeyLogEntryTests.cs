using KeyEventLogger.Core.Models;

namespace KeyEventLogger.Tests;

public class KeyLogEntryTests
{
    private static readonly DateTime SampleTime = new(2026, 11, 25, 14, 3, 12, 345);

    [Fact]
    public void ToLogLine_WithModifiers_FormatsAllFields()
    {
        var entry = new KeyLogEntry(SampleTime, KeyEventType.KeyDown, "A", "Shift");

        Assert.Equal("2026-11-25 14:03:12.345 | KeyDown | A | Shift", entry.ToLogLine());
    }

    [Fact]
    public void ToLogLine_WithoutModifiers_UsesDashPlaceholder()
    {
        var entry = new KeyLogEntry(SampleTime, KeyEventType.KeyUp, "Space");

        Assert.Equal("2026-11-25 14:03:12.345 | KeyUp | Space | -", entry.ToLogLine());
    }

    [Fact]
    public void ToLogLine_PadsMillisecondsToThreeDigits()
    {
        var entry = new KeyLogEntry(new DateTime(2026, 1, 2, 3, 4, 5, 6), KeyEventType.KeyDown, "B");

        Assert.StartsWith("2026-01-02 03:04:05.006 |", entry.ToLogLine());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_BlankKeyName_Throws(string? keyName)
    {
        Assert.ThrowsAny<ArgumentException>(
            () => new KeyLogEntry(SampleTime, KeyEventType.KeyDown, keyName!));
    }

    [Fact]
    public void Constructor_UndefinedEventType_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new KeyLogEntry(SampleTime, (KeyEventType)99, "A"));
    }

    [Fact]
    public void Entries_WithSameValues_AreEqual()
    {
        var first = new KeyLogEntry(SampleTime, KeyEventType.KeyDown, "A", "Ctrl");
        var second = new KeyLogEntry(SampleTime, KeyEventType.KeyDown, "A", "Ctrl");

        Assert.Equal(first, second);
    }
}
