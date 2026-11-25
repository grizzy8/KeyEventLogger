using KeyEventLogger.Core.Models;
using KeyEventLogger.Core.Services;

namespace KeyEventLogger.Tests;

public sealed class TextFileLogWriterTests : IDisposable
{
    private static readonly DateTime SampleTime = new(2026, 11, 25, 14, 3, 12, 345);

    private readonly string _directory =
        Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), "KeyEventLoggerTests_" + Guid.NewGuid().ToString("N"))).FullName;

    private readonly TextFileLogWriter _writer = new();

    public void Dispose() => Directory.Delete(_directory, recursive: true);

    private string NewPath(string name = "log.txt") => Path.Combine(_directory, name);

    [Fact]
    public void Write_WritesOneLinePerEntryInOrder()
    {
        var path = NewPath();
        KeyLogEntry[] entries =
        [
            new(SampleTime, KeyEventType.KeyDown, "A", "Shift"),
            new(SampleTime.AddMilliseconds(80), KeyEventType.KeyUp, "A")
        ];

        _writer.Write(path, entries);

        Assert.Equal(
            [
                "2026-11-25 14:03:12.345 | KeyDown | A | Shift",
                "2026-11-25 14:03:12.425 | KeyUp | A | -"
            ],
            File.ReadAllLines(path));
    }

    [Fact]
    public void Write_NoEntries_CreatesEmptyFile()
    {
        var path = NewPath();

        _writer.Write(path, []);

        Assert.True(File.Exists(path));
        Assert.Empty(File.ReadAllText(path));
    }

    [Fact]
    public void Write_ExistingFile_IsOverwritten()
    {
        var path = NewPath();
        File.WriteAllText(path, "old content that must disappear");

        _writer.Write(path, [new KeyLogEntry(SampleTime, KeyEventType.KeyDown, "B")]);

        Assert.Equal(["2026-11-25 14:03:12.345 | KeyDown | B | -"], File.ReadAllLines(path));
    }

    [Fact]
    public void Write_NonAsciiKeyName_IsStoredAsUtf8()
    {
        var path = NewPath();

        _writer.Write(path, [new KeyLogEntry(SampleTime, KeyEventType.KeyDown, "Ş")]);

        Assert.Contains("| Ş |", File.ReadAllText(path, System.Text.Encoding.UTF8));
    }

    [Fact]
    public void Write_MissingDirectory_ThrowsLogWriteExceptionWithInnerCause()
    {
        var path = Path.Combine(_directory, "does-not-exist", "log.txt");

        var ex = Assert.Throws<LogWriteException>(() => _writer.Write(path, []));

        Assert.IsType<DirectoryNotFoundException>(ex.InnerException);
    }

    [Fact]
    public void Write_PathIsADirectory_ThrowsLogWriteException()
    {
        Assert.Throws<LogWriteException>(() => _writer.Write(_directory, []));
    }

    [Fact]
    public void Write_FileLockedByAnotherHandle_ThrowsLogWriteException()
    {
        var path = NewPath();
        using var locked = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);

        Assert.Throws<LogWriteException>(() => _writer.Write(path, []));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Write_BlankPath_ThrowsArgumentException(string? path)
    {
        Assert.ThrowsAny<ArgumentException>(() => _writer.Write(path!, []));
    }

    [Fact]
    public void Write_NullEntries_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _writer.Write(NewPath(), null!));
    }
}
