namespace KeyEventLogger.Core.Services;

/// <summary>
/// Raised when saving the log fails for an environmental reason (missing folder,
/// permissions, locked file). The original exception is kept as <see cref="Exception.InnerException"/>.
/// </summary>
public sealed class LogWriteException : Exception
{
    public LogWriteException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
