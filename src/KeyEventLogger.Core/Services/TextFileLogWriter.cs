using System.Security;
using System.Text;
using KeyEventLogger.Core.Models;

namespace KeyEventLogger.Core.Services;

/// <summary>
/// Saves entries as a UTF-8 text file, one <see cref="KeyLogEntry.ToLogLine"/> per line.
/// Writes only the file the caller names; nothing is written automatically.
/// </summary>
public sealed class TextFileLogWriter : ILogWriter
{
    private static readonly UTF8Encoding Utf8WithoutBom = new(encoderShouldEmitUTF8Identifier: false);

    public void Write(string path, IEnumerable<KeyLogEntry> entries)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        ArgumentNullException.ThrowIfNull(entries);

        try
        {
            using var writer = new StreamWriter(path, append: false, Utf8WithoutBom);

            foreach (var entry in entries)
            {
                writer.WriteLine(entry.ToLogLine());
            }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or SecurityException
                                       or NotSupportedException or ArgumentException)
        {
            throw new LogWriteException($"Kayıt dosyası yazılamadı: {ex.Message}", ex);
        }
    }
}
