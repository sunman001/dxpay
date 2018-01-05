namespace JMNOTICE
{
    partial class FrmNoticeMonitor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.gbBottonDisplay = new System.Windows.Forms.GroupBox();
            this.lbl_GlobalInfo = new System.Windows.Forms.Label();
            this.gbCenterConfig = new System.Windows.Forms.GroupBox();
            this.txt_CLIENT_ID = new System.Windows.Forms.TextBox();
            this.lb_CLIENT_ID = new System.Windows.Forms.Label();
            this.lbl_EACH_TIME_SELECT_TOP_COUNT = new System.Windows.Forms.Label();
            this.lbl_MAX_THREAD_COUNT = new System.Windows.Forms.Label();
            this.txt_ENCRYPT_IP_ADDRESS = new System.Windows.Forms.TextBox();
            this.txt_MAX_THREAD_COUNT = new System.Windows.Forms.TextBox();
            this.lbl_ENCRYPT_IP_ADDRESS = new System.Windows.Forms.Label();
            this.txt_EACH_TIME_SELECT_TOP_COUNT = new System.Windows.Forms.TextBox();
            this.txt_CLEAR_LOG_THRESHOLD = new System.Windows.Forms.TextBox();
            this.lbl_LOOP_LOAD_DATA_THRESHOLD = new System.Windows.Forms.Label();
            this.lbl_CLEAR_LOG_THRESHOLD = new System.Windows.Forms.Label();
            this.txt_TASK_CRONS_CONFIG_STRING = new System.Windows.Forms.TextBox();
            this.txt_LOOP_LOAD_DATA_THRESHOLD = new System.Windows.Forms.TextBox();
            this.lbl_TASK_CRONS_CONFIG_STRING = new System.Windows.Forms.Label();
            this.gbTopButton = new System.Windows.Forms.GroupBox();
            this.btnStartOrderNotifyJob = new System.Windows.Forms.Button();
            this.btnSaveLog = new System.Windows.Forms.Button();
            this.btnStopOrderNotifyJob = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnApplyGlobalConfig = new System.Windows.Forms.Button();
            this.btnSaveGlobalConfig = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLeft = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusRight = new System.Windows.Forms.ToolStripStatusLabel();
            this.lstLog = new System.Windows.Forms.RichTextBox();
            this.timerAppStartTimer = new System.Windows.Forms.Timer(this.components);
            this.chkNEED_CHECK_FROM_DATABASE = new System.Windows.Forms.CheckBox();
            this.panelLeft.SuspendLayout();
            this.gbBottonDisplay.SuspendLayout();
            this.gbCenterConfig.SuspendLayout();
            this.gbTopButton.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLeft.Controls.Add(this.gbBottonDisplay);
            this.panelLeft.Controls.Add(this.gbCenterConfig);
            this.panelLeft.Controls.Add(this.gbTopButton);
            this.panelLeft.Location = new System.Drawing.Point(2, 2);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(317, 574);
            this.panelLeft.TabIndex = 1;
            // 
            // gbBottonDisplay
            // 
            this.gbBottonDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBottonDisplay.Controls.Add(this.lbl_GlobalInfo);
            this.gbBottonDisplay.Location = new System.Drawing.Point(3, 444);
            this.gbBottonDisplay.Name = "gbBottonDisplay";
            this.gbBottonDisplay.Size = new System.Drawing.Size(309, 125);
            this.gbBottonDisplay.TabIndex = 20;
            this.gbBottonDisplay.TabStop = false;
            this.gbBottonDisplay.Text = "信息显示";
            // 
            // lbl_GlobalInfo
            // 
            this.lbl_GlobalInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_GlobalInfo.Location = new System.Drawing.Point(9, 23);
            this.lbl_GlobalInfo.Name = "lbl_GlobalInfo";
            this.lbl_GlobalInfo.Size = new System.Drawing.Size(294, 85);
            this.lbl_GlobalInfo.TabIndex = 0;
            // 
            // gbCenterConfig
            // 
            this.gbCenterConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCenterConfig.Controls.Add(this.chkNEED_CHECK_FROM_DATABASE);
            this.gbCenterConfig.Controls.Add(this.txt_CLIENT_ID);
            this.gbCenterConfig.Controls.Add(this.lb_CLIENT_ID);
            this.gbCenterConfig.Controls.Add(this.lbl_EACH_TIME_SELECT_TOP_COUNT);
            this.gbCenterConfig.Controls.Add(this.lbl_MAX_THREAD_COUNT);
            this.gbCenterConfig.Controls.Add(this.txt_ENCRYPT_IP_ADDRESS);
            this.gbCenterConfig.Controls.Add(this.txt_MAX_THREAD_COUNT);
            this.gbCenterConfig.Controls.Add(this.lbl_ENCRYPT_IP_ADDRESS);
            this.gbCenterConfig.Controls.Add(this.txt_EACH_TIME_SELECT_TOP_COUNT);
            this.gbCenterConfig.Controls.Add(this.txt_CLEAR_LOG_THRESHOLD);
            this.gbCenterConfig.Controls.Add(this.lbl_LOOP_LOAD_DATA_THRESHOLD);
            this.gbCenterConfig.Controls.Add(this.lbl_CLEAR_LOG_THRESHOLD);
            this.gbCenterConfig.Controls.Add(this.txt_TASK_CRONS_CONFIG_STRING);
            this.gbCenterConfig.Controls.Add(this.txt_LOOP_LOAD_DATA_THRESHOLD);
            this.gbCenterConfig.Controls.Add(this.lbl_TASK_CRONS_CONFIG_STRING);
            this.gbCenterConfig.Location = new System.Drawing.Point(4, 125);
            this.gbCenterConfig.Name = "gbCenterConfig";
            this.gbCenterConfig.Size = new System.Drawing.Size(308, 313);
            this.gbCenterConfig.TabIndex = 19;
            this.gbCenterConfig.TabStop = false;
            this.gbCenterConfig.Text = "配置选项区";
            // 
            // txt_CLIENT_ID
            // 
            this.txt_CLIENT_ID.Location = new System.Drawing.Point(233, 188);
            this.txt_CLIENT_ID.Name = "txt_CLIENT_ID";
            this.txt_CLIENT_ID.Size = new System.Drawing.Size(68, 20);
            this.txt_CLIENT_ID.TabIndex = 18;
            this.txt_CLIENT_ID.Text = "tz220";
            // 
            // lb_CLIENT_ID
            // 
            this.lb_CLIENT_ID.AutoSize = true;
            this.lb_CLIENT_ID.Location = new System.Drawing.Point(6, 194);
            this.lb_CLIENT_ID.Name = "lb_CLIENT_ID";
            this.lb_CLIENT_ID.Size = new System.Drawing.Size(180, 13);
            this.lb_CLIENT_ID.TabIndex = 17;
            this.lb_CLIENT_ID.Text = "客户端ID(不同服务器不同的标识)";
            // 
            // lbl_EACH_TIME_SELECT_TOP_COUNT
            // 
            this.lbl_EACH_TIME_SELECT_TOP_COUNT.AutoSize = true;
            this.lbl_EACH_TIME_SELECT_TOP_COUNT.Location = new System.Drawing.Point(6, 59);
            this.lbl_EACH_TIME_SELECT_TOP_COUNT.Name = "lbl_EACH_TIME_SELECT_TOP_COUNT";
            this.lbl_EACH_TIME_SELECT_TOP_COUNT.Size = new System.Drawing.Size(214, 13);
            this.lbl_EACH_TIME_SELECT_TOP_COUNT.TabIndex = 5;
            this.lbl_EACH_TIME_SELECT_TOP_COUNT.Text = "每次从数据库加载的数据行数(默认值:6)";
            // 
            // lbl_MAX_THREAD_COUNT
            // 
            this.lbl_MAX_THREAD_COUNT.AutoSize = true;
            this.lbl_MAX_THREAD_COUNT.Location = new System.Drawing.Point(6, 22);
            this.lbl_MAX_THREAD_COUNT.Name = "lbl_MAX_THREAD_COUNT";
            this.lbl_MAX_THREAD_COUNT.Size = new System.Drawing.Size(142, 13);
            this.lbl_MAX_THREAD_COUNT.TabIndex = 3;
            this.lbl_MAX_THREAD_COUNT.Text = "最大并行线程数(默认值:5)";
            // 
            // txt_ENCRYPT_IP_ADDRESS
            // 
            this.txt_ENCRYPT_IP_ADDRESS.Enabled = false;
            this.txt_ENCRYPT_IP_ADDRESS.Location = new System.Drawing.Point(123, 222);
            this.txt_ENCRYPT_IP_ADDRESS.Name = "txt_ENCRYPT_IP_ADDRESS";
            this.txt_ENCRYPT_IP_ADDRESS.Size = new System.Drawing.Size(178, 20);
            this.txt_ENCRYPT_IP_ADDRESS.TabIndex = 16;
            // 
            // txt_MAX_THREAD_COUNT
            // 
            this.txt_MAX_THREAD_COUNT.Location = new System.Drawing.Point(161, 15);
            this.txt_MAX_THREAD_COUNT.Name = "txt_MAX_THREAD_COUNT";
            this.txt_MAX_THREAD_COUNT.Size = new System.Drawing.Size(140, 20);
            this.txt_MAX_THREAD_COUNT.TabIndex = 4;
            this.txt_MAX_THREAD_COUNT.Text = "5";
            // 
            // lbl_ENCRYPT_IP_ADDRESS
            // 
            this.lbl_ENCRYPT_IP_ADDRESS.AutoSize = true;
            this.lbl_ENCRYPT_IP_ADDRESS.Location = new System.Drawing.Point(6, 228);
            this.lbl_ENCRYPT_IP_ADDRESS.Name = "lbl_ENCRYPT_IP_ADDRESS";
            this.lbl_ENCRYPT_IP_ADDRESS.Size = new System.Drawing.Size(101, 13);
            this.lbl_ENCRYPT_IP_ADDRESS.TabIndex = 15;
            this.lbl_ENCRYPT_IP_ADDRESS.Text = "加密服务器IP地址";
            // 
            // txt_EACH_TIME_SELECT_TOP_COUNT
            // 
            this.txt_EACH_TIME_SELECT_TOP_COUNT.Location = new System.Drawing.Point(233, 52);
            this.txt_EACH_TIME_SELECT_TOP_COUNT.Name = "txt_EACH_TIME_SELECT_TOP_COUNT";
            this.txt_EACH_TIME_SELECT_TOP_COUNT.Size = new System.Drawing.Size(68, 20);
            this.txt_EACH_TIME_SELECT_TOP_COUNT.TabIndex = 6;
            this.txt_EACH_TIME_SELECT_TOP_COUNT.Text = "6";
            // 
            // txt_CLEAR_LOG_THRESHOLD
            // 
            this.txt_CLEAR_LOG_THRESHOLD.Location = new System.Drawing.Point(233, 126);
            this.txt_CLEAR_LOG_THRESHOLD.Name = "txt_CLEAR_LOG_THRESHOLD";
            this.txt_CLEAR_LOG_THRESHOLD.Size = new System.Drawing.Size(68, 20);
            this.txt_CLEAR_LOG_THRESHOLD.TabIndex = 14;
            this.txt_CLEAR_LOG_THRESHOLD.Text = "2000";
            // 
            // lbl_LOOP_LOAD_DATA_THRESHOLD
            // 
            this.lbl_LOOP_LOAD_DATA_THRESHOLD.AutoSize = true;
            this.lbl_LOOP_LOAD_DATA_THRESHOLD.Location = new System.Drawing.Point(6, 95);
            this.lbl_LOOP_LOAD_DATA_THRESHOLD.Name = "lbl_LOOP_LOAD_DATA_THRESHOLD";
            this.lbl_LOOP_LOAD_DATA_THRESHOLD.Size = new System.Drawing.Size(214, 13);
            this.lbl_LOOP_LOAD_DATA_THRESHOLD.TabIndex = 9;
            this.lbl_LOOP_LOAD_DATA_THRESHOLD.Text = "循环加载数据最大间隔阀值(默认值:5秒)";
            // 
            // lbl_CLEAR_LOG_THRESHOLD
            // 
            this.lbl_CLEAR_LOG_THRESHOLD.AutoSize = true;
            this.lbl_CLEAR_LOG_THRESHOLD.Location = new System.Drawing.Point(6, 132);
            this.lbl_CLEAR_LOG_THRESHOLD.Name = "lbl_CLEAR_LOG_THRESHOLD";
            this.lbl_CLEAR_LOG_THRESHOLD.Size = new System.Drawing.Size(172, 13);
            this.lbl_CLEAR_LOG_THRESHOLD.TabIndex = 13;
            this.lbl_CLEAR_LOG_THRESHOLD.Text = "日志最大显示行数(默认值:2000)";
            // 
            // txt_TASK_CRONS_CONFIG_STRING
            // 
            this.txt_TASK_CRONS_CONFIG_STRING.Location = new System.Drawing.Point(8, 282);
            this.txt_TASK_CRONS_CONFIG_STRING.Name = "txt_TASK_CRONS_CONFIG_STRING";
            this.txt_TASK_CRONS_CONFIG_STRING.Size = new System.Drawing.Size(293, 20);
            this.txt_TASK_CRONS_CONFIG_STRING.TabIndex = 12;
            // 
            // txt_LOOP_LOAD_DATA_THRESHOLD
            // 
            this.txt_LOOP_LOAD_DATA_THRESHOLD.Location = new System.Drawing.Point(233, 89);
            this.txt_LOOP_LOAD_DATA_THRESHOLD.Name = "txt_LOOP_LOAD_DATA_THRESHOLD";
            this.txt_LOOP_LOAD_DATA_THRESHOLD.Size = new System.Drawing.Size(68, 20);
            this.txt_LOOP_LOAD_DATA_THRESHOLD.TabIndex = 10;
            this.txt_LOOP_LOAD_DATA_THRESHOLD.Text = "5";
            // 
            // lbl_TASK_CRONS_CONFIG_STRING
            // 
            this.lbl_TASK_CRONS_CONFIG_STRING.AutoSize = true;
            this.lbl_TASK_CRONS_CONFIG_STRING.Location = new System.Drawing.Point(6, 262);
            this.lbl_TASK_CRONS_CONFIG_STRING.Name = "lbl_TASK_CRONS_CONFIG_STRING";
            this.lbl_TASK_CRONS_CONFIG_STRING.Size = new System.Drawing.Size(229, 13);
            this.lbl_TASK_CRONS_CONFIG_STRING.TabIndex = 11;
            this.lbl_TASK_CRONS_CONFIG_STRING.Text = "需要开启任务的配置字符串(重启作业生效)";
            // 
            // gbTopButton
            // 
            this.gbTopButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTopButton.Controls.Add(this.btnStartOrderNotifyJob);
            this.gbTopButton.Controls.Add(this.btnSaveLog);
            this.gbTopButton.Controls.Add(this.btnStopOrderNotifyJob);
            this.gbTopButton.Controls.Add(this.btnClearLog);
            this.gbTopButton.Controls.Add(this.btnApplyGlobalConfig);
            this.gbTopButton.Controls.Add(this.btnSaveGlobalConfig);
            this.gbTopButton.Location = new System.Drawing.Point(3, 3);
            this.gbTopButton.Name = "gbTopButton";
            this.gbTopButton.Size = new System.Drawing.Size(309, 114);
            this.gbTopButton.TabIndex = 18;
            this.gbTopButton.TabStop = false;
            this.gbTopButton.Text = "操作区";
            // 
            // btnStartOrderNotifyJob
            // 
            this.btnStartOrderNotifyJob.Location = new System.Drawing.Point(6, 20);
            this.btnStartOrderNotifyJob.Name = "btnStartOrderNotifyJob";
            this.btnStartOrderNotifyJob.Size = new System.Drawing.Size(144, 39);
            this.btnStartOrderNotifyJob.TabIndex = 0;
            this.btnStartOrderNotifyJob.Text = "开启订单通知作业";
            this.btnStartOrderNotifyJob.UseVisualStyleBackColor = true;
            this.btnStartOrderNotifyJob.Click += new System.EventHandler(this.btnStartOrderNotifyJob_Click);
            // 
            // btnSaveLog
            // 
            this.btnSaveLog.Location = new System.Drawing.Point(237, 69);
            this.btnSaveLog.Name = "btnSaveLog";
            this.btnSaveLog.Size = new System.Drawing.Size(67, 31);
            this.btnSaveLog.TabIndex = 17;
            this.btnSaveLog.Text = "保存日志";
            this.btnSaveLog.UseVisualStyleBackColor = true;
            this.btnSaveLog.Click += new System.EventHandler(this.btnSaveLog_Click);
            // 
            // btnStopOrderNotifyJob
            // 
            this.btnStopOrderNotifyJob.Enabled = false;
            this.btnStopOrderNotifyJob.Location = new System.Drawing.Point(160, 20);
            this.btnStopOrderNotifyJob.Name = "btnStopOrderNotifyJob";
            this.btnStopOrderNotifyJob.Size = new System.Drawing.Size(144, 39);
            this.btnStopOrderNotifyJob.TabIndex = 1;
            this.btnStopOrderNotifyJob.Text = "停止订单通知作业";
            this.btnStopOrderNotifyJob.UseVisualStyleBackColor = true;
            this.btnStopOrderNotifyJob.Click += new System.EventHandler(this.btnStopOrderNotifyJob_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(159, 69);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(67, 31);
            this.btnClearLog.TabIndex = 2;
            this.btnClearLog.Text = "清空日志";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // btnApplyGlobalConfig
            // 
            this.btnApplyGlobalConfig.Location = new System.Drawing.Point(6, 69);
            this.btnApplyGlobalConfig.Name = "btnApplyGlobalConfig";
            this.btnApplyGlobalConfig.Size = new System.Drawing.Size(68, 31);
            this.btnApplyGlobalConfig.TabIndex = 7;
            this.btnApplyGlobalConfig.Text = "应用配置";
            this.btnApplyGlobalConfig.UseVisualStyleBackColor = true;
            this.btnApplyGlobalConfig.Click += new System.EventHandler(this.btnApplyGlobalConfig_Click);
            // 
            // btnSaveGlobalConfig
            // 
            this.btnSaveGlobalConfig.Location = new System.Drawing.Point(82, 69);
            this.btnSaveGlobalConfig.Name = "btnSaveGlobalConfig";
            this.btnSaveGlobalConfig.Size = new System.Drawing.Size(68, 31);
            this.btnSaveGlobalConfig.TabIndex = 8;
            this.btnSaveGlobalConfig.Text = "保存配置";
            this.btnSaveGlobalConfig.UseVisualStyleBackColor = true;
            this.btnSaveGlobalConfig.Click += new System.EventHandler(this.btnSaveGlobalConfig_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLeft,
            this.statusSpace,
            this.statusRight});
            this.statusStrip1.Location = new System.Drawing.Point(0, 580);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1048, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLeft
            // 
            this.statusLeft.Name = "statusLeft";
            this.statusLeft.Size = new System.Drawing.Size(46, 17);
            this.statusLeft.Text = "已就绪";
            // 
            // statusSpace
            // 
            this.statusSpace.ActiveLinkColor = System.Drawing.Color.Red;
            this.statusSpace.Name = "statusSpace";
            this.statusSpace.Size = new System.Drawing.Size(915, 17);
            this.statusSpace.Spring = true;
            // 
            // statusRight
            // 
            this.statusRight.Name = "statusRight";
            this.statusRight.Size = new System.Drawing.Size(72, 17);
            this.statusRight.Text = "程序已启动";
            // 
            // lstLog
            // 
            this.lstLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstLog.Location = new System.Drawing.Point(326, 2);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(722, 574);
            this.lstLog.TabIndex = 3;
            this.lstLog.Text = "";
            // 
            // timerAppStartTimer
            // 
            this.timerAppStartTimer.Enabled = true;
            this.timerAppStartTimer.Interval = 1000;
            this.timerAppStartTimer.Tick += new System.EventHandler(this.timerAppStartTimer_Tick);
            // 
            // chkNEED_CHECK_FROM_DATABASE
            // 
            this.chkNEED_CHECK_FROM_DATABASE.AutoSize = true;
            this.chkNEED_CHECK_FROM_DATABASE.Location = new System.Drawing.Point(9, 161);
            this.chkNEED_CHECK_FROM_DATABASE.Name = "chkNEED_CHECK_FROM_DATABASE";
            this.chkNEED_CHECK_FROM_DATABASE.Size = new System.Drawing.Size(146, 17);
            this.chkNEED_CHECK_FROM_DATABASE.TabIndex = 20;
            this.chkNEED_CHECK_FROM_DATABASE.Text = "是否需要判断启用标识";
            this.chkNEED_CHECK_FROM_DATABASE.UseVisualStyleBackColor = true;
            // 
            // FrmNoticeMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 602);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelLeft);
            this.Name = "FrmNoticeMonitor";
            this.Text = "订单通知程序";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmNoticeMonitor_FormClosing);
            this.Load += new System.EventHandler(this.FrmNoticeMonitor_Load);
            this.panelLeft.ResumeLayout(false);
            this.gbBottonDisplay.ResumeLayout(false);
            this.gbCenterConfig.ResumeLayout(false);
            this.gbCenterConfig.PerformLayout();
            this.gbTopButton.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Button btnStopOrderNotifyJob;
        private System.Windows.Forms.Button btnStartOrderNotifyJob;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLeft;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.TextBox txt_MAX_THREAD_COUNT;
        private System.Windows.Forms.Label lbl_MAX_THREAD_COUNT;
        private System.Windows.Forms.TextBox txt_EACH_TIME_SELECT_TOP_COUNT;
        private System.Windows.Forms.Label lbl_EACH_TIME_SELECT_TOP_COUNT;
        private System.Windows.Forms.Button btnApplyGlobalConfig;
        private System.Windows.Forms.Button btnSaveGlobalConfig;
        private System.Windows.Forms.TextBox txt_LOOP_LOAD_DATA_THRESHOLD;
        private System.Windows.Forms.Label lbl_LOOP_LOAD_DATA_THRESHOLD;
        private System.Windows.Forms.TextBox txt_TASK_CRONS_CONFIG_STRING;
        private System.Windows.Forms.Label lbl_TASK_CRONS_CONFIG_STRING;
        private System.Windows.Forms.TextBox txt_CLEAR_LOG_THRESHOLD;
        private System.Windows.Forms.Label lbl_CLEAR_LOG_THRESHOLD;
        private System.Windows.Forms.TextBox txt_ENCRYPT_IP_ADDRESS;
        private System.Windows.Forms.Label lbl_ENCRYPT_IP_ADDRESS;
        private System.Windows.Forms.RichTextBox lstLog;
        private System.Windows.Forms.Button btnSaveLog;
        private System.Windows.Forms.ToolStripStatusLabel statusSpace;
        private System.Windows.Forms.ToolStripStatusLabel statusRight;
        private System.Windows.Forms.Timer timerAppStartTimer;
        private System.Windows.Forms.GroupBox gbTopButton;
        private System.Windows.Forms.GroupBox gbCenterConfig;
        private System.Windows.Forms.GroupBox gbBottonDisplay;
        private System.Windows.Forms.Label lbl_GlobalInfo;
        private System.Windows.Forms.TextBox txt_CLIENT_ID;
        private System.Windows.Forms.Label lb_CLIENT_ID;
        private System.Windows.Forms.CheckBox chkNEED_CHECK_FROM_DATABASE;
    }
}