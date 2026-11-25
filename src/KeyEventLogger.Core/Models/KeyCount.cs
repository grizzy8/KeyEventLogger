namespace KeyEventLogger.Core.Models;

/// <summary>How many times a key was pressed.</summary>
public sealed record KeyCount(string KeyName, int Count);
