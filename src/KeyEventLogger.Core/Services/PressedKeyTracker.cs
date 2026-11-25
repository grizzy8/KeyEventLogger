namespace KeyEventLogger.Core.Services;

/// <summary>
/// Remembers which keys are currently held down so that the operating system's
/// auto-repeat (many KeyDown events while a key is held) is logged only once.
/// </summary>
public sealed class PressedKeyTracker
{
    private readonly HashSet<string> _pressed = new(StringComparer.Ordinal);

    public int Count => _pressed.Count;

    /// <returns><c>true</c> if this is a fresh press; <c>false</c> if the key is already held (auto-repeat).</returns>
    public bool TryPress(string keyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(keyName);
        return _pressed.Add(keyName);
    }

    public void Release(string keyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(keyName);
        _pressed.Remove(keyName);
    }

    /// <summary>Forgets all held keys, e.g. when the window loses focus and key-up events may be missed.</summary>
    public void Reset() => _pressed.Clear();
}
