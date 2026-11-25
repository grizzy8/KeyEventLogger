namespace KeyEventLogger.App;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        statusPanel = new Panel();
        lblRecordingStatus = new Label();
        buttonPanel = new FlowLayoutPanel();
        btnStart = new Button();
        btnStop = new Button();
        btnClear = new Button();
        btnSave = new Button();
        inputPanel = new Panel();
        txtTryInput = new TextBox();
        lblInputHint = new Label();
        lvEntries = new ListView();
        colTime = new ColumnHeader();
        colEvent = new ColumnHeader();
        colKey = new ColumnHeader();
        colModifiers = new ColumnHeader();
        sidePanel = new Panel();
        grpStats = new GroupBox();
        lvTopKeys = new ListView();
        colTopKey = new ColumnHeader();
        colTopCount = new ColumnHeader();
        lblTotalKeys = new Label();
        grpFilter = new GroupBox();
        lblFilterFrom = new Label();
        dtpFrom = new DateTimePicker();
        lblFilterTo = new Label();
        dtpTo = new DateTimePicker();
        filterButtonPanel = new FlowLayoutPanel();
        btnApplyFilter = new Button();
        btnResetFilter = new Button();
        statusStrip = new StatusStrip();
        lblEntryCount = new ToolStripStatusLabel();
        lblMessage = new ToolStripStatusLabel();
        statusPanel.SuspendLayout();
        buttonPanel.SuspendLayout();
        inputPanel.SuspendLayout();
        sidePanel.SuspendLayout();
        grpStats.SuspendLayout();
        grpFilter.SuspendLayout();
        filterButtonPanel.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();
        //
        // statusPanel
        //
        statusPanel.BackColor = Color.FromArgb(90, 90, 90);
        statusPanel.Controls.Add(lblRecordingStatus);
        statusPanel.Dock = DockStyle.Top;
        statusPanel.Name = "statusPanel";
        statusPanel.Size = new Size(820, 52);
        statusPanel.TabIndex = 0;
        //
        // lblRecordingStatus
        //
        lblRecordingStatus.Dock = DockStyle.Fill;
        lblRecordingStatus.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblRecordingStatus.ForeColor = Color.White;
        lblRecordingStatus.Name = "lblRecordingStatus";
        lblRecordingStatus.Text = "■  KAYIT DURDURULDU";
        lblRecordingStatus.TextAlign = ContentAlignment.MiddleCenter;
        //
        // buttonPanel
        //
        buttonPanel.Controls.Add(btnStart);
        buttonPanel.Controls.Add(btnStop);
        buttonPanel.Controls.Add(btnClear);
        buttonPanel.Controls.Add(btnSave);
        buttonPanel.Dock = DockStyle.Top;
        buttonPanel.Name = "buttonPanel";
        buttonPanel.Padding = new Padding(8, 8, 8, 4);
        buttonPanel.Size = new Size(820, 52);
        buttonPanel.TabIndex = 1;
        //
        // btnStart
        //
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(110, 34);
        btnStart.TabIndex = 0;
        btnStart.Text = "Başlat";
        btnStart.UseVisualStyleBackColor = true;
        btnStart.Click += btnStart_Click;
        //
        // btnStop
        //
        btnStop.Enabled = false;
        btnStop.Name = "btnStop";
        btnStop.Size = new Size(110, 34);
        btnStop.TabIndex = 1;
        btnStop.Text = "Durdur";
        btnStop.UseVisualStyleBackColor = true;
        btnStop.Click += btnStop_Click;
        //
        // btnClear
        //
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(110, 34);
        btnClear.TabIndex = 2;
        btnClear.Text = "Temizle";
        btnClear.UseVisualStyleBackColor = true;
        btnClear.Click += btnClear_Click;
        //
        // btnSave
        //
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(130, 34);
        btnSave.TabIndex = 3;
        btnSave.Text = "Kaydet...";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += btnSave_Click;
        //
        // inputPanel
        //
        inputPanel.Controls.Add(txtTryInput);
        inputPanel.Controls.Add(lblInputHint);
        inputPanel.Dock = DockStyle.Top;
        inputPanel.Name = "inputPanel";
        inputPanel.Padding = new Padding(12, 6, 12, 10);
        inputPanel.Size = new Size(820, 74);
        inputPanel.TabIndex = 2;
        //
        // lblInputHint
        //
        lblInputHint.AutoSize = false;
        lblInputHint.Dock = DockStyle.Top;
        lblInputHint.Name = "lblInputHint";
        lblInputHint.Size = new Size(796, 24);
        lblInputHint.Text = "Deneme alanı — buraya yazın (gerçek parola veya kişisel bilgi yazmayın):";
        lblInputHint.TextAlign = ContentAlignment.MiddleLeft;
        //
        // txtTryInput
        //
        txtTryInput.Dock = DockStyle.Bottom;
        txtTryInput.Name = "txtTryInput";
        txtTryInput.TabIndex = 0;
        //
        // lvEntries
        //
        lvEntries.Columns.AddRange(new ColumnHeader[] { colTime, colEvent, colKey, colModifiers });
        lvEntries.Dock = DockStyle.Fill;
        lvEntries.FullRowSelect = true;
        lvEntries.GridLines = true;
        lvEntries.HideSelection = false;
        lvEntries.Name = "lvEntries";
        lvEntries.TabIndex = 3;
        lvEntries.UseCompatibleStateImageBehavior = false;
        lvEntries.View = View.Details;
        //
        // colTime
        //
        colTime.Text = "Zaman";
        colTime.Width = 185;
        //
        // colEvent
        //
        colEvent.Text = "Olay";
        colEvent.Width = 85;
        //
        // colKey
        //
        colKey.Text = "Tuş";
        colKey.Width = 110;
        //
        // colModifiers
        //
        colModifiers.Text = "Değiştirici";
        colModifiers.Width = 100;
        //
        // sidePanel
        //
        sidePanel.AutoScroll = true;
        sidePanel.Controls.Add(grpFilter);
        sidePanel.Controls.Add(grpStats);
        sidePanel.Dock = DockStyle.Right;
        sidePanel.Name = "sidePanel";
        sidePanel.Padding = new Padding(8, 8, 12, 8);
        sidePanel.Size = new Size(250, 300);
        sidePanel.TabIndex = 5;
        //
        // grpStats
        //
        grpStats.Controls.Add(lvTopKeys);
        grpStats.Controls.Add(lblTotalKeys);
        grpStats.Dock = DockStyle.Top;
        grpStats.Name = "grpStats";
        grpStats.Padding = new Padding(8, 4, 8, 8);
        grpStats.Size = new Size(230, 180);
        grpStats.TabIndex = 0;
        grpStats.TabStop = false;
        grpStats.Text = "İstatistik";
        //
        // lblTotalKeys
        //
        lblTotalKeys.AutoSize = false;
        lblTotalKeys.Dock = DockStyle.Top;
        lblTotalKeys.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTotalKeys.Name = "lblTotalKeys";
        lblTotalKeys.Size = new Size(214, 30);
        lblTotalKeys.Text = "Toplam tuş: 0";
        lblTotalKeys.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lvTopKeys
        //
        lvTopKeys.Columns.AddRange(new ColumnHeader[] { colTopKey, colTopCount });
        lvTopKeys.Dock = DockStyle.Fill;
        lvTopKeys.FullRowSelect = true;
        lvTopKeys.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        lvTopKeys.Name = "lvTopKeys";
        lvTopKeys.TabIndex = 1;
        lvTopKeys.UseCompatibleStateImageBehavior = false;
        lvTopKeys.View = View.Details;
        //
        // colTopKey
        //
        colTopKey.Text = "En sık 5 tuş";
        colTopKey.Width = 130;
        //
        // colTopCount
        //
        colTopCount.Text = "Adet";
        colTopCount.Width = 60;
        //
        // grpFilter
        //
        grpFilter.Controls.Add(filterButtonPanel);
        grpFilter.Controls.Add(dtpTo);
        grpFilter.Controls.Add(lblFilterTo);
        grpFilter.Controls.Add(dtpFrom);
        grpFilter.Controls.Add(lblFilterFrom);
        grpFilter.Dock = DockStyle.Top;
        grpFilter.Name = "grpFilter";
        grpFilter.Padding = new Padding(8, 4, 8, 8);
        grpFilter.Size = new Size(230, 185);
        grpFilter.TabIndex = 1;
        grpFilter.TabStop = false;
        grpFilter.Text = "Tarih/saat filtresi";
        //
        // lblFilterFrom
        //
        lblFilterFrom.AutoSize = false;
        lblFilterFrom.Dock = DockStyle.Top;
        lblFilterFrom.Name = "lblFilterFrom";
        lblFilterFrom.Size = new Size(214, 24);
        lblFilterFrom.Text = "Başlangıç:";
        lblFilterFrom.TextAlign = ContentAlignment.BottomLeft;
        //
        // dtpFrom
        //
        dtpFrom.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        dtpFrom.Dock = DockStyle.Top;
        dtpFrom.Format = DateTimePickerFormat.Custom;
        dtpFrom.Name = "dtpFrom";
        dtpFrom.ShowCheckBox = true;
        dtpFrom.Checked = false;
        dtpFrom.TabIndex = 1;
        dtpFrom.ValueChanged += dtpFrom_ValueChanged;
        //
        // lblFilterTo
        //
        lblFilterTo.AutoSize = false;
        lblFilterTo.Dock = DockStyle.Top;
        lblFilterTo.Name = "lblFilterTo";
        lblFilterTo.Size = new Size(214, 24);
        lblFilterTo.Text = "Bitiş:";
        lblFilterTo.TextAlign = ContentAlignment.BottomLeft;
        //
        // dtpTo
        //
        dtpTo.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        dtpTo.Dock = DockStyle.Top;
        dtpTo.Format = DateTimePickerFormat.Custom;
        dtpTo.Name = "dtpTo";
        dtpTo.ShowCheckBox = true;
        dtpTo.Checked = false;
        dtpTo.TabIndex = 3;
        dtpTo.ValueChanged += dtpTo_ValueChanged;
        //
        // filterButtonPanel
        //
        filterButtonPanel.Controls.Add(btnApplyFilter);
        filterButtonPanel.Controls.Add(btnResetFilter);
        filterButtonPanel.Dock = DockStyle.Top;
        filterButtonPanel.Name = "filterButtonPanel";
        filterButtonPanel.Padding = new Padding(0, 8, 0, 0);
        filterButtonPanel.Size = new Size(214, 48);
        filterButtonPanel.TabIndex = 4;
        //
        // btnApplyFilter
        //
        btnApplyFilter.Name = "btnApplyFilter";
        btnApplyFilter.Size = new Size(98, 32);
        btnApplyFilter.TabIndex = 0;
        btnApplyFilter.Text = "Uygula";
        btnApplyFilter.UseVisualStyleBackColor = true;
        btnApplyFilter.Click += btnApplyFilter_Click;
        //
        // btnResetFilter
        //
        btnResetFilter.Name = "btnResetFilter";
        btnResetFilter.Size = new Size(98, 32);
        btnResetFilter.TabIndex = 1;
        btnResetFilter.Text = "Sıfırla";
        btnResetFilter.UseVisualStyleBackColor = true;
        btnResetFilter.Click += btnResetFilter_Click;
        //
        // statusStrip
        //
        statusStrip.Items.AddRange(new ToolStripItem[] { lblEntryCount, lblMessage });
        statusStrip.Name = "statusStrip";
        statusStrip.TabIndex = 4;
        //
        // lblEntryCount
        //
        lblEntryCount.Name = "lblEntryCount";
        lblEntryCount.Text = "0 kayıt";
        //
        // lblMessage
        //
        lblMessage.Name = "lblMessage";
        lblMessage.Text = "Hazır. Kaydı başlatmak için \"Başlat\" düğmesine basın.";
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(860, 600);
        Controls.Add(lvEntries);
        Controls.Add(sidePanel);
        Controls.Add(statusStrip);
        Controls.Add(inputPanel);
        Controls.Add(buttonPanel);
        Controls.Add(statusPanel);
        KeyPreview = true;
        MinimumSize = new Size(720, 560);
        Name = "MainForm";
        ShowInTaskbar = true;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Key Event Logger (Eğitsel)";
        Deactivate += MainForm_Deactivate;
        KeyDown += MainForm_KeyDown;
        KeyUp += MainForm_KeyUp;
        statusPanel.ResumeLayout(false);
        buttonPanel.ResumeLayout(false);
        inputPanel.ResumeLayout(false);
        inputPanel.PerformLayout();
        sidePanel.ResumeLayout(false);
        grpStats.ResumeLayout(false);
        grpFilter.ResumeLayout(false);
        filterButtonPanel.ResumeLayout(false);
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Panel statusPanel;
    private Label lblRecordingStatus;
    private FlowLayoutPanel buttonPanel;
    private Button btnStart;
    private Button btnStop;
    private Button btnClear;
    private Button btnSave;
    private Panel inputPanel;
    private TextBox txtTryInput;
    private Label lblInputHint;
    private ListView lvEntries;
    private ColumnHeader colTime;
    private ColumnHeader colEvent;
    private ColumnHeader colKey;
    private ColumnHeader colModifiers;
    private Panel sidePanel;
    private GroupBox grpStats;
    private Label lblTotalKeys;
    private ListView lvTopKeys;
    private ColumnHeader colTopKey;
    private ColumnHeader colTopCount;
    private GroupBox grpFilter;
    private Label lblFilterFrom;
    private DateTimePicker dtpFrom;
    private Label lblFilterTo;
    private DateTimePicker dtpTo;
    private FlowLayoutPanel filterButtonPanel;
    private Button btnApplyFilter;
    private Button btnResetFilter;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel lblEntryCount;
    private ToolStripStatusLabel lblMessage;
}
