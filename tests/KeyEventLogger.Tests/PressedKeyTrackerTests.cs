using KeyEventLogger.Core.Services;

namespace KeyEventLogger.Tests;

public class PressedKeyTrackerTests
{
    [Fact]
    public void TryPress_FirstPress_ReturnsTrue()
    {
        var tracker = new PressedKeyTracker();

        Assert.True(tracker.TryPress("A"));
    }

    [Fact]
    public void TryPress_RepeatWhileHeld_ReturnsFalse()
    {
        var tracker = new PressedKeyTracker();
        tracker.TryPress("A");

        Assert.False(tracker.TryPress("A"));
    }

    [Fact]
    public void TryPress_AfterRelease_ReturnsTrueAgain()
    {
        var tracker = new PressedKeyTracker();
        tracker.TryPress("A");
        tracker.Release("A");

        Assert.True(tracker.TryPress("A"));
    }

    [Fact]
    public void TryPress_DifferentKeys_AreTrackedIndependently()
    {
        var tracker = new PressedKeyTracker();
        tracker.TryPress("A");

        Assert.True(tracker.TryPress("B"));
        Assert.Equal(2, tracker.Count);
    }

    [Fact]
    public void Release_HeldKey_ReturnsTrue()
    {
        var tracker = new PressedKeyTracker();
        tracker.TryPress("A");

        Assert.True(tracker.Release("A"));
        Assert.Equal(0, tracker.Count);
    }

    [Fact]
    public void Release_KeyThatWasNotPressed_ReturnsFalse()
    {
        var tracker = new PressedKeyTracker();

        Assert.False(tracker.Release("A"));
        Assert.Equal(0, tracker.Count);
    }

    [Fact]
    public void Release_SameKeyTwice_ReturnsFalseSecondTime()
    {
        var tracker = new PressedKeyTracker();
        tracker.TryPress("A");
        tracker.Release("A");

        Assert.False(tracker.Release("A"));
    }

    [Fact]
    public void Reset_ForgetsAllHeldKeys()
    {
        var tracker = new PressedKeyTracker();
        tracker.TryPress("A");
        tracker.TryPress("Shift");

        tracker.Reset();

        Assert.Equal(0, tracker.Count);
        Assert.True(tracker.TryPress("A"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void TryPress_BlankKeyName_Throws(string? keyName)
    {
        var tracker = new PressedKeyTracker();

        Assert.ThrowsAny<ArgumentException>(() => tracker.TryPress(keyName!));
    }
}
