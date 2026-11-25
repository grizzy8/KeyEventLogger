using KeyEventLogger.Core.Models;
using KeyEventLogger.Core.Services;

namespace KeyEventLogger.App;

public partial class MainForm : Form
{
    private const int WmKeyDown = 0x0100;
    private const int WmSysKeyDown = 0x0104;
    private const int TopKeysShown = 5;

    private static readonly Color RecordingColor = Color.FromArgb(200, 30, 30);
    private static readonly Color StoppedColor = Color.FromArgb(90, 90, 90);

    private readonly KeyLogSession _session = new();
    private readonly PressedKeyTracker _pressedKeys = new();
    private readonly ILogWriter _logWriter = new TextFileLogWriter();

    public MainForm()
    {
        InitializeComponent();
        // Reuse the icon embedded in the exe (ApplicationIcon) for the window and taskbar.
        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath) ?? Icon;
        ScaleColumnWidths(lvEntries, lvTopKeys);
        _session.EntryAdded += OnEntryAdded;
        _session.RecordingStateChanged += OnRecordingStateChanged;
        _session.Cleared += OnCleared;
        UpdateRecordingUi();
    }

    // WinForms does not scale ListView column widths with the display DPI, so headers and
    // timestamps would be cut off on high-DPI screens.
    private void ScaleColumnWidths(params ListView[] lists)
    {
        foreach (var column in lists.SelectMany(list => list.Columns.Cast<ColumnHeader>()))
        {
            column.Width = LogicalToDeviceUnits(column.Width);
        }
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

    private void btnClear_Click(object? sender, EventArgs e)
    {
        if (_session.Entries.Count == 0)
        {
            lblMessage.Text = "Temizlenecek kayıt yok.";
            return;
        }

        var scope = _range.IsActive ? "Filtre aktif olsa bile tüm " : string.Empty;
        var answer = MessageBox.Show(
            this,
            $"{scope}{_session.Entries.Count} kayıt silinecek. Kaydetmediyseniz geri alınamaz.\n\nDevam edilsin mi?",
            "Kayıtları temizle",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2);

        if (answer == DialogResult.Yes)
        {
            _session.Clear();
        }
    }

    private void OnCleared(object? sender, EventArgs e)
    {
        RebuildEntryList();
        lblMessage.Text = "Kayıtlar temizlendi.";
    }

    // The statistics describe what is visible, i.e. the entries that pass the active filter.
    private void RefreshStatistics()
    {
        grpStats.Text = _range.IsActive ? "İstatistik (filtreli)" : "İstatistik";
        lblTotalKeys.Text = $"Toplam tuş: {KeyStatistics.TotalKeyPresses(_visibleEntries)}";

        lvTopKeys.BeginUpdate();
        lvTopKeys.Items.Clear();

        foreach (var keyCount in KeyStatistics.TopKeys(_visibleEntries, TopKeysShown))
        {
            lvTopKeys.Items.Add(new ListViewItem([keyCount.KeyName, keyCount.Count.ToString()]));
        }

        lvTopKeys.EndUpdate();
    }

    private void UpdateEntryCount() => lblEntryCount.Text = _range.IsActive
        ? $"{_visibleEntries.Count} / {_session.Entries.Count} kayıt (filtreli)"
        : $"{_session.Entries.Count} kayıt";

    // Nothing is written unless the user presses "Kaydet" and picks a file themselves.
    // With an active filter only the visible entries are saved.
    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (_visibleEntries.Count == 0)
        {
            var reason = _session.Entries.Count == 0
                ? "Kaydedilecek kayıt yok. Önce kaydı başlatıp birkaç tuşa basın."
                : "Filtreye uyan kayıt yok. Filtreyi değiştirin veya sıfırlayın.";

            MessageBox.Show(this, reason, "Kaydet", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            _logWriter.Write(path, _visibleEntries);
            var filtered = _range.IsActive ? " (filtreli)" : string.Empty;
            lblMessage.Text = $"{_visibleEntries.Count} kayıt{filtered} şuraya kaydedildi: {path}";
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
        else if (!_pressedKeys.Release(keyName))
        {
            return; // key-up of a key whose key-down we never saw (e.g. pressed inside a dialog)
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
        if (!_range.Contains(entry.Timestamp))
        {
            UpdateEntryCount(); // recorded, but hidden by the active filter
            return;
        }

        _visibleEntries.Add(entry);

        var item = CreateRow(entry);
        lvEntries.Items.Add(item);
        item.EnsureVisible();
        UpdateEntryCount();

        if (entry.EventType == KeyEventType.KeyDown)
        {
            RefreshStatistics(); // only key presses change the statistics
        }
    }

    private static ListViewItem CreateRow(KeyLogEntry entry) =>
        new([entry.TimestampText, entry.EventType.ToString(), entry.KeyName, entry.Modifiers]);
}
