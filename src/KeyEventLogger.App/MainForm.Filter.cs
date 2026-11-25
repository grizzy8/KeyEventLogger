using KeyEventLogger.Core.Models;
using KeyEventLogger.Core.Services;

namespace KeyEventLogger.App;

// Date/time range filter: the part of MainForm that decides which recorded entries are visible.
public partial class MainForm
{
    private readonly List<KeyLogEntry> _visibleEntries = [];
    private TimeRange _range = TimeRange.None;
    private bool _fromWasChecked;
    private bool _toWasChecked;

    // When a picker is ticked, start from the span of the recorded entries (first / last entry)
    // instead of the app start time, so ticking a box does not hide everything by surprise.
    private void dtpFrom_ValueChanged(object? sender, EventArgs e) =>
        SeedWhenJustChecked(dtpFrom, ref _fromWasChecked, _session.Entries.FirstOrDefault()?.Timestamp);

    private void dtpTo_ValueChanged(object? sender, EventArgs e) =>
        SeedWhenJustChecked(dtpTo, ref _toWasChecked, _session.Entries.LastOrDefault()?.Timestamp);

    private static void SeedWhenJustChecked(DateTimePicker picker, ref bool wasChecked, DateTime? suggested)
    {
        var justChecked = picker.Checked && !wasChecked;
        wasChecked = picker.Checked;

        if (justChecked)
        {
            picker.Value = suggested ?? DateTime.Now;
        }
    }

    // An unchecked picker means "no bound", so a filter can be open-ended on either side.
    private void btnApplyFilter_Click(object? sender, EventArgs e)
    {
        try
        {
            _range = TimeRange.FromWholeSeconds(
                dtpFrom.Checked ? dtpFrom.Value : null,
                dtpTo.Checked ? dtpTo.Value : null);
        }
        catch (ArgumentException)
        {
            MessageBox.Show(
                this, "Başlangıç zamanı bitiş zamanından sonra olamaz.",
                "Geçersiz aralık", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        RebuildEntryList();
        lblMessage.Text = _range.IsActive
            ? $"Filtre uygulandı: {_visibleEntries.Count} / {_session.Entries.Count} kayıt gösteriliyor."
            : "Filtre için en az bir tarih/saat kutusunu işaretleyin.";
    }

    private void btnResetFilter_Click(object? sender, EventArgs e)
    {
        dtpFrom.Checked = false;
        dtpTo.Checked = false;
        _range = TimeRange.None;

        RebuildEntryList();
        lblMessage.Text = "Filtre sıfırlandı.";
    }

    // Recomputes everything that depends on the visible set: list, counter and statistics.
    private void RebuildEntryList()
    {
        _visibleEntries.Clear();
        _visibleEntries.AddRange(LogFilter.ByTimeRange(_session.Entries, _range));

        lvEntries.BeginUpdate();
        lvEntries.Items.Clear();
        lvEntries.Items.AddRange(_visibleEntries.Select(CreateRow).ToArray());
        lvEntries.EndUpdate();

        if (lvEntries.Items.Count > 0)
        {
            lvEntries.Items[^1].EnsureVisible();
        }

        UpdateEntryCount();
        RefreshStatistics();
    }
}
