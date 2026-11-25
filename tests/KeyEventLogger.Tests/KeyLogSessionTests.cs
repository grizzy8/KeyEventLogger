using KeyEventLogger.Core.Models;
using KeyEventLogger.Core.Services;

namespace KeyEventLogger.Tests;

public class KeyLogSessionTests
{
    private sealed class FixedTimeProvider(DateTime localTime) : TimeProvider
    {
        public override TimeZoneInfo LocalTimeZone => TimeZoneInfo.Utc;

        public override DateTimeOffset GetUtcNow() => new(DateTime.SpecifyKind(localTime, DateTimeKind.Utc));
    }

    private static readonly DateTime FixedTime = new(2026, 11, 25, 14, 3, 12, 345);

    private static KeyLogSession CreateSession() => new(new FixedTimeProvider(FixedTime));

    [Fact]
    public void NewSession_IsNotRecordingAndHasNoEntries()
    {
        var session = CreateSession();

        Assert.False(session.IsRecording);
        Assert.Empty(session.Entries);
    }

    [Fact]
    public void Start_SetsRecordingAndRaisesStateChangedOnce()
    {
        var session = CreateSession();
        var raised = 0;
        session.RecordingStateChanged += (_, _) => raised++;

        session.Start();
        session.Start();

        Assert.True(session.IsRecording);
        Assert.Equal(1, raised);
    }

    [Fact]
    public void Stop_ClearsRecordingAndRaisesStateChangedOnce()
    {
        var session = CreateSession();
        session.Start();
        var raised = 0;
        session.RecordingStateChanged += (_, _) => raised++;

        session.Stop();
        session.Stop();

        Assert.False(session.IsRecording);
        Assert.Equal(1, raised);
    }

    [Fact]
    public void Record_WhileRecording_AddsEntryWithProviderTimestamp()
    {
        var session = CreateSession();
        session.Start();

        var entry = session.Record(KeyEventType.KeyDown, "A", "Shift");

        Assert.NotNull(entry);
        Assert.Equal(FixedTime, entry.Timestamp);
        Assert.Equal("A", entry.KeyName);
        Assert.Equal("Shift", entry.Modifiers);
        Assert.Single(session.Entries);
    }

    [Fact]
    public void Record_WhileRecording_RaisesEntryAdded()
    {
        var session = CreateSession();
        session.Start();
        KeyLogEntry? received = null;
        session.EntryAdded += (_, e) => received = e;

        var entry = session.Record(KeyEventType.KeyUp, "B");

        Assert.Same(entry, received);
    }

    [Fact]
    public void Record_WhenStopped_IgnoresEvent()
    {
        var session = CreateSession();
        var raised = false;
        session.EntryAdded += (_, _) => raised = true;

        var entry = session.Record(KeyEventType.KeyDown, "A");

        Assert.Null(entry);
        Assert.Empty(session.Entries);
        Assert.False(raised);
    }

    [Fact]
    public void Record_WhenFormInactive_IgnoresEvent()
    {
        var session = CreateSession();
        session.Start();

        var entry = session.Record(KeyEventType.KeyDown, "A", isFormActive: false);

        Assert.Null(entry);
        Assert.Empty(session.Entries);
    }

    [Fact]
    public void Record_FromSensitiveInput_IgnoresEvent()
    {
        var session = CreateSession();
        session.Start();

        var entry = session.Record(KeyEventType.KeyDown, "P", isSensitiveInput: true);

        Assert.Null(entry);
        Assert.Empty(session.Entries);
    }

    [Fact]
    public void Clear_RemovesEntriesKeepsRecordingStateAndRaisesCleared()
    {
        var session = CreateSession();
        session.Start();
        session.Record(KeyEventType.KeyDown, "A");
        var raised = 0;
        session.Cleared += (_, _) => raised++;

        session.Clear();

        Assert.Empty(session.Entries);
        Assert.True(session.IsRecording);
        Assert.Equal(1, raised);
    }

    [Fact]
    public void Entries_PreservesRecordingOrder()
    {
        var session = CreateSession();
        session.Start();

        session.Record(KeyEventType.KeyDown, "A");
        session.Record(KeyEventType.KeyUp, "A");
        session.Record(KeyEventType.KeyDown, "B");

        Assert.Equal(["A", "A", "B"], session.Entries.Select(e => e.KeyName));
        Assert.Equal(
            [KeyEventType.KeyDown, KeyEventType.KeyUp, KeyEventType.KeyDown],
            session.Entries.Select(e => e.EventType));
    }
}
