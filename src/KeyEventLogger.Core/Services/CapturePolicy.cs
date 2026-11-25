namespace KeyEventLogger.Core.Services;

/// <summary>
/// Central rule for when a key event may be recorded. Enforces the assignment's
/// scope limit: only while the user started recording, only while this app's own
/// form is active, and never from sensitive (password) inputs.
/// </summary>
public static class CapturePolicy
{
    public static bool ShouldCapture(bool isRecording, bool isFormActive, bool isSensitiveInput) =>
        isRecording && isFormActive && !isSensitiveInput;
}
