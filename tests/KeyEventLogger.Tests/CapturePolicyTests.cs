using KeyEventLogger.Core.Services;

namespace KeyEventLogger.Tests;

public class CapturePolicyTests
{
    [Fact]
    public void ShouldCapture_RecordingActiveFormAndPlainInput_ReturnsTrue()
    {
        Assert.True(CapturePolicy.ShouldCapture(isRecording: true, isFormActive: true, isSensitiveInput: false));
    }

    [Fact]
    public void ShouldCapture_NotRecording_ReturnsFalse()
    {
        Assert.False(CapturePolicy.ShouldCapture(isRecording: false, isFormActive: true, isSensitiveInput: false));
    }

    [Fact]
    public void ShouldCapture_FormNotActive_ReturnsFalse()
    {
        Assert.False(CapturePolicy.ShouldCapture(isRecording: true, isFormActive: false, isSensitiveInput: false));
    }

    [Fact]
    public void ShouldCapture_SensitiveInput_ReturnsFalse()
    {
        Assert.False(CapturePolicy.ShouldCapture(isRecording: true, isFormActive: true, isSensitiveInput: true));
    }
}
