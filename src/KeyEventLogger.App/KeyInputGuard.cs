namespace KeyEventLogger.App;

/// <summary>
/// Detects inputs that must never be logged (password fields). Part of the
/// assignment's scope limit: no password / credential capture.
/// </summary>
public static class KeyInputGuard
{
    /// <param name="activeControl">The control that currently has keyboard focus (may be <c>null</c>).</param>
    public static bool IsSensitiveInput(Control? activeControl)
    {
        var focused = ResolveFocusedControl(activeControl);

        return focused switch
        {
            TextBox textBox => textBox.UseSystemPasswordChar || textBox.PasswordChar != '\0',
            MaskedTextBox maskedTextBox => maskedTextBox.UseSystemPasswordChar || maskedTextBox.PasswordChar != '\0',
            _ => false
        };
    }

    // ActiveControl of a container may itself be a container (e.g. UserControl); walk down to the leaf.
    private static Control? ResolveFocusedControl(Control? control)
    {
        while (control is ContainerControl { ActiveControl: { } inner })
        {
            control = inner;
        }

        return control;
    }
}
