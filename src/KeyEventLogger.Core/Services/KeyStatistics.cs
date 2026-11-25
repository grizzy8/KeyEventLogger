using KeyEventLogger.Core.Models;

namespace KeyEventLogger.Core.Services;

/// <summary>Usage statistics over a set of log entries. A "press" is one KeyDown event.</summary>
public static class KeyStatistics
{
    public static int TotalKeyPresses(IEnumerable<KeyLogEntry> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);

        return entries.Count(entry => entry.EventType == KeyEventType.KeyDown);
    }

    /// <summary>
    /// The most frequently pressed keys, highest count first; ties are ordered by key name
    /// so the result is stable.
    /// </summary>
    public static IReadOnlyList<KeyCount> TopKeys(IEnumerable<KeyLogEntry> entries, int count)
    {
        ArgumentNullException.ThrowIfNull(entries);
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        return entries
            .Where(entry => entry.EventType == KeyEventType.KeyDown)
            .GroupBy(entry => entry.KeyName, StringComparer.Ordinal)
            .Select(group => new KeyCount(group.Key, group.Count()))
            .OrderByDescending(keyCount => keyCount.Count)
            .ThenBy(keyCount => keyCount.KeyName, StringComparer.Ordinal)
            .Take(count)
            .ToList();
    }
}
