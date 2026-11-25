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
        statusStrip = new StatusStrip();
        lblEntryCount = new ToolStripStatusLabel();
        lblMessage = new ToolStripStatusLabel();
        statusPanel.SuspendLayout();
        buttonPanel.SuspendLayout();
        inputPanel.SuspendLayout();
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
        //
        // btnSave
        //
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(130, 34);
        btnSave.TabIndex = 3;
        btnSave.Text = "Kaydet...";
        btnSave.UseVisualStyleBackColor = true;
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
        colTime.Width = 200;
        //
        // colEvent
        //
        colEvent.Text = "Olay";
        colEvent.Width = 100;
        //
        // colKey
        //
        colKey.Text = "Tuş";
        colKey.Width = 160;
        //
        // colModifiers
        //
        colModifiers.Text = "Değiştirici";
        colModifiers.Width = 160;
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
        ClientSize = new Size(820, 560);
        Controls.Add(lvEntries);
        Controls.Add(statusStrip);
        Controls.Add(inputPanel);
        Controls.Add(buttonPanel);
        Controls.Add(statusPanel);
        KeyPreview = true;
        MinimumSize = new Size(640, 440);
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
    private StatusStrip statusStrip;
    private ToolStripStatusLabel lblEntryCount;
    private ToolStripStatusLabel lblMessage;
}
