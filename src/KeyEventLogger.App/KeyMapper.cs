namespace KeyEventLogger.App;

/// <summary>Converts WinForms key data into the plain-text names stored in the log.</summary>
internal static class KeyMapper
{
    public static string GetKeyName(Keys keyCode) => keyCode switch
    {
        Keys.Return => "Enter",
        Keys.Back => "Backspace",
        Keys.Capital => "CapsLock",
        Keys.Prior => "PageUp",
        Keys.Next => "PageDown",
        >= Keys.D0 and <= Keys.D9 => ((char)keyCode).ToString(),
        _ => keyCode.ToString()
    };

    /// <summary>Returns the held modifiers joined with "+", e.g. "Ctrl+Shift"; empty when none.</summary>
    public static string GetModifiers(KeyEventArgs e)
    {
        var parts = new List<string>(3);

        if (e.Control)
        {
            parts.Add("Ctrl");
        }

        if (e.Alt)
        {
            parts.Add("Alt");
        }

        if (e.Shift)
        {
            parts.Add("Shift");
        }

        return string.Join('+', parts);
    }
}
