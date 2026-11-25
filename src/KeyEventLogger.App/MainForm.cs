using KeyEventLogger.Core.Models;
using KeyEventLogger.Core.Services;

namespace KeyEventLogger.App;

public partial class MainForm : Form
{
    private const int WmKeyDown = 0x0100;
    private const int WmSysKeyDown = 0x0104;

    private static readonly Color RecordingColor = Color.FromArgb(200, 30, 30);
    private static readonly Color StoppedColor = Color.FromArgb(90, 90, 90);

    private readonly KeyLogSession _session = new();
    private readonly PressedKeyTracker _pressedKeys = new();
    private readonly ILogWriter _logWriter = new TextFileLogWriter();

    public MainForm()
    {
        InitializeComponent();
        _session.EntryAdded += OnEntryAdded;
        _session.RecordingStateChanged += OnRecordingStateChanged;
        UpdateRecordingUi();
    }

    private void OnRecordingStateChanged(object? sender, EventArgs e)
    {
        UpdateRecordingUi();
        lblMessage.Text = _session.IsRecording
            ? "Kayıt başladı. Yalnızca bu pencere odaktayken basılan tuşlar kaydedilir."
            : "Kayıt durduruldu.";
    }

    // The banner changes both colour and text so the state is clear without relying on colour alone.
    private void UpdateRecordingUi()
    {
        var isRecording = _session.IsRecording;

        statusPanel.BackColor = isRecording ? RecordingColor : StoppedColor;
        lblRecordingStatus.Text = isRecording ? "●  KAYIT AKTİF" : "■  KAYIT DURDURULDU";
        btnStart.Enabled = !isRecording;
        btnStop.Enabled = isRecording;
    }

    private void btnStart_Click(object? sender, EventArgs e) => _session.Start();

    private void btnStop_Click(object? sender, EventArgs e) => _session.Stop();

    // Nothing is written unless the user presses "Kaydet" and picks a file themselves.
    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (_session.Entries.Count == 0)
        {
            MessageBox.Show(
                this, "Kaydedilecek kayıt yok. Önce kaydı başlatıp birkaç tuşa basın.",
                "Kaydet", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dialog = new SaveFileDialog
        {
            Title = "Kayıtları metin dosyasına kaydet",
            Filter = "Metin dosyası (*.txt)|*.txt",
            DefaultExt = "txt",
            AddExtension = true,
            OverwritePrompt = true,
            FileName = $"keylog_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            lblMessage.Text = "Kaydetme iptal edildi.";
            return;
        }

        SaveEntries(dialog.FileName);
    }

    private void SaveEntries(string path)
    {
        try
        {
            _logWriter.Write(path, _session.Entries);
            lblMessage.Text = $"{_session.Entries.Count} kayıt şuraya kaydedildi: {path}";
        }
        catch (LogWriteException ex)
        {
            lblMessage.Text = "Kaydetme başarısız.";
            MessageBox.Show(this, ex.Message, "Kaydetme hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // Only this form's own KeyDown/KeyUp events are used (KeyPreview = true);
    // there is no global hook, so nothing is seen while another window has focus.
    private void MainForm_KeyDown(object? sender, KeyEventArgs e) => HandleKeyEvent(KeyEventType.KeyDown, e);

    private void MainForm_KeyUp(object? sender, KeyEventArgs e) => HandleKeyEvent(KeyEventType.KeyUp, e);

    // WinForms does not raise KeyDown on the form for dialog keys (Tab, Enter on a button,
    // arrows, Esc), although their KeyUp does arrive. ProcessCmdKey sees every key-down of
    // this window before dialog handling, so route it through the same handler; the
    // PressedKeyTracker drops the duplicate when KeyDown fires as well.
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (msg.Msg is WmKeyDown or WmSysKeyDown)
        {
            HandleKeyEvent(KeyEventType.KeyDown, new KeyEventArgs(keyData));
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    // While another window is active our KeyUp events are not delivered, so forget held keys.
    private void MainForm_Deactivate(object? sender, EventArgs e) => _pressedKeys.Reset();

    private void HandleKeyEvent(KeyEventType eventType, KeyEventArgs e)
    {
        var keyName = KeyMapper.GetKeyName(e.KeyCode);

        if (eventType == KeyEventType.KeyDown)
        {
            if (!_pressedKeys.TryPress(keyName))
            {
                return; // auto-repeat of a key that is still held
            }
        }
        else
        {
            _pressedKeys.Release(keyName);
        }

        _session.Record(
            eventType,
            keyName,
            KeyMapper.GetModifiers(e),
            isFormActive: ReferenceEquals(ActiveForm, this),
            isSensitiveInput: KeyInputGuard.IsSensitiveInput(ActiveControl));
    }

    private void OnEntryAdded(object? sender, KeyLogEntry entry)
    {
        var item = new ListViewItem(
            [entry.TimestampText, entry.EventType.ToString(), entry.KeyName, entry.Modifiers]);

        lvEntries.Items.Add(item);
        item.EnsureVisible();
        lblEntryCount.Text = $"{_session.Entries.Count} kayıt";
    }
}
