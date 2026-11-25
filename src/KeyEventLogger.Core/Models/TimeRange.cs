namespace KeyEventLogger.Core.Models;

/// <summary>
/// An optional, inclusive time window. Either bound may be missing (open-ended);
/// with no bounds it matches everything.
/// </summary>
public sealed record TimeRange
{
    public TimeRange(DateTime? from, DateTime? to)
    {
        if (from.HasValue && to.HasValue && from.Value > to.Value)
        {
            throw new ArgumentException("The start of the range must not be after its end.");
        }

        From = from;
        To = to;
    }

    /// <summary>A range without bounds: no filtering.</summary>
    public static TimeRange None { get; } = new(null, null);

    public DateTime? From { get; }

    public DateTime? To { get; }

    public bool IsActive => From.HasValue || To.HasValue;

    public bool Contains(DateTime timestamp) =>
        (From is null || timestamp >= From) && (To is null || timestamp <= To);

    /// <summary>
    /// Builds a range from values chosen with second precision (e.g. a DateTimePicker):
    /// the start is cut to the beginning of its second and the end is extended to the last
    /// tick of its second, so an end of 14:03:12 also includes 14:03:12.345.
    /// </summary>
    /// <exception cref="ArgumentException">The start second is after the end second.</exception>
    public static TimeRange FromWholeSeconds(DateTime? from, DateTime? to) =>
        new(from is { } start ? StartOfSecond(start) : null,
            to is { } end ? StartOfSecond(end).AddSeconds(1).AddTicks(-1) : null);

    private static DateTime StartOfSecond(DateTime value) =>
        new(value.Ticks - value.Ticks % TimeSpan.TicksPerSecond, value.Kind);
}
