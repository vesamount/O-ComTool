namespace O_ComTool_Pro
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gpdSend = new System.Windows.Forms.GroupBox();
            this.chkNewLine = new System.Windows.Forms.CheckBox();
            this.labRepeatInterval = new System.Windows.Forms.Label();
            this.btnClearSend = new System.Windows.Forms.Button();
            this.chkRepeatSend = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.nudRepeatInterval = new System.Windows.Forms.NumericUpDown();
            this.chkAutoCount = new System.Windows.Forms.CheckBox();
            this.radHexSend = new System.Windows.Forms.RadioButton();
            this.radAsciiSend = new System.Windows.Forms.RadioButton();
            this.gpdReceive = new System.Windows.Forms.GroupBox();
            this.picReceiveLed = new System.Windows.Forms.PictureBox();
            this.chkAutoReply = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.labReplyDelay = new System.Windows.Forms.Label();
            this.nudReplyDelay = new System.Windows.Forms.NumericUpDown();
            this.btnClearReceive = new System.Windows.Forms.Button();
            this.chkAutoSave = new System.Windows.Forms.CheckBox();
            this.chkShowTime = new System.Windows.Forms.CheckBox();
            this.chkAutoLine = new System.Windows.Forms.CheckBox();
            this.radHexReceive = new System.Windows.Forms.RadioButton();
            this.radAsciiReceive = new System.Windows.Forms.RadioButton();
            this.gpdCom = new System.Windows.Forms.GroupBox();
            this.cmbFlowCtrl = new System.Windows.Forms.ComboBox();
            this.labFlowCtrl = new System.Windows.Forms.Label();
            this.picConnectStatus = new System.Windows.Forms.PictureBox();
            this.btnOpenCom = new System.Windows.Forms.Button();
            this.cmbStopBit = new System.Windows.Forms.ComboBox();
            this.cmbParityBit = new System.Windows.Forms.ComboBox();
            this.cmbDataBit = new System.Windows.Forms.ComboBox();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.cmbCom = new System.Windows.Forms.ComboBox();
            this.labStopBit = new System.Windows.Forms.Label();
            this.labParityBit = new System.Windows.Forms.Label();
            this.labDataBit = new System.Windows.Forms.Label();
            this.labBaudRate = new System.Windows.Forms.Label();
            this.labPort = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.o_ScrollBar1 = new O_ComTool_Pro.O_ScrollBar();
            this.fctbReceive = new FastColoredTextBoxNS.FastColoredTextBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsH2A = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsA2H = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsHexFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsCalcLength = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsCheckSum = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsXor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsSaveCur = new System.Windows.Forms.ToolStripMenuItem();
            this.picAddQuickSend = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.btnSend = new System.Windows.Forms.Button();
            this.txbSend = new System.Windows.Forms.TextBox();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmstbSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmstbCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.cmstbH2A = new System.Windows.Forms.ToolStripMenuItem();
            this.cmstbA2H = new System.Windows.Forms.ToolStripMenuItem();
            this.cmstbHexFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.cmstbCalcLength = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.cmstbCheckSum = new System.Windows.Forms.ToolStripMenuItem();
            this.cmstbXor = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEditFile = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.cmbLoadFile = new System.Windows.Forms.ComboBox();
            this.tbpQuick = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddNote = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmImportConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExportConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpenLogPath = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOption = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTool = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCalc = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDevMgmt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAscii = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmChangeDisplay = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHomePage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDonate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmClearSend = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmClearRece = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssLabCom = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tssLabReset = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssImageChange = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssRxCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssTxCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLabRate = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLabRateValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssCurstatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.llabAd = new System.Windows.Forms.LinkLabel();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timerAutoReply = new System.Windows.Forms.Timer(this.components);
            this.timerRepeat = new System.Windows.Forms.Timer(this.components);
            this.timerProcessBar = new System.Windows.Forms.Timer(this.components);
            this.timerReceiveLed = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsOption = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsExit = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gpdSend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepeatInterval)).BeginInit();
            this.gpdReceive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReceiveLed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReplyDelay)).BeginInit();
            this.gpdCom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picConnectStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fctbReceive)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAddQuickSend)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gpdSend);
            this.splitContainer1.Panel1.Controls.Add(this.gpdReceive);
            this.splitContainer1.Panel1.Controls.Add(this.gpdCom);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(784, 550);
            this.splitContainer1.SplitterDistance = 140;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.Click += new System.EventHandler(this.splitContainer1_Click);
            // 
            // gpdSend
            // 
            this.gpdSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gpdSend.BackColor = System.Drawing.Color.White;
            this.gpdSend.Controls.Add(this.chkNewLine);
            this.gpdSend.Controls.Add(this.labRepeatInterval);
            this.gpdSend.Controls.Add(this.btnClearSend);
            this.gpdSend.Controls.Add(this.chkRepeatSend);
            this.gpdSend.Controls.Add(this.label10);
            this.gpdSend.Controls.Add(this.nudRepeatInterval);
            this.gpdSend.Controls.Add(this.chkAutoCount);
            this.gpdSend.Controls.Add(this.radHexSend);
            this.gpdSend.Controls.Add(this.radAsciiSend);
            this.gpdSend.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.gpdSend.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpdSend.Location = new System.Drawing.Point(6, 390);
            this.gpdSend.Name = "gpdSend";
            this.gpdSend.Size = new System.Drawing.Size(129, 159);
            this.gpdSend.TabIndex = 5;
            this.gpdSend.TabStop = false;
            this.gpdSend.Text = "发送设置";
            // 
            // chkNewLine
            // 
            this.chkNewLine.AutoSize = true;
            this.chkNewLine.Location = new System.Drawing.Point(9, 59);
            this.chkNewLine.Name = "chkNewLine";
            this.chkNewLine.Size = new System.Drawing.Size(72, 16);
            this.chkNewLine.TabIndex = 9;
            this.chkNewLine.Text = "追加新行";
            this.chkNewLine.UseVisualStyleBackColor = true;
            // 
            // labRepeatInterval
            // 
            this.labRepeatInterval.AutoSize = true;
            this.labRepeatInterval.Location = new System.Drawing.Point(4, 106);
            this.labRepeatInterval.Name = "labRepeatInterval";
            this.labRepeatInterval.Size = new System.Drawing.Size(41, 12);
            this.labRepeatInterval.TabIndex = 8;
            this.labRepeatInterval.Text = "间隔：";
            // 
            // btnClearSend
            // 
            this.btnClearSend.Location = new System.Drawing.Point(46, 131);
            this.btnClearSend.Name = "btnClearSend";
            this.btnClearSend.Size = new System.Drawing.Size(75, 23);
            this.btnClearSend.TabIndex = 7;
            this.btnClearSend.Text = "清除发送";
            this.btnClearSend.UseVisualStyleBackColor = true;
            this.btnClearSend.Click += new System.EventHandler(this.btnClearSend_Click);
            // 
            // chkRepeatSend
            // 
            this.chkRepeatSend.AutoSize = true;
            this.chkRepeatSend.Location = new System.Drawing.Point(9, 82);
            this.chkRepeatSend.Name = "chkRepeatSend";
            this.chkRepeatSend.Size = new System.Drawing.Size(72, 16);
            this.chkRepeatSend.TabIndex = 6;
            this.chkRepeatSend.Text = "重复发送";
            this.chkRepeatSend.UseVisualStyleBackColor = true;
            this.chkRepeatSend.CheckedChanged += new System.EventHandler(this.chkRepeatSend_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(108, 106);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 5;
            this.label10.Text = "MS";
            // 
            // nudRepeatInterval
            // 
            this.nudRepeatInterval.Location = new System.Drawing.Point(45, 102);
            this.nudRepeatInterval.Maximum = new decimal(new int[] {
            86400000,
            0,
            0,
            0});
            this.nudRepeatInterval.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudRepeatInterval.Name = "nudRepeatInterval";
            this.nudRepeatInterval.Size = new System.Drawing.Size(59, 21);
            this.nudRepeatInterval.TabIndex = 4;
            this.nudRepeatInterval.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // chkAutoCount
            // 
            this.chkAutoCount.AutoSize = true;
            this.chkAutoCount.Location = new System.Drawing.Point(9, 37);
            this.chkAutoCount.Name = "chkAutoCount";
            this.chkAutoCount.Size = new System.Drawing.Size(72, 16);
            this.chkAutoCount.TabIndex = 3;
            this.chkAutoCount.Text = "自动计数";
            this.chkAutoCount.UseVisualStyleBackColor = true;
            this.chkAutoCount.CheckedChanged += new System.EventHandler(this.chkAutoCount_CheckedChanged);
            // 
            // radHexSend
            // 
            this.radHexSend.AutoSize = true;
            this.radHexSend.Location = new System.Drawing.Point(81, 15);
            this.radHexSend.Name = "radHexSend";
            this.radHexSend.Size = new System.Drawing.Size(41, 16);
            this.radHexSend.TabIndex = 2;
            this.radHexSend.Text = "HEX";
            this.radHexSend.UseVisualStyleBackColor = true;
            // 
            // radAsciiSend
            // 
            this.radAsciiSend.AutoSize = true;
            this.radAsciiSend.Checked = true;
            this.radAsciiSend.Location = new System.Drawing.Point(9, 15);
            this.radAsciiSend.Name = "radAsciiSend";
            this.radAsciiSend.Size = new System.Drawing.Size(53, 16);
            this.radAsciiSend.TabIndex = 1;
            this.radAsciiSend.TabStop = true;
            this.radAsciiSend.Text = "ASCII";
            this.radAsciiSend.UseVisualStyleBackColor = true;
            // 
            // gpdReceive
            // 
            this.gpdReceive.BackColor = System.Drawing.Color.White;
            this.gpdReceive.Controls.Add(this.picReceiveLed);
            this.gpdReceive.Controls.Add(this.chkAutoReply);
            this.gpdReceive.Controls.Add(this.label12);
            this.gpdReceive.Controls.Add(this.labReplyDelay);
            this.gpdReceive.Controls.Add(this.nudReplyDelay);
            this.gpdReceive.Controls.Add(this.btnClearReceive);
            this.gpdReceive.Controls.Add(this.chkAutoSave);
            this.gpdReceive.Controls.Add(this.chkShowTime);
            this.gpdReceive.Controls.Add(this.chkAutoLine);
            this.gpdReceive.Controls.Add(this.radHexReceive);
            this.gpdReceive.Controls.Add(this.radAsciiReceive);
            this.gpdReceive.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.gpdReceive.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpdReceive.Location = new System.Drawing.Point(6, 203);
            this.gpdReceive.Name = "gpdReceive";
            this.gpdReceive.Size = new System.Drawing.Size(129, 181);
            this.gpdReceive.TabIndex = 4;
            this.gpdReceive.TabStop = false;
            this.gpdReceive.Text = "接收设置";
            // 
            // picReceiveLed
            // 
            this.picReceiveLed.Image = global::O_ComTool_Pro.Properties.Resources.Led_Red_50px;
            this.picReceiveLed.Location = new System.Drawing.Point(15, 156);
            this.picReceiveLed.Name = "picReceiveLed";
            this.picReceiveLed.Size = new System.Drawing.Size(15, 15);
            this.picReceiveLed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picReceiveLed.TabIndex = 12;
            this.picReceiveLed.TabStop = false;
            // 
            // chkAutoReply
            // 
            this.chkAutoReply.AutoSize = true;
            this.chkAutoReply.Location = new System.Drawing.Point(9, 103);
            this.chkAutoReply.Name = "chkAutoReply";
            this.chkAutoReply.Size = new System.Drawing.Size(72, 16);
            this.chkAutoReply.TabIndex = 9;
            this.chkAutoReply.Text = "自动回复";
            this.chkAutoReply.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(108, 129);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 8;
            this.label12.Text = "MS";
            // 
            // labReplyDelay
            // 
            this.labReplyDelay.AutoSize = true;
            this.labReplyDelay.Location = new System.Drawing.Point(4, 129);
            this.labReplyDelay.Name = "labReplyDelay";
            this.labReplyDelay.Size = new System.Drawing.Size(41, 12);
            this.labReplyDelay.TabIndex = 7;
            this.labReplyDelay.Text = "延迟：";
            // 
            // nudReplyDelay
            // 
            this.nudReplyDelay.Location = new System.Drawing.Point(45, 125);
            this.nudReplyDelay.Maximum = new decimal(new int[] {
            3600000,
            0,
            0,
            0});
            this.nudReplyDelay.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudReplyDelay.Name = "nudReplyDelay";
            this.nudReplyDelay.Size = new System.Drawing.Size(59, 21);
            this.nudReplyDelay.TabIndex = 6;
            this.nudReplyDelay.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // btnClearReceive
            // 
            this.btnClearReceive.Location = new System.Drawing.Point(46, 152);
            this.btnClearReceive.Name = "btnClearReceive";
            this.btnClearReceive.Size = new System.Drawing.Size(75, 23);
            this.btnClearReceive.TabIndex = 5;
            this.btnClearReceive.Text = "清空接收";
            this.btnClearReceive.UseVisualStyleBackColor = true;
            this.btnClearReceive.Click += new System.EventHandler(this.btnClearReceive_Click);
            // 
            // chkAutoSave
            // 
            this.chkAutoSave.AutoSize = true;
            this.chkAutoSave.Location = new System.Drawing.Point(9, 81);
            this.chkAutoSave.Name = "chkAutoSave";
            this.chkAutoSave.Size = new System.Drawing.Size(72, 16);
            this.chkAutoSave.TabIndex = 4;
            this.chkAutoSave.Text = "自动保存";
            this.chkAutoSave.UseVisualStyleBackColor = true;
            this.chkAutoSave.CheckedChanged += new System.EventHandler(this.chkAutoSave_CheckedChanged);
            // 
            // chkShowTime
            // 
            this.chkShowTime.AutoSize = true;
            this.chkShowTime.Location = new System.Drawing.Point(9, 59);
            this.chkShowTime.Name = "chkShowTime";
            this.chkShowTime.Size = new System.Drawing.Size(72, 16);
            this.chkShowTime.TabIndex = 3;
            this.chkShowTime.Text = "显示时间";
            this.chkShowTime.UseVisualStyleBackColor = true;
            this.chkShowTime.CheckedChanged += new System.EventHandler(this.chkShowTime_CheckedChanged);
            // 
            // chkAutoLine
            // 
            this.chkAutoLine.AutoSize = true;
            this.chkAutoLine.Location = new System.Drawing.Point(9, 37);
            this.chkAutoLine.Name = "chkAutoLine";
            this.chkAutoLine.Size = new System.Drawing.Size(72, 16);
            this.chkAutoLine.TabIndex = 2;
            this.chkAutoLine.Text = "自动换行";
            this.chkAutoLine.UseVisualStyleBackColor = true;
            // 
            // radHexReceive
            // 
            this.radHexReceive.AutoSize = true;
            this.radHexReceive.Location = new System.Drawing.Point(81, 15);
            this.radHexReceive.Name = "radHexReceive";
            this.radHexReceive.Size = new System.Drawing.Size(41, 16);
            this.radHexReceive.TabIndex = 1;
            this.radHexReceive.Text = "HEX";
            this.radHexReceive.UseVisualStyleBackColor = true;
            // 
            // radAsciiReceive
            // 
            this.radAsciiReceive.AutoSize = true;
            this.radAsciiReceive.Checked = true;
            this.radAsciiReceive.Location = new System.Drawing.Point(9, 15);
            this.radAsciiReceive.Name = "radAsciiReceive";
            this.radAsciiReceive.Size = new System.Drawing.Size(53, 16);
            this.radAsciiReceive.TabIndex = 0;
            this.radAsciiReceive.TabStop = true;
            this.radAsciiReceive.Text = "ASCII";
            this.radAsciiReceive.UseVisualStyleBackColor = true;
            // 
            // gpdCom
            // 
            this.gpdCom.BackColor = System.Drawing.Color.White;
            this.gpdCom.Controls.Add(this.cmbFlowCtrl);
            this.gpdCom.Controls.Add(this.labFlowCtrl);
            this.gpdCom.Controls.Add(this.picConnectStatus);
            this.gpdCom.Controls.Add(this.btnOpenCom);
            this.gpdCom.Controls.Add(this.cmbStopBit);
            this.gpdCom.Controls.Add(this.cmbParityBit);
            this.gpdCom.Controls.Add(this.cmbDataBit);
            this.gpdCom.Controls.Add(this.cmbBaudRate);
            this.gpdCom.Controls.Add(this.cmbCom);
            this.gpdCom.Controls.Add(this.labStopBit);
            this.gpdCom.Controls.Add(this.labParityBit);
            this.gpdCom.Controls.Add(this.labDataBit);
            this.gpdCom.Controls.Add(this.labBaudRate);
            this.gpdCom.Controls.Add(this.labPort);
            this.gpdCom.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.gpdCom.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpdCom.Location = new System.Drawing.Point(6, 3);
            this.gpdCom.Name = "gpdCom";
            this.gpdCom.Size = new System.Drawing.Size(129, 200);
            this.gpdCom.TabIndex = 2;
            this.gpdCom.TabStop = false;
            this.gpdCom.Text = "串口设置";
            // 
            // cmbFlowCtrl
            // 
            this.cmbFlowCtrl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFlowCtrl.FormattingEnabled = true;
            this.cmbFlowCtrl.Items.AddRange(new object[] {
            "None",
            "XOn/XOff",
            "RTS/CTS"});
            this.cmbFlowCtrl.Location = new System.Drawing.Point(54, 146);
            this.cmbFlowCtrl.Name = "cmbFlowCtrl";
            this.cmbFlowCtrl.Size = new System.Drawing.Size(68, 20);
            this.cmbFlowCtrl.TabIndex = 13;
            // 
            // labFlowCtrl
            // 
            this.labFlowCtrl.AutoSize = true;
            this.labFlowCtrl.Location = new System.Drawing.Point(4, 149);
            this.labFlowCtrl.Name = "labFlowCtrl";
            this.labFlowCtrl.Size = new System.Drawing.Size(53, 12);
            this.labFlowCtrl.TabIndex = 12;
            this.labFlowCtrl.Text = "流  控：";
            // 
            // picConnectStatus
            // 
            this.picConnectStatus.Image = global::O_ComTool_Pro.Properties.Resources.Disconnected_48px;
            this.picConnectStatus.Location = new System.Drawing.Point(10, 170);
            this.picConnectStatus.Name = "picConnectStatus";
            this.picConnectStatus.Size = new System.Drawing.Size(24, 24);
            this.picConnectStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picConnectStatus.TabIndex = 11;
            this.picConnectStatus.TabStop = false;
            // 
            // btnOpenCom
            // 
            this.btnOpenCom.BackColor = System.Drawing.Color.Chocolate;
            this.btnOpenCom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOpenCom.Location = new System.Drawing.Point(46, 171);
            this.btnOpenCom.Name = "btnOpenCom";
            this.btnOpenCom.Size = new System.Drawing.Size(76, 23);
            this.btnOpenCom.TabIndex = 10;
            this.btnOpenCom.Text = "打开串口";
            this.btnOpenCom.UseVisualStyleBackColor = false;
            this.btnOpenCom.Click += new System.EventHandler(this.btnOpenCom_Click);
            // 
            // cmbStopBit
            // 
            this.cmbStopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBit.FormattingEnabled = true;
            this.cmbStopBit.Items.AddRange(new object[] {
            "1",
            "2",
            "1.5"});
            this.cmbStopBit.Location = new System.Drawing.Point(54, 121);
            this.cmbStopBit.Name = "cmbStopBit";
            this.cmbStopBit.Size = new System.Drawing.Size(68, 20);
            this.cmbStopBit.TabIndex = 9;
            // 
            // cmbParityBit
            // 
            this.cmbParityBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParityBit.FormattingEnabled = true;
            this.cmbParityBit.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd"});
            this.cmbParityBit.Location = new System.Drawing.Point(54, 95);
            this.cmbParityBit.Name = "cmbParityBit";
            this.cmbParityBit.Size = new System.Drawing.Size(68, 20);
            this.cmbParityBit.TabIndex = 8;
            // 
            // cmbDataBit
            // 
            this.cmbDataBit.BackColor = System.Drawing.SystemColors.Window;
            this.cmbDataBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataBit.FormattingEnabled = true;
            this.cmbDataBit.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cmbDataBit.Location = new System.Drawing.Point(54, 69);
            this.cmbDataBit.Name = "cmbDataBit";
            this.cmbDataBit.Size = new System.Drawing.Size(68, 20);
            this.cmbDataBit.TabIndex = 7;
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmbBaudRate.Location = new System.Drawing.Point(54, 43);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(68, 20);
            this.cmbBaudRate.TabIndex = 6;
            // 
            // cmbCom
            // 
            this.cmbCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCom.FormattingEnabled = true;
            this.cmbCom.Location = new System.Drawing.Point(54, 17);
            this.cmbCom.Name = "cmbCom";
            this.cmbCom.Size = new System.Drawing.Size(68, 20);
            this.cmbCom.TabIndex = 5;
            this.cmbCom.DropDown += new System.EventHandler(this.cmbCom_DropDown);
            // 
            // labStopBit
            // 
            this.labStopBit.AutoSize = true;
            this.labStopBit.Location = new System.Drawing.Point(4, 124);
            this.labStopBit.Name = "labStopBit";
            this.labStopBit.Size = new System.Drawing.Size(53, 12);
            this.labStopBit.TabIndex = 4;
            this.labStopBit.Text = "停止位：";
            // 
            // labParityBit
            // 
            this.labParityBit.AutoSize = true;
            this.labParityBit.Location = new System.Drawing.Point(4, 98);
            this.labParityBit.Name = "labParityBit";
            this.labParityBit.Size = new System.Drawing.Size(53, 12);
            this.labParityBit.TabIndex = 3;
            this.labParityBit.Text = "校验位：";
            // 
            // labDataBit
            // 
            this.labDataBit.AutoSize = true;
            this.labDataBit.Location = new System.Drawing.Point(4, 72);
            this.labDataBit.Name = "labDataBit";
            this.labDataBit.Size = new System.Drawing.Size(53, 12);
            this.labDataBit.TabIndex = 2;
            this.labDataBit.Text = "数据位：";
            // 
            // labBaudRate
            // 
            this.labBaudRate.AutoSize = true;
            this.labBaudRate.Location = new System.Drawing.Point(4, 46);
            this.labBaudRate.Name = "labBaudRate";
            this.labBaudRate.Size = new System.Drawing.Size(53, 12);
            this.labBaudRate.TabIndex = 1;
            this.labBaudRate.Text = "波特率：";
            // 
            // labPort
            // 
            this.labPort.AutoSize = true;
            this.labPort.Location = new System.Drawing.Point(4, 20);
            this.labPort.Name = "labPort";
            this.labPort.Size = new System.Drawing.Size(53, 12);
            this.labPort.TabIndex = 0;
            this.labPort.Text = "端口号：";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.o_ScrollBar1);
            this.splitContainer2.Panel1.Controls.Add(this.fctbReceive);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.picAddQuickSend);
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Panel2MinSize = 0;
            this.splitContainer2.Size = new System.Drawing.Size(640, 550);
            this.splitContainer2.SplitterDistance = 395;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
            // 
            // o_ScrollBar1
            // 
            this.o_ScrollBar1.BorderColor = System.Drawing.Color.Silver;
            this.o_ScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.o_ScrollBar1.Location = new System.Drawing.Point(628, 0);
            this.o_ScrollBar1.Maximum = 100;
            this.o_ScrollBar1.Name = "o_ScrollBar1";
            this.o_ScrollBar1.Orientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
            this.o_ScrollBar1.Size = new System.Drawing.Size(12, 395);
            this.o_ScrollBar1.TabIndex = 7;
            this.o_ScrollBar1.Text = "o_ScrollBar1";
            this.o_ScrollBar1.ThumbColor = System.Drawing.Color.Gray;
            this.o_ScrollBar1.ThumbSize = 20;
            this.o_ScrollBar1.Value = 0;
            this.o_ScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.o_ScrollBar1_Scroll);
            // 
            // fctbReceive
            // 
            this.fctbReceive.AllowMacroRecording = false;
            this.fctbReceive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fctbReceive.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fctbReceive.AutoIndent = false;
            this.fctbReceive.AutoIndentChars = false;
            this.fctbReceive.AutoIndentExistingLines = false;
            this.fctbReceive.AutoScrollMinSize = new System.Drawing.Size(0, 16);
            this.fctbReceive.BackBrush = null;
            this.fctbReceive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(34)))));
            this.fctbReceive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fctbReceive.CharHeight = 16;
            this.fctbReceive.CharWidth = 8;
            this.fctbReceive.ContextMenuStrip = this.contextMenuStrip2;
            this.fctbReceive.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fctbReceive.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fctbReceive.Font = new System.Drawing.Font("Consolas", 10.5F);
            this.fctbReceive.ForeColor = System.Drawing.Color.White;
            this.fctbReceive.HighlightFoldingIndicator = false;
            this.fctbReceive.HighlightingRangeType = FastColoredTextBoxNS.HighlightingRangeType.VisibleRange;
            this.fctbReceive.Hotkeys = resources.GetString("fctbReceive.Hotkeys");
            this.fctbReceive.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.fctbReceive.IsReplaceMode = false;
            this.fctbReceive.Location = new System.Drawing.Point(0, 0);
            this.fctbReceive.Name = "fctbReceive";
            this.fctbReceive.Paddings = new System.Windows.Forms.Padding(0);
            this.fctbReceive.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctbReceive.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fctbReceive.ServiceColors")));
            this.fctbReceive.ShowLineNumbers = false;
            this.fctbReceive.ShowScrollBars = false;
            this.fctbReceive.Size = new System.Drawing.Size(628, 395);
            this.fctbReceive.TabIndex = 6;
            this.fctbReceive.WordWrap = true;
            this.fctbReceive.WordWrapAutoIndent = false;
            this.fctbReceive.Zoom = 100;
            this.fctbReceive.VisibleRangeChanged += new System.EventHandler(this.fctbReceive_VisibleRangeChanged);
            this.fctbReceive.ScrollbarsUpdated += new System.EventHandler(this.fctbReceive_ScrollbarsUpdated);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsSelectAll,
            this.cmsCopy,
            this.cmsPaste,
            this.toolStripSeparator2,
            this.cmsH2A,
            this.cmsA2H,
            this.cmsHexFormat,
            this.cmsCalcLength,
            this.toolStripSeparator3,
            this.cmsCheckSum,
            this.cmsXor,
            this.toolStripSeparator4,
            this.cmsSaveCur});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(149, 242);
            this.contextMenuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip2_Opening);
            // 
            // cmsSelectAll
            // 
            this.cmsSelectAll.Name = "cmsSelectAll";
            this.cmsSelectAll.Size = new System.Drawing.Size(148, 22);
            this.cmsSelectAll.Text = "全选";
            this.cmsSelectAll.Click += new System.EventHandler(this.cmsSelectAll_Click);
            // 
            // cmsCopy
            // 
            this.cmsCopy.Name = "cmsCopy";
            this.cmsCopy.Size = new System.Drawing.Size(148, 22);
            this.cmsCopy.Text = "复制";
            this.cmsCopy.Click += new System.EventHandler(this.cmsCopy_Click);
            // 
            // cmsPaste
            // 
            this.cmsPaste.Name = "cmsPaste";
            this.cmsPaste.Size = new System.Drawing.Size(148, 22);
            this.cmsPaste.Text = "粘贴";
            this.cmsPaste.Click += new System.EventHandler(this.cmsPaste_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // cmsH2A
            // 
            this.cmsH2A.Name = "cmsH2A";
            this.cmsH2A.Size = new System.Drawing.Size(148, 22);
            this.cmsH2A.Text = "Hex2Ascii";
            this.cmsH2A.Click += new System.EventHandler(this.cmsH2A_Click);
            // 
            // cmsA2H
            // 
            this.cmsA2H.Name = "cmsA2H";
            this.cmsA2H.Size = new System.Drawing.Size(148, 22);
            this.cmsA2H.Text = "Ascii2Hex";
            this.cmsA2H.Click += new System.EventHandler(this.cmsA2H_Click);
            // 
            // cmsHexFormat
            // 
            this.cmsHexFormat.Name = "cmsHexFormat";
            this.cmsHexFormat.Size = new System.Drawing.Size(148, 22);
            this.cmsHexFormat.Text = "Hex格式化";
            this.cmsHexFormat.Click += new System.EventHandler(this.cmsHexFormat_Click);
            // 
            // cmsCalcLength
            // 
            this.cmsCalcLength.Name = "cmsCalcLength";
            this.cmsCalcLength.Size = new System.Drawing.Size(148, 22);
            this.cmsCalcLength.Text = "计算字节长度";
            this.cmsCalcLength.Click += new System.EventHandler(this.cmsCalcLength_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(145, 6);
            // 
            // cmsCheckSum
            // 
            this.cmsCheckSum.Name = "cmsCheckSum";
            this.cmsCheckSum.Size = new System.Drawing.Size(148, 22);
            this.cmsCheckSum.Text = "校验和";
            this.cmsCheckSum.Click += new System.EventHandler(this.cmsCheckSum_Click);
            // 
            // cmsXor
            // 
            this.cmsXor.Name = "cmsXor";
            this.cmsXor.Size = new System.Drawing.Size(148, 22);
            this.cmsXor.Text = "异或";
            this.cmsXor.Click += new System.EventHandler(this.cmsXor_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(145, 6);
            // 
            // cmsSaveCur
            // 
            this.cmsSaveCur.Name = "cmsSaveCur";
            this.cmsSaveCur.Size = new System.Drawing.Size(148, 22);
            this.cmsSaveCur.Text = "保存当前数据";
            this.cmsSaveCur.Click += new System.EventHandler(this.cmsSaveCur_Click);
            // 
            // picAddQuickSend
            // 
            this.picAddQuickSend.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.picAddQuickSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picAddQuickSend.Image = global::O_ComTool_Pro.Properties.Resources.Add_48px;
            this.picAddQuickSend.Location = new System.Drawing.Point(580, 59);
            this.picAddQuickSend.Name = "picAddQuickSend";
            this.picAddQuickSend.Size = new System.Drawing.Size(40, 40);
            this.picAddQuickSend.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAddQuickSend.TabIndex = 0;
            this.picAddQuickSend.TabStop = false;
            this.picAddQuickSend.Visible = false;
            this.picAddQuickSend.Click += new System.EventHandler(this.picAddQuickSend_Click);
            this.picAddQuickSend.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picAddQuickSend_MouseDown);
            this.picAddQuickSend.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picAddQuickSend_MouseUp);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpGeneral);
            this.tabControl1.Controls.Add(this.tbpQuick);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(640, 150);
            this.tabControl1.TabIndex = 6;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.btnSend);
            this.tbpGeneral.Controls.Add(this.txbSend);
            this.tbpGeneral.Controls.Add(this.btnEditFile);
            this.tbpGeneral.Controls.Add(this.btnLoadFile);
            this.tbpGeneral.Controls.Add(this.cmbLoadFile);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeneral.Size = new System.Drawing.Size(632, 124);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "通用";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSend.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSend.Location = new System.Drawing.Point(556, 24);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 52);
            this.btnSend.TabIndex = 18;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txbSend
            // 
            this.txbSend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbSend.ContextMenuStrip = this.contextMenuStrip3;
            this.txbSend.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbSend.Location = new System.Drawing.Point(3, 3);
            this.txbSend.Multiline = true;
            this.txbSend.Name = "txbSend";
            this.txbSend.Size = new System.Drawing.Size(548, 91);
            this.txbSend.TabIndex = 19;
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmstbSelectAll,
            this.cmstbCopy,
            this.toolStripMenuItem3,
            this.toolStripSeparator5,
            this.cmstbH2A,
            this.cmstbA2H,
            this.cmstbHexFormat,
            this.cmstbCalcLength,
            this.toolStripSeparator6,
            this.cmstbCheckSum,
            this.cmstbXor});
            this.contextMenuStrip3.Name = "contextMenuStrip2";
            this.contextMenuStrip3.Size = new System.Drawing.Size(149, 214);
            this.contextMenuStrip3.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip3_Opening);
            // 
            // cmstbSelectAll
            // 
            this.cmstbSelectAll.Name = "cmstbSelectAll";
            this.cmstbSelectAll.Size = new System.Drawing.Size(148, 22);
            this.cmstbSelectAll.Text = "全选";
            this.cmstbSelectAll.Click += new System.EventHandler(this.cmstbSelectAll_Click);
            // 
            // cmstbCopy
            // 
            this.cmstbCopy.Name = "cmstbCopy";
            this.cmstbCopy.Size = new System.Drawing.Size(148, 22);
            this.cmstbCopy.Text = "复制";
            this.cmstbCopy.Click += new System.EventHandler(this.cmstbCopy_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem3.Text = "粘贴";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(145, 6);
            // 
            // cmstbH2A
            // 
            this.cmstbH2A.Name = "cmstbH2A";
            this.cmstbH2A.Size = new System.Drawing.Size(148, 22);
            this.cmstbH2A.Text = "Hex2Ascii";
            this.cmstbH2A.Click += new System.EventHandler(this.cmstbH2A_Click);
            // 
            // cmstbA2H
            // 
            this.cmstbA2H.Name = "cmstbA2H";
            this.cmstbA2H.Size = new System.Drawing.Size(148, 22);
            this.cmstbA2H.Text = "Ascii2Hex";
            this.cmstbA2H.Click += new System.EventHandler(this.cmstbA2H_Click);
            // 
            // cmstbHexFormat
            // 
            this.cmstbHexFormat.Name = "cmstbHexFormat";
            this.cmstbHexFormat.Size = new System.Drawing.Size(148, 22);
            this.cmstbHexFormat.Text = "Hex格式化";
            this.cmstbHexFormat.Click += new System.EventHandler(this.cmstbHexFormat_Click);
            // 
            // cmstbCalcLength
            // 
            this.cmstbCalcLength.Name = "cmstbCalcLength";
            this.cmstbCalcLength.Size = new System.Drawing.Size(148, 22);
            this.cmstbCalcLength.Text = "计算字节长度";
            this.cmstbCalcLength.Click += new System.EventHandler(this.cmstbCalcLength_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(145, 6);
            // 
            // cmstbCheckSum
            // 
            this.cmstbCheckSum.Name = "cmstbCheckSum";
            this.cmstbCheckSum.Size = new System.Drawing.Size(148, 22);
            this.cmstbCheckSum.Text = "校验和";
            this.cmstbCheckSum.Click += new System.EventHandler(this.cmstbCheckSum_Click);
            // 
            // cmstbXor
            // 
            this.cmstbXor.Name = "cmstbXor";
            this.cmstbXor.Size = new System.Drawing.Size(148, 22);
            this.cmstbXor.Text = "异或";
            this.cmstbXor.Click += new System.EventHandler(this.cmstbXor_Click);
            // 
            // btnEditFile
            // 
            this.btnEditFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditFile.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEditFile.Location = new System.Drawing.Point(594, 99);
            this.btnEditFile.Name = "btnEditFile";
            this.btnEditFile.Size = new System.Drawing.Size(39, 23);
            this.btnEditFile.TabIndex = 16;
            this.btnEditFile.Text = "编辑";
            this.btnEditFile.UseVisualStyleBackColor = true;
            this.btnEditFile.Click += new System.EventHandler(this.btnEditFile_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadFile.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLoadFile.Location = new System.Drawing.Point(554, 99);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(39, 23);
            this.btnLoadFile.TabIndex = 15;
            this.btnLoadFile.Text = "载入";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // cmbLoadFile
            // 
            this.cmbLoadFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLoadFile.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLoadFile.FormattingEnabled = true;
            this.cmbLoadFile.Location = new System.Drawing.Point(3, 99);
            this.cmbLoadFile.Name = "cmbLoadFile";
            this.cmbLoadFile.Size = new System.Drawing.Size(548, 22);
            this.cmbLoadFile.TabIndex = 14;
            this.cmbLoadFile.TabStop = false;
            this.cmbLoadFile.SelectedIndexChanged += new System.EventHandler(this.cmbLoadFile_SelectedIndexChanged);
            // 
            // tbpQuick
            // 
            this.tbpQuick.AutoScroll = true;
            this.tbpQuick.AutoScrollMargin = new System.Drawing.Size(10, 0);
            this.tbpQuick.Location = new System.Drawing.Point(4, 22);
            this.tbpQuick.Name = "tbpQuick";
            this.tbpQuick.Padding = new System.Windows.Forms.Padding(3);
            this.tbpQuick.Size = new System.Drawing.Size(632, 124);
            this.tbpQuick.TabIndex = 1;
            this.tbpQuick.Text = "快捷";
            this.tbpQuick.UseVisualStyleBackColor = true;
            this.tbpQuick.Scroll += new System.Windows.Forms.ScrollEventHandler(this.tbpQuick_Scroll);
            this.tbpQuick.SizeChanged += new System.EventHandler(this.tbpQuick_SizeChanged);
            this.tbpQuick.Click += new System.EventHandler(this.tbpQuick_Click);
            this.tbpQuick.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.tbpQuick_MouseWheel);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFile,
            this.tsmTool,
            this.tsmHelp,
            this.tsmDonate,
            this.tsmClearSend,
            this.tsmClearRece});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmFile
            // 
            this.tsmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAddNote,
            this.tsmImportConfig,
            this.tsmExportConfig,
            this.tsmOpenLogPath,
            this.tsmOption,
            this.tsmExit});
            this.tsmFile.Image = global::O_ComTool_Pro.Properties.Resources.File_48px;
            this.tsmFile.Name = "tsmFile";
            this.tsmFile.Size = new System.Drawing.Size(74, 21);
            this.tsmFile.Text = "文件(&F)";
            // 
            // tsmAddNote
            // 
            this.tsmAddNote.Image = global::O_ComTool_Pro.Properties.Resources.Add_Tag_48px;
            this.tsmAddNote.Name = "tsmAddNote";
            this.tsmAddNote.Size = new System.Drawing.Size(148, 22);
            this.tsmAddNote.Text = "添加备注";
            this.tsmAddNote.Click += new System.EventHandler(this.tsmAddNote_Click);
            // 
            // tsmImportConfig
            // 
            this.tsmImportConfig.Image = global::O_ComTool_Pro.Properties.Resources.Import_48px;
            this.tsmImportConfig.Name = "tsmImportConfig";
            this.tsmImportConfig.Size = new System.Drawing.Size(148, 22);
            this.tsmImportConfig.Text = "导入配置文件";
            this.tsmImportConfig.Click += new System.EventHandler(this.tsmImportConfig_Click);
            // 
            // tsmExportConfig
            // 
            this.tsmExportConfig.Image = global::O_ComTool_Pro.Properties.Resources.Export_48px;
            this.tsmExportConfig.Name = "tsmExportConfig";
            this.tsmExportConfig.Size = new System.Drawing.Size(148, 22);
            this.tsmExportConfig.Text = "导出配置文件";
            this.tsmExportConfig.Click += new System.EventHandler(this.tsmExportConfig_Click);
            // 
            // tsmOpenLogPath
            // 
            this.tsmOpenLogPath.Image = global::O_ComTool_Pro.Properties.Resources.Log_Cabin_48px;
            this.tsmOpenLogPath.Name = "tsmOpenLogPath";
            this.tsmOpenLogPath.Size = new System.Drawing.Size(148, 22);
            this.tsmOpenLogPath.Text = "打开日志目录";
            this.tsmOpenLogPath.Click += new System.EventHandler(this.tsmOpenLogPath_Click);
            // 
            // tsmOption
            // 
            this.tsmOption.Image = global::O_ComTool_Pro.Properties.Resources.Settings_48px;
            this.tsmOption.Name = "tsmOption";
            this.tsmOption.Size = new System.Drawing.Size(148, 22);
            this.tsmOption.Text = "选项设置";
            this.tsmOption.Click += new System.EventHandler(this.tsmOption_Click);
            // 
            // tsmExit
            // 
            this.tsmExit.Image = global::O_ComTool_Pro.Properties.Resources.Exit_48px;
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(148, 22);
            this.tsmExit.Text = "退出";
            this.tsmExit.Click += new System.EventHandler(this.tsmExit_Click);
            // 
            // tsmTool
            // 
            this.tsmTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCheck,
            this.tsmCalc,
            this.tsmDevMgmt,
            this.tsmFormat,
            this.tsmAscii,
            this.tsmChangeDisplay});
            this.tsmTool.Image = global::O_ComTool_Pro.Properties.Resources.Tools_48px;
            this.tsmTool.Name = "tsmTool";
            this.tsmTool.Size = new System.Drawing.Size(75, 21);
            this.tsmTool.Text = "工具(&T)";
            // 
            // tsmCheck
            // 
            this.tsmCheck.Image = global::O_ComTool_Pro.Properties.Resources.Check_48px;
            this.tsmCheck.Name = "tsmCheck";
            this.tsmCheck.Size = new System.Drawing.Size(153, 22);
            this.tsmCheck.Text = "校验(&P)";
            this.tsmCheck.Click += new System.EventHandler(this.tsmCheck_Click);
            // 
            // tsmCalc
            // 
            this.tsmCalc.Image = global::O_ComTool_Pro.Properties.Resources.Calculator_48px;
            this.tsmCalc.Name = "tsmCalc";
            this.tsmCalc.Size = new System.Drawing.Size(153, 22);
            this.tsmCalc.Text = "计算器(&C)";
            this.tsmCalc.Click += new System.EventHandler(this.tsmCalc_Click);
            // 
            // tsmDevMgmt
            // 
            this.tsmDevMgmt.Image = global::O_ComTool_Pro.Properties.Resources.Device_Manager_48px;
            this.tsmDevMgmt.Name = "tsmDevMgmt";
            this.tsmDevMgmt.Size = new System.Drawing.Size(153, 22);
            this.tsmDevMgmt.Text = "设备管理器(&D)";
            this.tsmDevMgmt.Click += new System.EventHandler(this.tsmDevMgmt_Click);
            // 
            // tsmFormat
            // 
            this.tsmFormat.Image = global::O_ComTool_Pro.Properties.Resources.Format_48px;
            this.tsmFormat.Name = "tsmFormat";
            this.tsmFormat.Size = new System.Drawing.Size(153, 22);
            this.tsmFormat.Text = "报文格式化(&F)";
            this.tsmFormat.Click += new System.EventHandler(this.tsmFormat_Click);
            // 
            // tsmAscii
            // 
            this.tsmAscii.Image = global::O_ComTool_Pro.Properties.Resources.ASC_48px;
            this.tsmAscii.Name = "tsmAscii";
            this.tsmAscii.Size = new System.Drawing.Size(153, 22);
            this.tsmAscii.Text = "ASCII码表(&A)";
            this.tsmAscii.Click += new System.EventHandler(this.tsmAscii_Click);
            // 
            // tsmChangeDisplay
            // 
            this.tsmChangeDisplay.Image = global::O_ComTool_Pro.Properties.Resources.Change_Theme_48px;
            this.tsmChangeDisplay.Name = "tsmChangeDisplay";
            this.tsmChangeDisplay.Size = new System.Drawing.Size(153, 22);
            this.tsmChangeDisplay.Text = "显示切换(&D)";
            this.tsmChangeDisplay.Click += new System.EventHandler(this.tsmChangeDisplay_Click);
            // 
            // tsmHelp
            // 
            this.tsmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmHomePage,
            this.tsmUpdate,
            this.tsmAbout});
            this.tsmHelp.Image = global::O_ComTool_Pro.Properties.Resources.Help_48px;
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Size = new System.Drawing.Size(77, 21);
            this.tsmHelp.Text = "帮助(&H)";
            // 
            // tsmHomePage
            // 
            this.tsmHomePage.Image = global::O_ComTool_Pro.Properties.Resources.Home_48px;
            this.tsmHomePage.Name = "tsmHomePage";
            this.tsmHomePage.Size = new System.Drawing.Size(117, 22);
            this.tsmHomePage.Text = "主页(&H)";
            this.tsmHomePage.Click += new System.EventHandler(this.tsmHomePage_Click);
            // 
            // tsmUpdate
            // 
            this.tsmUpdate.Image = global::O_ComTool_Pro.Properties.Resources.Updates_48px;
            this.tsmUpdate.Name = "tsmUpdate";
            this.tsmUpdate.Size = new System.Drawing.Size(117, 22);
            this.tsmUpdate.Text = "更新(&U)";
            this.tsmUpdate.Click += new System.EventHandler(this.tsmUpdate_Click);
            // 
            // tsmAbout
            // 
            this.tsmAbout.Image = global::O_ComTool_Pro.Properties.Resources.About_48px;
            this.tsmAbout.Name = "tsmAbout";
            this.tsmAbout.Size = new System.Drawing.Size(117, 22);
            this.tsmAbout.Text = "关于(&A)";
            this.tsmAbout.Click += new System.EventHandler(this.tsmAbout_Click);
            // 
            // tsmDonate
            // 
            this.tsmDonate.Image = global::O_ComTool_Pro.Properties.Resources.Donate_48px;
            this.tsmDonate.Name = "tsmDonate";
            this.tsmDonate.Size = new System.Drawing.Size(77, 21);
            this.tsmDonate.Text = "捐赠(&D)";
            this.tsmDonate.Click += new System.EventHandler(this.tsmDonate_Click);
            // 
            // tsmClearSend
            // 
            this.tsmClearSend.Image = global::O_ComTool_Pro.Properties.Resources.Send_Clear_48px;
            this.tsmClearSend.Name = "tsmClearSend";
            this.tsmClearSend.Size = new System.Drawing.Size(84, 21);
            this.tsmClearSend.Text = "清空发送";
            this.tsmClearSend.Visible = false;
            this.tsmClearSend.Click += new System.EventHandler(this.tsmClearSend_Click);
            // 
            // tsmClearRece
            // 
            this.tsmClearRece.Image = global::O_ComTool_Pro.Properties.Resources.Rec_Clear_48px;
            this.tsmClearRece.Name = "tsmClearRece";
            this.tsmClearRece.Size = new System.Drawing.Size(84, 21);
            this.tsmClearRece.Text = "清空接收";
            this.tsmClearRece.Visible = false;
            this.tsmClearRece.Click += new System.EventHandler(this.tsmClearRece_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssLabCom,
            this.tssProgressBar,
            this.tssLabReset,
            this.tssImageChange,
            this.tssRxCount,
            this.tssTxCount,
            this.tssLabRate,
            this.tssLabRateValue,
            this.toolStripStatusLabel4,
            this.tssCurstatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 574);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssLabCom
            // 
            this.tssLabCom.ForeColor = System.Drawing.Color.Red;
            this.tssLabCom.Name = "tssLabCom";
            this.tssLabCom.Size = new System.Drawing.Size(91, 17);
            this.tssLabCom.Text = "COMx: Closed";
            this.tssLabCom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tssProgressBar
            // 
            this.tssProgressBar.Name = "tssProgressBar";
            this.tssProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // tssLabReset
            // 
            this.tssLabReset.ForeColor = System.Drawing.Color.Blue;
            this.tssLabReset.IsLink = true;
            this.tssLabReset.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.tssLabReset.Name = "tssLabReset";
            this.tssLabReset.Size = new System.Drawing.Size(64, 17);
            this.tssLabReset.Text = "  重置计数";
            this.tssLabReset.Click += new System.EventHandler(this.tssLabReset_Click);
            // 
            // tssImageChange
            // 
            this.tssImageChange.Image = global::O_ComTool_Pro.Properties.Resources.Replace_48px;
            this.tssImageChange.IsLink = true;
            this.tssImageChange.Name = "tssImageChange";
            this.tssImageChange.Size = new System.Drawing.Size(16, 17);
            this.tssImageChange.Click += new System.EventHandler(this.tssImageChange_Click);
            this.tssImageChange.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tssImageChange_MouseDown);
            this.tssImageChange.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tssImageChange_MouseUp);
            // 
            // tssRxCount
            // 
            this.tssRxCount.Name = "tssRxCount";
            this.tssRxCount.Size = new System.Drawing.Size(82, 17);
            this.tssRxCount.Text = "RX: 0 frames";
            // 
            // tssTxCount
            // 
            this.tssTxCount.Name = "tssTxCount";
            this.tssTxCount.Size = new System.Drawing.Size(81, 17);
            this.tssTxCount.Text = "TX: 0 frames";
            // 
            // tssLabRate
            // 
            this.tssLabRate.Name = "tssLabRate";
            this.tssLabRate.Size = new System.Drawing.Size(47, 17);
            this.tssLabRate.Text = "收发比:";
            // 
            // tssLabRateValue
            // 
            this.tssLabRateValue.Name = "tssLabRateValue";
            this.tssLabRateValue.Size = new System.Drawing.Size(39, 17);
            this.tssLabRateValue.Text = "NULL";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.IsLink = true;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(0, 17);
            // 
            // tssCurstatus
            // 
            this.tssCurstatus.Name = "tssCurstatus";
            this.tssCurstatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tssCurstatus.Size = new System.Drawing.Size(247, 17);
            this.tssCurstatus.Spring = true;
            this.tssCurstatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // llabAd
            // 
            this.llabAd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.llabAd.AutoEllipsis = true;
            this.llabAd.BackColor = System.Drawing.Color.White;
            this.llabAd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.llabAd.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llabAd.Location = new System.Drawing.Point(483, 6);
            this.llabAd.Name = "llabAd";
            this.llabAd.Size = new System.Drawing.Size(300, 15);
            this.llabAd.TabIndex = 20;
            this.llabAd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.llabAd.VisitedLinkColor = System.Drawing.Color.Blue;
            this.llabAd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llabAd_LinkClicked);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // timerAutoReply
            // 
            this.timerAutoReply.Tick += new System.EventHandler(this.timerAutoReply_Tick);
            // 
            // timerRepeat
            // 
            this.timerRepeat.Tick += new System.EventHandler(this.timerRepeat_Tick);
            // 
            // timerReceiveLed
            // 
            this.timerReceiveLed.Tick += new System.EventHandler(this.timerReceiveLed_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "O-ComTool";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsTitle,
            this.toolStripSeparator1,
            this.cmsAbout,
            this.cmsOption,
            this.cmsExit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 98);
            // 
            // cmsTitle
            // 
            this.cmsTitle.Image = global::O_ComTool_Pro.Properties.Resources.Logo;
            this.cmsTitle.Name = "cmsTitle";
            this.cmsTitle.Size = new System.Drawing.Size(144, 22);
            this.cmsTitle.Text = "O-ComTool";
            this.cmsTitle.Click += new System.EventHandler(this.cmsTitle_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(141, 6);
            // 
            // cmsAbout
            // 
            this.cmsAbout.Image = global::O_ComTool_Pro.Properties.Resources.About_48px;
            this.cmsAbout.Name = "cmsAbout";
            this.cmsAbout.Size = new System.Drawing.Size(144, 22);
            this.cmsAbout.Text = "关于";
            this.cmsAbout.Click += new System.EventHandler(this.cmsAbout_Click);
            // 
            // cmsOption
            // 
            this.cmsOption.Image = global::O_ComTool_Pro.Properties.Resources.Settings_48px;
            this.cmsOption.Name = "cmsOption";
            this.cmsOption.Size = new System.Drawing.Size(144, 22);
            this.cmsOption.Text = "选项";
            this.cmsOption.Click += new System.EventHandler(this.cmsOption_Click);
            // 
            // cmsExit
            // 
            this.cmsExit.Image = global::O_ComTool_Pro.Properties.Resources.Exit_48px;
            this.cmsExit.Name = "cmsExit";
            this.cmsExit.Size = new System.Drawing.Size(144, 22);
            this.cmsExit.Text = "退出";
            this.cmsExit.Click += new System.EventHandler(this.cmsExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(784, 596);
            this.Controls.Add(this.llabAd);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "O-ComTool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gpdSend.ResumeLayout(false);
            this.gpdSend.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepeatInterval)).EndInit();
            this.gpdReceive.ResumeLayout(false);
            this.gpdReceive.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReceiveLed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReplyDelay)).EndInit();
            this.gpdCom.ResumeLayout(false);
            this.gpdCom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picConnectStatus)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fctbReceive)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picAddQuickSend)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.tbpGeneral.PerformLayout();
            this.contextMenuStrip3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gpdSend;
        private System.Windows.Forms.CheckBox chkNewLine;
        private System.Windows.Forms.Label labRepeatInterval;
        private System.Windows.Forms.Button btnClearSend;
        private System.Windows.Forms.CheckBox chkRepeatSend;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudRepeatInterval;
        private System.Windows.Forms.CheckBox chkAutoCount;
        private System.Windows.Forms.RadioButton radHexSend;
        private System.Windows.Forms.RadioButton radAsciiSend;
        private System.Windows.Forms.GroupBox gpdReceive;
        private System.Windows.Forms.Button btnClearReceive;
        private System.Windows.Forms.CheckBox chkAutoSave;
        private System.Windows.Forms.CheckBox chkShowTime;
        private System.Windows.Forms.CheckBox chkAutoLine;
        private System.Windows.Forms.RadioButton radHexReceive;
        private System.Windows.Forms.RadioButton radAsciiReceive;
        private System.Windows.Forms.GroupBox gpdCom;
        private System.Windows.Forms.ComboBox cmbFlowCtrl;
        private System.Windows.Forms.Label labFlowCtrl;
        private System.Windows.Forms.PictureBox picConnectStatus;
        private System.Windows.Forms.Button btnOpenCom;
        private System.Windows.Forms.ComboBox cmbStopBit;
        private System.Windows.Forms.ComboBox cmbParityBit;
        private System.Windows.Forms.ComboBox cmbDataBit;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.ComboBox cmbCom;
        private System.Windows.Forms.Label labStopBit;
        private System.Windows.Forms.Label labParityBit;
        private System.Windows.Forms.Label labDataBit;
        private System.Windows.Forms.Label labBaudRate;
        private System.Windows.Forms.Label labPort;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmFile;
        private System.Windows.Forms.ToolStripMenuItem tsmImportConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmExportConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmOpenLogPath;
        private System.Windows.Forms.ToolStripMenuItem tsmTool;
        private System.Windows.Forms.ToolStripMenuItem tsmCheck;
        private System.Windows.Forms.ToolStripMenuItem tsmCalc;
        private System.Windows.Forms.ToolStripMenuItem tsmDevMgmt;
        private System.Windows.Forms.ToolStripMenuItem tsmFormat;
        private System.Windows.Forms.ToolStripMenuItem tsmChangeDisplay;
        private System.Windows.Forms.ToolStripMenuItem tsmHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.ToolStripMenuItem tsmAscii;
        private System.Windows.Forms.ToolStripMenuItem tsmUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmDonate;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpGeneral;
        private System.Windows.Forms.TabPage tbpQuick;
        private System.Windows.Forms.Button btnEditFile;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.ComboBox cmbLoadFile;
        private System.Windows.Forms.TextBox txbSend;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssLabCom;
        private System.Windows.Forms.ToolStripProgressBar tssProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel tssLabReset;
        private System.Windows.Forms.ToolStripStatusLabel tssImageChange;
        private System.Windows.Forms.ToolStripStatusLabel tssRxCount;
        private System.Windows.Forms.ToolStripStatusLabel tssTxCount;
        private System.Windows.Forms.ToolStripStatusLabel tssLabRate;
        private System.Windows.Forms.ToolStripStatusLabel tssLabRateValue;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel tssCurstatus;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ToolStripMenuItem tsmHomePage;
        private System.Windows.Forms.ToolStripMenuItem tsmClearSend;
        private System.Windows.Forms.ToolStripMenuItem tsmClearRece;
        private System.Windows.Forms.LinkLabel llabAd;
        private System.Windows.Forms.ToolStripMenuItem tsmOption;
        private System.Windows.Forms.ToolStripMenuItem tsmAddNote;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timerAutoReply;
        private System.Windows.Forms.Timer timerRepeat;
        private System.Windows.Forms.Timer timerProcessBar;
        private System.Windows.Forms.Timer timerReceiveLed;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox picAddQuickSend;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmsTitle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cmsAbout;
        private System.Windows.Forms.ToolStripMenuItem cmsOption;
        private System.Windows.Forms.ToolStripMenuItem cmsExit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem cmsSelectAll;
        private System.Windows.Forms.ToolStripMenuItem cmsCopy;
        private System.Windows.Forms.ToolStripMenuItem cmsPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem cmsH2A;
        private System.Windows.Forms.ToolStripMenuItem cmsA2H;
        private System.Windows.Forms.ToolStripMenuItem cmsHexFormat;
        private System.Windows.Forms.ToolStripMenuItem cmsCalcLength;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cmsCheckSum;
        private System.Windows.Forms.ToolStripMenuItem cmsXor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem cmsSaveCur;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem cmstbSelectAll;
        private System.Windows.Forms.ToolStripMenuItem cmstbCopy;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem cmstbH2A;
        private System.Windows.Forms.ToolStripMenuItem cmstbA2H;
        private System.Windows.Forms.ToolStripMenuItem cmstbHexFormat;
        private System.Windows.Forms.ToolStripMenuItem cmstbCalcLength;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem cmstbCheckSum;
        private System.Windows.Forms.ToolStripMenuItem cmstbXor;
        private FastColoredTextBoxNS.FastColoredTextBox fctbReceive;
        private O_ScrollBar o_ScrollBar1;
        private System.Windows.Forms.PictureBox picReceiveLed;
        private System.Windows.Forms.CheckBox chkAutoReply;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labReplyDelay;
        private System.Windows.Forms.NumericUpDown nudReplyDelay;
    }
}

