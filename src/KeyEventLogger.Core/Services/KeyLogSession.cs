using KeyEventLogger.Core.Models;

namespace KeyEventLogger.Core.Services;

/// <summary>
/// Holds the in-memory log and the recording state. A new session starts stopped;
/// nothing is recorded until <see cref="Start"/> is called.
/// </summary>
public sealed class KeyLogSession
{
    private readonly List<KeyLogEntry> _entries = [];
    private readonly TimeProvider _timeProvider;

    public KeyLogSession(TimeProvider? timeProvider = null)
    {
        _timeProvider = timeProvider ?? TimeProvider.System;
    }

    public bool IsRecording { get; private set; }

    public IReadOnlyList<KeyLogEntry> Entries => _entries.AsReadOnly();

    public event EventHandler? RecordingStateChanged;

    public event EventHandler<KeyLogEntry>? EntryAdded;

    public event EventHandler? Cleared;

    public void Start() => SetRecording(true);

    public void Stop() => SetRecording(false);

    /// <summary>Removes all entries. The recording state is left unchanged.</summary>
    public void Clear()
    {
        _entries.Clear();
        Cleared?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Records an event if <see cref="CapturePolicy"/> allows it.
    /// </summary>
    /// <returns>The new entry, or <c>null</c> when the event was ignored.</returns>
    public KeyLogEntry? Record(
        KeyEventType eventType,
        string keyName,
        string modifiers = "",
        bool isFormActive = true,
        bool isSensitiveInput = false)
    {
        if (!CapturePolicy.ShouldCapture(IsRecording, isFormActive, isSensitiveInput))
        {
            return null;
        }

        var timestamp = _timeProvider.GetLocalNow().DateTime;
        var entry = new KeyLogEntry(timestamp, eventType, keyName, modifiers);

        _entries.Add(entry);
        EntryAdded?.Invoke(this, entry);

        return entry;
    }

    private void SetRecording(bool isRecording)
    {
        if (IsRecording == isRecording)
        {
            return;
        }

        IsRecording = isRecording;
        RecordingStateChanged?.Invoke(this, EventArgs.Empty);
    }
}
