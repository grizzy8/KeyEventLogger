using System.Globalization;

namespace KeyEventLogger.Core.Models;

/// <summary>
/// One recorded keyboard event. Immutable; the key is stored by name so this
/// type has no dependency on Windows Forms.
/// </summary>
public sealed record KeyLogEntry
{
    private const string TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff";
    private const string NoModifiersPlaceholder = "-";
    private const string FieldSeparator = " | ";

    public KeyLogEntry(DateTime timestamp, KeyEventType eventType, string keyName, string modifiers = "")
    {
        if (!Enum.IsDefined(eventType))
        {
            throw new ArgumentOutOfRangeException(nameof(eventType), eventType, "Unknown key event type.");
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(keyName);

        Timestamp = timestamp;
        EventType = eventType;
        KeyName = keyName;
        Modifiers = modifiers ?? string.Empty;
    }

    public DateTime Timestamp { get; }

    public KeyEventType EventType { get; }

    public string KeyName { get; }

    /// <summary>Held modifier keys such as "Ctrl+Shift"; empty when none.</summary>
    public string Modifiers { get; }

    /// <summary>Formats the entry as a single text line, e.g. "2026-11-25 14:03:12.345 | KeyDown | A | Shift".</summary>
    public string ToLogLine()
    {
        var modifiers = Modifiers.Length == 0 ? NoModifiersPlaceholder : Modifiers;

        return string.Join(
            FieldSeparator,
            Timestamp.ToString(TimestampFormat, CultureInfo.InvariantCulture),
            EventType,
            KeyName,
            modifiers);
    }
}
