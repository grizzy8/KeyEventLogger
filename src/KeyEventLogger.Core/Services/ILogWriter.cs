using KeyEventLogger.Core.Models;

namespace KeyEventLogger.Core.Services;

/// <summary>Writes log entries to a user-chosen local destination.</summary>
public interface ILogWriter
{
    /// <exception cref="LogWriteException">The destination could not be written.</exception>
    void Write(string path, IEnumerable<KeyLogEntry> entries);
}
