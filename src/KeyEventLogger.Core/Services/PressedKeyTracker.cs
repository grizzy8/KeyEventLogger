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

    /// <returns>
    /// <c>true</c> if the key was held (its press was seen); <c>false</c> for a key-up whose
    /// key-down never reached us, e.g. a key pressed in a dialog or another window.
    /// </returns>
    public bool Release(string keyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(keyName);
        return _pressed.Remove(keyName);
    }

    /// <summary>Forgets all held keys, e.g. when the window loses focus and key-up events may be missed.</summary>
    public void Reset() => _pressed.Clear();
}
