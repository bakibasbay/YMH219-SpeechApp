namespace SpeechApp
{
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.statusIndicator = new SpeechApp.Controls.StatusIndicator();
            this.btnNavStt = new SpeechApp.Controls.ModernButton();
            this.btnNavTts = new SpeechApp.Controls.ModernButton();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblEnvInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlTts = new System.Windows.Forms.Panel();
            this.tlpTts = new System.Windows.Forms.TableLayoutPanel();
            this.cardTtsText = new SpeechApp.Controls.CardPanel();
            this.pnlTtsEditor = new System.Windows.Forms.Panel();
            this.rtbTtsText = new System.Windows.Forms.RichTextBox();
            this.flpTtsButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSpeak = new SpeechApp.Controls.ModernButton();
            this.btnPauseResume = new SpeechApp.Controls.ModernButton();
            this.btnStop = new SpeechApp.Controls.ModernButton();
            this.btnTtsClear = new SpeechApp.Controls.ModernButton();
            this.btnOpenText = new SpeechApp.Controls.ModernButton();
            this.btnSaveText = new SpeechApp.Controls.ModernButton();
            this.lblTtsTitle = new System.Windows.Forms.Label();
            this.cardTtsSettings = new SpeechApp.Controls.CardPanel();
            this.lblSettingsNote = new System.Windows.Forms.Label();
            this.lblReadingDetail = new System.Windows.Forms.Label();
            this.progressReading = new System.Windows.Forms.ProgressBar();
            this.lblCurrentWord = new System.Windows.Forms.Label();
            this.lblReadingTitle = new System.Windows.Forms.Label();
            this.pnlSeparator = new System.Windows.Forms.Panel();
            this.tbVolume = new System.Windows.Forms.TrackBar();
            this.lblVolumeValue = new System.Windows.Forms.Label();
            this.lblVolume = new System.Windows.Forms.Label();
            this.tbRate = new System.Windows.Forms.TrackBar();
            this.lblRateValue = new System.Windows.Forms.Label();
            this.lblRate = new System.Windows.Forms.Label();
            this.lblVoiceHint = new System.Windows.Forms.Label();
            this.cmbVoice = new System.Windows.Forms.ComboBox();
            this.lblVoice = new System.Windows.Forms.Label();
            this.lblSettingsTitle = new System.Windows.Forms.Label();
            this.pnlStt = new System.Windows.Forms.Panel();
            this.tlpStt = new System.Windows.Forms.TableLayoutPanel();
            this.cardSttToolbar = new SpeechApp.Controls.CardPanel();
            this.flpSttToolbar = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.btnListenStart = new SpeechApp.Controls.ModernButton();
            this.btnListenStop = new SpeechApp.Controls.ModernButton();
            this.btnSttClear = new SpeechApp.Controls.ModernButton();
            this.btnSaveResults = new SpeechApp.Controls.ModernButton();
            this.cardSttText = new SpeechApp.Controls.CardPanel();
            this.pnlSttEditor = new System.Windows.Forms.Panel();
            this.rtbSttText = new System.Windows.Forms.RichTextBox();
            this.lblHypothesis = new System.Windows.Forms.Label();
            this.lblSttTextTitle = new System.Windows.Forms.Label();
            this.cardSttList = new SpeechApp.Controls.CardPanel();
            this.lvResults = new System.Windows.Forms.ListView();
            this.colTime = new System.Windows.Forms.ColumnHeader();
            this.colConfidence = new System.Windows.Forms.ColumnHeader();
            this.colText = new System.Windows.Forms.ColumnHeader();
            this.lblSttListTitle = new System.Windows.Forms.Label();
            this.pnlSttWarningHost = new System.Windows.Forms.Panel();
            this.cardSttWarning = new SpeechApp.Controls.CardPanel();
            this.lblSttWarning = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlTts.SuspendLayout();
            this.tlpTts.SuspendLayout();
            this.cardTtsText.SuspendLayout();
            this.pnlTtsEditor.SuspendLayout();
            this.flpTtsButtons.SuspendLayout();
            this.cardTtsSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRate)).BeginInit();
            this.pnlStt.SuspendLayout();
            this.tlpStt.SuspendLayout();
            this.cardSttToolbar.SuspendLayout();
            this.flpSttToolbar.SuspendLayout();
            this.cardSttText.SuspendLayout();
            this.pnlSttEditor.SuspendLayout();
            this.cardSttList.SuspendLayout();
            this.pnlSttWarningHost.SuspendLayout();
            this.cardSttWarning.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.pnlHeader.Controls.Add(this.statusIndicator);
            this.pnlHeader.Controls.Add(this.btnNavStt);
            this.pnlHeader.Controls.Add(this.btnNavTts);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1180, 72);
            this.pnlHeader.TabIndex = 0;
            //
            // statusIndicator
            //
            this.statusIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statusIndicator.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            this.statusIndicator.Location = new System.Drawing.Point(950, 17);
            this.statusIndicator.Name = "statusIndicator";
            this.statusIndicator.Size = new System.Drawing.Size(206, 38);
            this.statusIndicator.TabIndex = 4;
            //
            // btnNavStt
            //
            this.btnNavStt.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnNavStt.Location = new System.Drawing.Point(476, 18);
            this.btnNavStt.Name = "btnNavStt";
            this.btnNavStt.Size = new System.Drawing.Size(168, 36);
            this.btnNavStt.TabIndex = 3;
            this.btnNavStt.Text = "Speech-to-Text";
            this.btnNavStt.Variant = SpeechApp.Controls.ButtonVariant.Ghost;
            this.btnNavStt.Click += new System.EventHandler(this.BtnNavStt_Click);
            //
            // btnNavTts
            //
            this.btnNavTts.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnNavTts.Location = new System.Drawing.Point(300, 18);
            this.btnNavTts.Name = "btnNavTts";
            this.btnNavTts.Size = new System.Drawing.Size(168, 36);
            this.btnNavTts.TabIndex = 2;
            this.btnNavTts.Text = "Text-to-Speech";
            this.btnNavTts.Variant = SpeechApp.Controls.ButtonVariant.Primary;
            this.btnNavTts.Click += new System.EventHandler(this.BtnNavTts_Click);
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblSubtitle.Location = new System.Drawing.Point(26, 42);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(185, 15);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Metin okuma ve konuşma tanıma";
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(22, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(122, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SpeechApp";
            //
            // statusStrip
            //
            this.statusStrip.BackColor = System.Drawing.Color.White;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusMessage,
            this.lblEnvInfo});
            this.statusStrip.Location = new System.Drawing.Point(0, 716);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.statusStrip.Size = new System.Drawing.Size(1180, 24);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 2;
            //
            // lblStatusMessage
            //
            this.lblStatusMessage.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(1047, 19);
            this.lblStatusMessage.Spring = true;
            this.lblStatusMessage.Text = "Hazır";
            this.lblStatusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblEnvInfo
            //
            this.lblEnvInfo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblEnvInfo.Name = "lblEnvInfo";
            this.lblEnvInfo.Size = new System.Drawing.Size(105, 19);
            this.lblEnvInfo.Text = "0 ses · 0 tanıyıcı";
            //
            // pnlContent
            //
            this.pnlContent.Controls.Add(this.pnlTts);
            this.pnlContent.Controls.Add(this.pnlStt);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 72);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(20);
            this.pnlContent.Size = new System.Drawing.Size(1180, 644);
            this.pnlContent.TabIndex = 1;
            //
            // pnlTts
            //
            this.pnlTts.Controls.Add(this.tlpTts);
            this.pnlTts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTts.Location = new System.Drawing.Point(20, 20);
            this.pnlTts.Name = "pnlTts";
            this.pnlTts.Size = new System.Drawing.Size(1140, 604);
            this.pnlTts.TabIndex = 0;
            //
            // tlpTts
            //
            this.tlpTts.ColumnCount = 2;
            this.tlpTts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 330F));
            this.tlpTts.Controls.Add(this.cardTtsText, 0, 0);
            this.tlpTts.Controls.Add(this.cardTtsSettings, 1, 0);
            this.tlpTts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTts.Location = new System.Drawing.Point(0, 0);
            this.tlpTts.Name = "tlpTts";
            this.tlpTts.RowCount = 1;
            this.tlpTts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTts.Size = new System.Drawing.Size(1140, 604);
            this.tlpTts.TabIndex = 0;
            //
            // cardTtsText
            //
            this.cardTtsText.BackColor = System.Drawing.Color.White;
            this.cardTtsText.Controls.Add(this.pnlTtsEditor);
            this.cardTtsText.Controls.Add(this.flpTtsButtons);
            this.cardTtsText.Controls.Add(this.lblTtsTitle);
            this.cardTtsText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTtsText.Location = new System.Drawing.Point(0, 0);
            this.cardTtsText.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.cardTtsText.Name = "cardTtsText";
            this.cardTtsText.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.cardTtsText.Size = new System.Drawing.Size(794, 604);
            this.cardTtsText.TabIndex = 0;
            //
            // pnlTtsEditor
            //
            this.pnlTtsEditor.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.pnlTtsEditor.Controls.Add(this.rtbTtsText);
            this.pnlTtsEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTtsEditor.Location = new System.Drawing.Point(20, 56);
            this.pnlTtsEditor.Name = "pnlTtsEditor";
            this.pnlTtsEditor.Padding = new System.Windows.Forms.Padding(14, 12, 14, 12);
            this.pnlTtsEditor.Size = new System.Drawing.Size(754, 474);
            this.pnlTtsEditor.TabIndex = 1;
            //
            // rtbTtsText
            //
            this.rtbTtsText.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.rtbTtsText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbTtsText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbTtsText.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.rtbTtsText.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.rtbTtsText.Location = new System.Drawing.Point(14, 12);
            this.rtbTtsText.Name = "rtbTtsText";
            this.rtbTtsText.TextChanged += new System.EventHandler(this.RtbTtsText_TextChanged);
            this.rtbTtsText.Size = new System.Drawing.Size(726, 450);
            this.rtbTtsText.TabIndex = 0;
            this.rtbTtsText.Text = "";
            //
            // flpTtsButtons
            //
            this.flpTtsButtons.BackColor = System.Drawing.Color.White;
            this.flpTtsButtons.Controls.Add(this.btnSpeak);
            this.flpTtsButtons.Controls.Add(this.btnPauseResume);
            this.flpTtsButtons.Controls.Add(this.btnStop);
            this.flpTtsButtons.Controls.Add(this.btnTtsClear);
            this.flpTtsButtons.Controls.Add(this.btnOpenText);
            this.flpTtsButtons.Controls.Add(this.btnSaveText);
            this.flpTtsButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpTtsButtons.Location = new System.Drawing.Point(20, 530);
            this.flpTtsButtons.Name = "flpTtsButtons";
            this.flpTtsButtons.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.flpTtsButtons.Size = new System.Drawing.Size(754, 58);
            this.flpTtsButtons.TabIndex = 2;
            this.flpTtsButtons.WrapContents = false;
            //
            // btnSpeak
            //
            this.btnSpeak.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnSpeak.Location = new System.Drawing.Point(0, 16);
            this.btnSpeak.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnSpeak.Name = "btnSpeak";
            this.btnSpeak.Click += new System.EventHandler(this.BtnSpeak_Click);
            this.btnSpeak.Size = new System.Drawing.Size(104, 40);
            this.btnSpeak.TabIndex = 0;
            this.btnSpeak.Text = "▶  Oku";
            this.btnSpeak.Variant = SpeechApp.Controls.ButtonVariant.Primary;
            //
            // btnPauseResume
            //
            this.btnPauseResume.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnPauseResume.Location = new System.Drawing.Point(112, 16);
            this.btnPauseResume.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnPauseResume.Name = "btnPauseResume";
            this.btnPauseResume.Click += new System.EventHandler(this.BtnPauseResume_Click);
            this.btnPauseResume.Size = new System.Drawing.Size(124, 40);
            this.btnPauseResume.TabIndex = 1;
            this.btnPauseResume.Text = "❚❚  Duraklat";
            //
            // btnStop
            //
            this.btnStop.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnStop.Location = new System.Drawing.Point(244, 16);
            this.btnStop.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnStop.Name = "btnStop";
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            this.btnStop.Size = new System.Drawing.Size(104, 40);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "■  Durdur";
            //
            // btnTtsClear
            //
            this.btnTtsClear.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnTtsClear.Location = new System.Drawing.Point(356, 16);
            this.btnTtsClear.Margin = new System.Windows.Forms.Padding(0, 0, 24, 0);
            this.btnTtsClear.Name = "btnTtsClear";
            this.btnTtsClear.Click += new System.EventHandler(this.BtnTtsClear_Click);
            this.btnTtsClear.Size = new System.Drawing.Size(104, 40);
            this.btnTtsClear.TabIndex = 3;
            this.btnTtsClear.Text = "✕  Temizle";
            //
            // btnOpenText
            //
            this.btnOpenText.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnOpenText.Location = new System.Drawing.Point(484, 16);
            this.btnOpenText.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnOpenText.Name = "btnOpenText";
            this.btnOpenText.Click += new System.EventHandler(this.BtnOpenText_Click);
            this.btnOpenText.Size = new System.Drawing.Size(104, 40);
            this.btnOpenText.TabIndex = 4;
            this.btnOpenText.Text = "Aç (.txt)";
            //
            // btnSaveText
            //
            this.btnSaveText.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnSaveText.Location = new System.Drawing.Point(596, 16);
            this.btnSaveText.Margin = new System.Windows.Forms.Padding(0);
            this.btnSaveText.Name = "btnSaveText";
            this.btnSaveText.Click += new System.EventHandler(this.BtnSaveText_Click);
            this.btnSaveText.Size = new System.Drawing.Size(124, 40);
            this.btnSaveText.TabIndex = 5;
            this.btnSaveText.Text = "Kaydet (.txt)";
            //
            // lblTtsTitle
            //
            this.lblTtsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTtsTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.lblTtsTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblTtsTitle.Location = new System.Drawing.Point(20, 16);
            this.lblTtsTitle.Name = "lblTtsTitle";
            this.lblTtsTitle.Size = new System.Drawing.Size(754, 40);
            this.lblTtsTitle.TabIndex = 0;
            this.lblTtsTitle.Text = "Okunacak Metin";
            //
            // cardTtsSettings
            //
            this.cardTtsSettings.BackColor = System.Drawing.Color.White;
            this.cardTtsSettings.Controls.Add(this.lblSettingsNote);
            this.cardTtsSettings.Controls.Add(this.lblReadingDetail);
            this.cardTtsSettings.Controls.Add(this.progressReading);
            this.cardTtsSettings.Controls.Add(this.lblCurrentWord);
            this.cardTtsSettings.Controls.Add(this.lblReadingTitle);
            this.cardTtsSettings.Controls.Add(this.pnlSeparator);
            this.cardTtsSettings.Controls.Add(this.tbVolume);
            this.cardTtsSettings.Controls.Add(this.lblVolumeValue);
            this.cardTtsSettings.Controls.Add(this.lblVolume);
            this.cardTtsSettings.Controls.Add(this.tbRate);
            this.cardTtsSettings.Controls.Add(this.lblRateValue);
            this.cardTtsSettings.Controls.Add(this.lblRate);
            this.cardTtsSettings.Controls.Add(this.lblVoiceHint);
            this.cardTtsSettings.Controls.Add(this.cmbVoice);
            this.cardTtsSettings.Controls.Add(this.lblVoice);
            this.cardTtsSettings.Controls.Add(this.lblSettingsTitle);
            this.cardTtsSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTtsSettings.Location = new System.Drawing.Point(810, 0);
            this.cardTtsSettings.Margin = new System.Windows.Forms.Padding(0);
            this.cardTtsSettings.Name = "cardTtsSettings";
            this.cardTtsSettings.Size = new System.Drawing.Size(330, 604);
            this.cardTtsSettings.TabIndex = 1;
            //
            // lblSettingsNote
            //
            this.lblSettingsNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSettingsNote.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSettingsNote.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSettingsNote.Location = new System.Drawing.Point(20, 548);
            this.lblSettingsNote.Name = "lblSettingsNote";
            this.lblSettingsNote.Size = new System.Drawing.Size(290, 38);
            this.lblSettingsNote.TabIndex = 15;
            this.lblSettingsNote.Text = "Hız ve ses düzeyi değişiklikleri bir sonraki okumada uygulanır. Okunan kelime metin üzerinde vurgulanır.";
            //
            // lblReadingDetail
            //
            this.lblReadingDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReadingDetail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblReadingDetail.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblReadingDetail.Location = new System.Drawing.Point(20, 418);
            this.lblReadingDetail.Name = "lblReadingDetail";
            this.lblReadingDetail.Size = new System.Drawing.Size(290, 20);
            this.lblReadingDetail.TabIndex = 14;
            this.lblReadingDetail.Text = "Okuma başlamadı";
            //
            // progressReading
            //
            this.progressReading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressReading.Location = new System.Drawing.Point(20, 402);
            this.progressReading.Name = "progressReading";
            this.progressReading.Size = new System.Drawing.Size(290, 10);
            this.progressReading.TabIndex = 13;
            //
            // lblCurrentWord
            //
            this.lblCurrentWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentWord.AutoEllipsis = true;
            this.lblCurrentWord.Font = new System.Drawing.Font("Segoe UI Semibold", 18F);
            this.lblCurrentWord.ForeColor = System.Drawing.Color.FromArgb(79, 70, 229);
            this.lblCurrentWord.Location = new System.Drawing.Point(18, 350);
            this.lblCurrentWord.Name = "lblCurrentWord";
            this.lblCurrentWord.Size = new System.Drawing.Size(292, 44);
            this.lblCurrentWord.TabIndex = 12;
            this.lblCurrentWord.Text = "—";
            this.lblCurrentWord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblReadingTitle
            //
            this.lblReadingTitle.AutoSize = true;
            this.lblReadingTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.lblReadingTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblReadingTitle.Location = new System.Drawing.Point(20, 324);
            this.lblReadingTitle.Name = "lblReadingTitle";
            this.lblReadingTitle.Size = new System.Drawing.Size(103, 19);
            this.lblReadingTitle.TabIndex = 11;
            this.lblReadingTitle.Text = "Okunan Kelime";
            //
            // pnlSeparator
            //
            this.pnlSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSeparator.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlSeparator.Location = new System.Drawing.Point(20, 306);
            this.pnlSeparator.Name = "pnlSeparator";
            this.pnlSeparator.Size = new System.Drawing.Size(290, 1);
            this.pnlSeparator.TabIndex = 10;
            //
            // tbVolume
            //
            this.tbVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVolume.BackColor = System.Drawing.Color.White;
            this.tbVolume.LargeChange = 10;
            this.tbVolume.Location = new System.Drawing.Point(12, 246);
            this.tbVolume.Maximum = 100;
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.ValueChanged += new System.EventHandler(this.TbVolume_ValueChanged);
            this.tbVolume.Size = new System.Drawing.Size(304, 45);
            this.tbVolume.SmallChange = 5;
            this.tbVolume.TabIndex = 9;
            this.tbVolume.TickFrequency = 10;
            this.tbVolume.Value = 100;
            //
            // lblVolumeValue
            //
            this.lblVolumeValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVolumeValue.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.lblVolumeValue.ForeColor = System.Drawing.Color.FromArgb(79, 70, 229);
            this.lblVolumeValue.Location = new System.Drawing.Point(230, 224);
            this.lblVolumeValue.Name = "lblVolumeValue";
            this.lblVolumeValue.Size = new System.Drawing.Size(80, 20);
            this.lblVolumeValue.TabIndex = 8;
            this.lblVolumeValue.Text = "%100";
            this.lblVolumeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblVolume
            //
            this.lblVolume.AutoSize = true;
            this.lblVolume.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblVolume.Location = new System.Drawing.Point(20, 225);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(70, 17);
            this.lblVolume.TabIndex = 7;
            this.lblVolume.Text = "Ses düzeyi";
            //
            // tbRate
            //
            this.tbRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRate.BackColor = System.Drawing.Color.White;
            this.tbRate.LargeChange = 2;
            this.tbRate.Location = new System.Drawing.Point(12, 170);
            this.tbRate.Minimum = -10;
            this.tbRate.Name = "tbRate";
            this.tbRate.ValueChanged += new System.EventHandler(this.TbRate_ValueChanged);
            this.tbRate.Size = new System.Drawing.Size(304, 45);
            this.tbRate.TabIndex = 6;
            this.tbRate.TickFrequency = 2;
            //
            // lblRateValue
            //
            this.lblRateValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRateValue.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.lblRateValue.ForeColor = System.Drawing.Color.FromArgb(79, 70, 229);
            this.lblRateValue.Location = new System.Drawing.Point(200, 148);
            this.lblRateValue.Name = "lblRateValue";
            this.lblRateValue.Size = new System.Drawing.Size(110, 20);
            this.lblRateValue.TabIndex = 5;
            this.lblRateValue.Text = "Normal (0)";
            this.lblRateValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblRate
            //
            this.lblRate.AutoSize = true;
            this.lblRate.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblRate.Location = new System.Drawing.Point(20, 149);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(27, 17);
            this.lblRate.TabIndex = 4;
            this.lblRate.Text = "Hız";
            //
            // lblVoiceHint
            //
            this.lblVoiceHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVoiceHint.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblVoiceHint.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblVoiceHint.Location = new System.Drawing.Point(20, 110);
            this.lblVoiceHint.Name = "lblVoiceHint";
            this.lblVoiceHint.Size = new System.Drawing.Size(290, 32);
            this.lblVoiceHint.TabIndex = 3;
            //
            // cmbVoice
            //
            this.cmbVoice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbVoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVoice.FormattingEnabled = true;
            this.cmbVoice.Location = new System.Drawing.Point(20, 80);
            this.cmbVoice.Name = "cmbVoice";
            this.cmbVoice.SelectedIndexChanged += new System.EventHandler(this.CmbVoice_SelectedIndexChanged);
            this.cmbVoice.Size = new System.Drawing.Size(290, 25);
            this.cmbVoice.TabIndex = 2;
            //
            // lblVoice
            //
            this.lblVoice.AutoSize = true;
            this.lblVoice.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblVoice.Location = new System.Drawing.Point(20, 58);
            this.lblVoice.Name = "lblVoice";
            this.lblVoice.Size = new System.Drawing.Size(26, 17);
            this.lblVoice.TabIndex = 1;
            this.lblVoice.Text = "Ses";
            //
            // lblSettingsTitle
            //
            this.lblSettingsTitle.AutoSize = true;
            this.lblSettingsTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.lblSettingsTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblSettingsTitle.Location = new System.Drawing.Point(18, 16);
            this.lblSettingsTitle.Name = "lblSettingsTitle";
            this.lblSettingsTitle.Size = new System.Drawing.Size(103, 21);
            this.lblSettingsTitle.TabIndex = 0;
            this.lblSettingsTitle.Text = "Ses Ayarları";
            //
            // pnlStt
            //
            this.pnlStt.Controls.Add(this.tlpStt);
            this.pnlStt.Controls.Add(this.pnlSttWarningHost);
            this.pnlStt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStt.Location = new System.Drawing.Point(20, 20);
            this.pnlStt.Name = "pnlStt";
            this.pnlStt.Size = new System.Drawing.Size(1140, 604);
            this.pnlStt.TabIndex = 1;
            this.pnlStt.Visible = false;
            //
            // tlpStt
            //
            this.tlpStt.ColumnCount = 2;
            this.tlpStt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpStt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpStt.Controls.Add(this.cardSttToolbar, 0, 0);
            this.tlpStt.Controls.Add(this.cardSttText, 0, 1);
            this.tlpStt.Controls.Add(this.cardSttList, 1, 1);
            this.tlpStt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStt.Location = new System.Drawing.Point(0, 72);
            this.tlpStt.Name = "tlpStt";
            this.tlpStt.RowCount = 2;
            this.tlpStt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tlpStt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStt.Size = new System.Drawing.Size(1140, 532);
            this.tlpStt.TabIndex = 1;
            //
            // cardSttToolbar
            //
            this.cardSttToolbar.BackColor = System.Drawing.Color.White;
            this.tlpStt.SetColumnSpan(this.cardSttToolbar, 2);
            this.cardSttToolbar.Controls.Add(this.flpSttToolbar);
            this.cardSttToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardSttToolbar.Location = new System.Drawing.Point(0, 0);
            this.cardSttToolbar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.cardSttToolbar.Name = "cardSttToolbar";
            this.cardSttToolbar.Padding = new System.Windows.Forms.Padding(18, 13, 18, 13);
            this.cardSttToolbar.Size = new System.Drawing.Size(1140, 66);
            this.cardSttToolbar.TabIndex = 0;
            //
            // flpSttToolbar
            //
            this.flpSttToolbar.BackColor = System.Drawing.Color.White;
            this.flpSttToolbar.Controls.Add(this.lblLanguage);
            this.flpSttToolbar.Controls.Add(this.cmbLanguage);
            this.flpSttToolbar.Controls.Add(this.btnListenStart);
            this.flpSttToolbar.Controls.Add(this.btnListenStop);
            this.flpSttToolbar.Controls.Add(this.btnSttClear);
            this.flpSttToolbar.Controls.Add(this.btnSaveResults);
            this.flpSttToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpSttToolbar.Location = new System.Drawing.Point(18, 13);
            this.flpSttToolbar.Name = "flpSttToolbar";
            this.flpSttToolbar.Size = new System.Drawing.Size(1104, 40);
            this.flpSttToolbar.TabIndex = 0;
            this.flpSttToolbar.WrapContents = false;
            //
            // lblLanguage
            //
            this.lblLanguage.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblLanguage.Location = new System.Drawing.Point(0, 0);
            this.lblLanguage.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(84, 40);
            this.lblLanguage.TabIndex = 0;
            this.lblLanguage.Text = "Tanıma dili";
            this.lblLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // cmbLanguage
            //
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(88, 8);
            this.cmbLanguage.Margin = new System.Windows.Forms.Padding(0, 8, 20, 0);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(280, 25);
            this.cmbLanguage.TabIndex = 1;
            //
            // btnListenStart
            //
            this.btnListenStart.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnListenStart.Location = new System.Drawing.Point(388, 0);
            this.btnListenStart.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnListenStart.Name = "btnListenStart";
            this.btnListenStart.Click += new System.EventHandler(this.BtnListenStart_Click);
            this.btnListenStart.Size = new System.Drawing.Size(180, 40);
            this.btnListenStart.TabIndex = 2;
            this.btnListenStart.Text = "●  Dinlemeyi Başlat";
            this.btnListenStart.Variant = SpeechApp.Controls.ButtonVariant.Danger;
            //
            // btnListenStop
            //
            this.btnListenStop.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnListenStop.Location = new System.Drawing.Point(576, 0);
            this.btnListenStop.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnListenStop.Name = "btnListenStop";
            this.btnListenStop.Click += new System.EventHandler(this.BtnListenStop_Click);
            this.btnListenStop.Size = new System.Drawing.Size(110, 40);
            this.btnListenStop.TabIndex = 3;
            this.btnListenStop.Text = "■  Durdur";
            //
            // btnSttClear
            //
            this.btnSttClear.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnSttClear.Location = new System.Drawing.Point(694, 0);
            this.btnSttClear.Margin = new System.Windows.Forms.Padding(0, 0, 24, 0);
            this.btnSttClear.Name = "btnSttClear";
            this.btnSttClear.Click += new System.EventHandler(this.BtnSttClear_Click);
            this.btnSttClear.Size = new System.Drawing.Size(110, 40);
            this.btnSttClear.TabIndex = 4;
            this.btnSttClear.Text = "✕  Temizle";
            //
            // btnSaveResults
            //
            this.btnSaveResults.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnSaveResults.Location = new System.Drawing.Point(828, 0);
            this.btnSaveResults.Margin = new System.Windows.Forms.Padding(0);
            this.btnSaveResults.Name = "btnSaveResults";
            this.btnSaveResults.Click += new System.EventHandler(this.BtnSaveResults_Click);
            this.btnSaveResults.Size = new System.Drawing.Size(190, 40);
            this.btnSaveResults.TabIndex = 5;
            this.btnSaveResults.Text = "Sonuçları Kaydet (.txt)";
            //
            // cardSttText
            //
            this.cardSttText.BackColor = System.Drawing.Color.White;
            this.cardSttText.Controls.Add(this.pnlSttEditor);
            this.cardSttText.Controls.Add(this.lblHypothesis);
            this.cardSttText.Controls.Add(this.lblSttTextTitle);
            this.cardSttText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardSttText.Location = new System.Drawing.Point(0, 82);
            this.cardSttText.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.cardSttText.Name = "cardSttText";
            this.cardSttText.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.cardSttText.Size = new System.Drawing.Size(562, 450);
            this.cardSttText.TabIndex = 1;
            //
            // pnlSttEditor
            //
            this.pnlSttEditor.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.pnlSttEditor.Controls.Add(this.rtbSttText);
            this.pnlSttEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSttEditor.Location = new System.Drawing.Point(20, 56);
            this.pnlSttEditor.Name = "pnlSttEditor";
            this.pnlSttEditor.Padding = new System.Windows.Forms.Padding(14, 12, 14, 12);
            this.pnlSttEditor.Size = new System.Drawing.Size(522, 344);
            this.pnlSttEditor.TabIndex = 1;
            //
            // rtbSttText
            //
            this.rtbSttText.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.rtbSttText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbSttText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSttText.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.rtbSttText.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.rtbSttText.Location = new System.Drawing.Point(14, 12);
            this.rtbSttText.Name = "rtbSttText";
            this.rtbSttText.ReadOnly = true;
            this.rtbSttText.Size = new System.Drawing.Size(494, 320);
            this.rtbSttText.TabIndex = 0;
            this.rtbSttText.Text = "";
            //
            // lblHypothesis
            //
            this.lblHypothesis.AutoEllipsis = true;
            this.lblHypothesis.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblHypothesis.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic);
            this.lblHypothesis.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblHypothesis.Location = new System.Drawing.Point(20, 400);
            this.lblHypothesis.Name = "lblHypothesis";
            this.lblHypothesis.Size = new System.Drawing.Size(522, 34);
            this.lblHypothesis.TabIndex = 2;
            this.lblHypothesis.Text = "Konuşma algılandığında ara sonuç burada görünür.";
            this.lblHypothesis.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // lblSttTextTitle
            //
            this.lblSttTextTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSttTextTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.lblSttTextTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblSttTextTitle.Location = new System.Drawing.Point(20, 16);
            this.lblSttTextTitle.Name = "lblSttTextTitle";
            this.lblSttTextTitle.Size = new System.Drawing.Size(522, 40);
            this.lblSttTextTitle.TabIndex = 0;
            this.lblSttTextTitle.Text = "Tanınan Metin";
            //
            // cardSttList
            //
            this.cardSttList.BackColor = System.Drawing.Color.White;
            this.cardSttList.Controls.Add(this.lvResults);
            this.cardSttList.Controls.Add(this.lblSttListTitle);
            this.cardSttList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardSttList.Location = new System.Drawing.Point(578, 82);
            this.cardSttList.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.cardSttList.Name = "cardSttList";
            this.cardSttList.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.cardSttList.Size = new System.Drawing.Size(562, 450);
            this.cardSttList.TabIndex = 2;
            //
            // lvResults
            //
            this.lvResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTime,
            this.colConfidence,
            this.colText});
            this.lvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvResults.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lvResults.FullRowSelect = true;
            this.lvResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvResults.Location = new System.Drawing.Point(20, 56);
            this.lvResults.MultiSelect = false;
            this.lvResults.Name = "lvResults";
            this.lvResults.Size = new System.Drawing.Size(522, 378);
            this.lvResults.TabIndex = 1;
            this.lvResults.UseCompatibleStateImageBehavior = false;
            this.lvResults.View = System.Windows.Forms.View.Details;
            //
            // colTime
            //
            this.colTime.Text = "Zaman";
            this.colTime.Width = 80;
            //
            // colConfidence
            //
            this.colConfidence.Text = "Güven";
            this.colConfidence.Width = 70;
            //
            // colText
            //
            this.colText.Text = "Tanınan metin";
            this.colText.Width = 360;
            //
            // lblSttListTitle
            //
            this.lblSttListTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSttListTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.lblSttListTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblSttListTitle.Location = new System.Drawing.Point(20, 16);
            this.lblSttListTitle.Name = "lblSttListTitle";
            this.lblSttListTitle.Size = new System.Drawing.Size(522, 40);
            this.lblSttListTitle.TabIndex = 0;
            this.lblSttListTitle.Text = "Sonuç Listesi";
            //
            // pnlSttWarningHost
            //
            this.pnlSttWarningHost.Controls.Add(this.cardSttWarning);
            this.pnlSttWarningHost.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSttWarningHost.Location = new System.Drawing.Point(0, 0);
            this.pnlSttWarningHost.Name = "pnlSttWarningHost";
            this.pnlSttWarningHost.Padding = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.pnlSttWarningHost.Size = new System.Drawing.Size(1140, 92);
            this.pnlSttWarningHost.TabIndex = 0;
            this.pnlSttWarningHost.Visible = false;
            //
            // cardSttWarning
            //
            this.cardSttWarning.BackColor = System.Drawing.Color.FromArgb(254, 243, 199);
            this.cardSttWarning.Controls.Add(this.lblSttWarning);
            this.cardSttWarning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardSttWarning.Location = new System.Drawing.Point(0, 0);
            this.cardSttWarning.Name = "cardSttWarning";
            this.cardSttWarning.Padding = new System.Windows.Forms.Padding(18, 6, 18, 6);
            this.cardSttWarning.Size = new System.Drawing.Size(1140, 76);
            this.cardSttWarning.TabIndex = 0;
            //
            // lblSttWarning
            //
            this.lblSttWarning.BackColor = System.Drawing.Color.FromArgb(254, 243, 199);
            this.lblSttWarning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSttWarning.ForeColor = System.Drawing.Color.FromArgb(146, 64, 14);
            this.lblSttWarning.Location = new System.Drawing.Point(18, 6);
            this.lblSttWarning.Name = "lblSttWarning";
            this.lblSttWarning.Size = new System.Drawing.Size(1104, 64);
            this.lblSttWarning.TabIndex = 0;
            this.lblSttWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(1180, 740);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.MinimumSize = new System.Drawing.Size(1120, 680);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YMH219 - Speech App";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlTts.ResumeLayout(false);
            this.tlpTts.ResumeLayout(false);
            this.cardTtsText.ResumeLayout(false);
            this.pnlTtsEditor.ResumeLayout(false);
            this.flpTtsButtons.ResumeLayout(false);
            this.cardTtsSettings.ResumeLayout(false);
            this.cardTtsSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRate)).EndInit();
            this.pnlStt.ResumeLayout(false);
            this.tlpStt.ResumeLayout(false);
            this.cardSttToolbar.ResumeLayout(false);
            this.flpSttToolbar.ResumeLayout(false);
            this.cardSttText.ResumeLayout(false);
            this.pnlSttEditor.ResumeLayout(false);
            this.cardSttList.ResumeLayout(false);
            this.pnlSttWarningHost.ResumeLayout(false);
            this.cardSttWarning.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private SpeechApp.Controls.ModernButton btnNavTts;
        private SpeechApp.Controls.ModernButton btnNavStt;
        private SpeechApp.Controls.StatusIndicator statusIndicator;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusMessage;
        private System.Windows.Forms.ToolStripStatusLabel lblEnvInfo;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlTts;
        private System.Windows.Forms.TableLayoutPanel tlpTts;
        private SpeechApp.Controls.CardPanel cardTtsText;
        private System.Windows.Forms.Label lblTtsTitle;
        private System.Windows.Forms.Panel pnlTtsEditor;
        private System.Windows.Forms.RichTextBox rtbTtsText;
        private System.Windows.Forms.FlowLayoutPanel flpTtsButtons;
        private SpeechApp.Controls.ModernButton btnSpeak;
        private SpeechApp.Controls.ModernButton btnPauseResume;
        private SpeechApp.Controls.ModernButton btnStop;
        private SpeechApp.Controls.ModernButton btnTtsClear;
        private SpeechApp.Controls.ModernButton btnOpenText;
        private SpeechApp.Controls.ModernButton btnSaveText;
        private SpeechApp.Controls.CardPanel cardTtsSettings;
        private System.Windows.Forms.Label lblSettingsTitle;
        private System.Windows.Forms.Label lblVoice;
        private System.Windows.Forms.ComboBox cmbVoice;
        private System.Windows.Forms.Label lblVoiceHint;
        private System.Windows.Forms.Label lblRate;
        private System.Windows.Forms.Label lblRateValue;
        private System.Windows.Forms.TrackBar tbRate;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.Label lblVolumeValue;
        private System.Windows.Forms.TrackBar tbVolume;
        private System.Windows.Forms.Panel pnlSeparator;
        private System.Windows.Forms.Label lblReadingTitle;
        private System.Windows.Forms.Label lblCurrentWord;
        private System.Windows.Forms.ProgressBar progressReading;
        private System.Windows.Forms.Label lblReadingDetail;
        private System.Windows.Forms.Label lblSettingsNote;
        private System.Windows.Forms.Panel pnlStt;
        private System.Windows.Forms.Panel pnlSttWarningHost;
        private SpeechApp.Controls.CardPanel cardSttWarning;
        private System.Windows.Forms.Label lblSttWarning;
        private System.Windows.Forms.TableLayoutPanel tlpStt;
        private SpeechApp.Controls.CardPanel cardSttToolbar;
        private System.Windows.Forms.FlowLayoutPanel flpSttToolbar;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private SpeechApp.Controls.ModernButton btnListenStart;
        private SpeechApp.Controls.ModernButton btnListenStop;
        private SpeechApp.Controls.ModernButton btnSttClear;
        private SpeechApp.Controls.ModernButton btnSaveResults;
        private SpeechApp.Controls.CardPanel cardSttText;
        private System.Windows.Forms.Label lblSttTextTitle;
        private System.Windows.Forms.Panel pnlSttEditor;
        private System.Windows.Forms.RichTextBox rtbSttText;
        private System.Windows.Forms.Label lblHypothesis;
        private SpeechApp.Controls.CardPanel cardSttList;
        private System.Windows.Forms.Label lblSttListTitle;
        private System.Windows.Forms.ListView lvResults;
        private System.Windows.Forms.ColumnHeader colTime;
        private System.Windows.Forms.ColumnHeader colConfidence;
        private System.Windows.Forms.ColumnHeader colText;
    }
}
