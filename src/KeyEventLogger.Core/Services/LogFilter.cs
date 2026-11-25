using KeyEventLogger.Core.Models;

namespace KeyEventLogger.Core.Services;

public static class LogFilter
{
    /// <summary>Returns the entries whose timestamp lies inside <paramref name="range"/>, in their original order.</summary>
    public static IReadOnlyList<KeyLogEntry> ByTimeRange(IEnumerable<KeyLogEntry> entries, TimeRange range)
    {
        ArgumentNullException.ThrowIfNull(entries);
        ArgumentNullException.ThrowIfNull(range);

        return entries.Where(entry => range.Contains(entry.Timestamp)).ToList();
    }
}
