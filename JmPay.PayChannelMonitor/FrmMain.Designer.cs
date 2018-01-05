namespace JmPay.PayChannelMonitor
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbtnReloadScheduleGroupData = new System.Windows.Forms.ToolStripButton();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.gboxLeftButton = new System.Windows.Forms.GroupBox();
            this.btnStopAllSchedules = new System.Windows.Forms.Button();
            this.btnStartAllSchedules = new System.Windows.Forms.Button();
            this.gboxLeftOption = new System.Windows.Forms.GroupBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.lbl_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE = new System.Windows.Forms.Label();
            this.txt_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE = new System.Windows.Forms.TextBox();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.btnApplyConfig = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLeft = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvSchedulers = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.gboxLeftButton.SuspendLayout();
            this.gboxLeftOption.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedulers)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnReloadScheduleGroupData});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1034, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbtnReloadScheduleGroupData
            // 
            this.tbtnReloadScheduleGroupData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbtnReloadScheduleGroupData.Image = ((System.Drawing.Image)(resources.GetObject("tbtnReloadScheduleGroupData.Image")));
            this.tbtnReloadScheduleGroupData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnReloadScheduleGroupData.Name = "tbtnReloadScheduleGroupData";
            this.tbtnReloadScheduleGroupData.Size = new System.Drawing.Size(63, 22);
            this.tbtnReloadScheduleGroupData.Text = "重载任务";
            this.tbtnReloadScheduleGroupData.Click += new System.EventHandler(this.tbtnReloadScheduleGroupData_Click);
            // 
            // rtxtLog
            // 
            this.rtxtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtLog.Location = new System.Drawing.Point(341, 301);
            this.rtxtLog.Margin = new System.Windows.Forms.Padding(5);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.Size = new System.Drawing.Size(693, 321);
            this.rtxtLog.TabIndex = 2;
            this.rtxtLog.Text = "";
            // 
            // panelLeft
            // 
            this.panelLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLeft.Controls.Add(this.gboxLeftButton);
            this.panelLeft.Controls.Add(this.gboxLeftOption);
            this.panelLeft.Location = new System.Drawing.Point(0, 30);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(333, 592);
            this.panelLeft.TabIndex = 5;
            // 
            // gboxLeftButton
            // 
            this.gboxLeftButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxLeftButton.Controls.Add(this.btnStopAllSchedules);
            this.gboxLeftButton.Controls.Add(this.btnStartAllSchedules);
            this.gboxLeftButton.Location = new System.Drawing.Point(2, 2);
            this.gboxLeftButton.Name = "gboxLeftButton";
            this.gboxLeftButton.Size = new System.Drawing.Size(327, 261);
            this.gboxLeftButton.TabIndex = 12;
            this.gboxLeftButton.TabStop = false;
            this.gboxLeftButton.Text = "任务操作区";
            // 
            // btnStopAllSchedules
            // 
            this.btnStopAllSchedules.Enabled = false;
            this.btnStopAllSchedules.Location = new System.Drawing.Point(166, 22);
            this.btnStopAllSchedules.Name = "btnStopAllSchedules";
            this.btnStopAllSchedules.Size = new System.Drawing.Size(154, 54);
            this.btnStopAllSchedules.TabIndex = 12;
            this.btnStopAllSchedules.Text = "停止所有任务";
            this.btnStopAllSchedules.UseVisualStyleBackColor = true;
            this.btnStopAllSchedules.Click += new System.EventHandler(this.btnStopAllSchedules_Click);
            // 
            // btnStartAllSchedules
            // 
            this.btnStartAllSchedules.Enabled = false;
            this.btnStartAllSchedules.Location = new System.Drawing.Point(6, 22);
            this.btnStartAllSchedules.Name = "btnStartAllSchedules";
            this.btnStartAllSchedules.Size = new System.Drawing.Size(154, 54);
            this.btnStartAllSchedules.TabIndex = 11;
            this.btnStartAllSchedules.Text = "开始所有任务";
            this.btnStartAllSchedules.UseVisualStyleBackColor = true;
            this.btnStartAllSchedules.Click += new System.EventHandler(this.btnStartAllSchedules_Click);
            // 
            // gboxLeftOption
            // 
            this.gboxLeftOption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxLeftOption.Controls.Add(this.btnClearLog);
            this.gboxLeftOption.Controls.Add(this.lbl_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE);
            this.gboxLeftOption.Controls.Add(this.txt_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE);
            this.gboxLeftOption.Controls.Add(this.btnSaveConfig);
            this.gboxLeftOption.Controls.Add(this.btnApplyConfig);
            this.gboxLeftOption.Location = new System.Drawing.Point(1, 270);
            this.gboxLeftOption.Name = "gboxLeftOption";
            this.gboxLeftOption.Size = new System.Drawing.Size(327, 321);
            this.gboxLeftOption.TabIndex = 11;
            this.gboxLeftOption.TabStop = false;
            this.gboxLeftOption.Text = "任务配置选项";
            // 
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLog.Location = new System.Drawing.Point(244, 277);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 37);
            this.btnClearLog.TabIndex = 15;
            this.btnClearLog.Text = "清空日志";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // lbl_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE
            // 
            this.lbl_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE.AutoSize = true;
            this.lbl_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE.Location = new System.Drawing.Point(5, 35);
            this.lbl_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE.Name = "lbl_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE";
            this.lbl_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE.Size = new System.Drawing.Size(193, 13);
            this.lbl_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE.TabIndex = 13;
            this.lbl_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE.Text = "无订单监控任务短信发送频率(分钟)";
            // 
            // txt_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE
            // 
            this.txt_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE.Location = new System.Drawing.Point(264, 29);
            this.txt_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE.Name = "txt_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE";
            this.txt_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE.Size = new System.Drawing.Size(57, 20);
            this.txt_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE.TabIndex = 14;
            this.txt_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE.Text = "5";
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveConfig.Location = new System.Drawing.Point(84, 277);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(75, 37);
            this.btnSaveConfig.TabIndex = 10;
            this.btnSaveConfig.Text = "保存配置";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // btnApplyConfig
            // 
            this.btnApplyConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApplyConfig.Location = new System.Drawing.Point(3, 277);
            this.btnApplyConfig.Name = "btnApplyConfig";
            this.btnApplyConfig.Size = new System.Drawing.Size(75, 37);
            this.btnApplyConfig.TabIndex = 9;
            this.btnApplyConfig.Text = "应用配置";
            this.btnApplyConfig.UseVisualStyleBackColor = true;
            this.btnApplyConfig.Click += new System.EventHandler(this.btnApplyConfig_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLeft});
            this.statusStrip1.Location = new System.Drawing.Point(0, 633);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1034, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLeft
            // 
            this.statusLeft.Name = "statusLeft";
            this.statusLeft.Size = new System.Drawing.Size(46, 17);
            this.statusLeft.Text = "已就绪";
            // 
            // dgvSchedulers
            // 
            this.dgvSchedulers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSchedulers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSchedulers.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSchedulers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedulers.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvSchedulers.Location = new System.Drawing.Point(341, 30);
            this.dgvSchedulers.Name = "dgvSchedulers";
            this.dgvSchedulers.ReadOnly = true;
            this.dgvSchedulers.RowTemplate.Height = 23;
            this.dgvSchedulers.Size = new System.Drawing.Size(692, 262);
            this.dgvSchedulers.TabIndex = 7;
            this.dgvSchedulers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchedulers_CellDoubleClick);
            this.dgvSchedulers.SelectionChanged += new System.EventHandler(this.dgvSchedulers_SelectionChanged);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 655);
            this.Controls.Add(this.dgvSchedulers);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.rtxtLog);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Text = "支付项目监控系统";
            this.Load += new System.EventHandler(this.Frm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.gboxLeftButton.ResumeLayout(false);
            this.gboxLeftOption.ResumeLayout(false);
            this.gboxLeftOption.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedulers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.RichTextBox rtxtLog;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLeft;
        private System.Windows.Forms.GroupBox gboxLeftButton;
        private System.Windows.Forms.GroupBox gboxLeftOption;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button btnApplyConfig;
        private System.Windows.Forms.Label lbl_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE;
        private System.Windows.Forms.TextBox txt_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE;
        private System.Windows.Forms.Button btnStartAllSchedules;
        private System.Windows.Forms.Button btnStopAllSchedules;
        private System.Windows.Forms.DataGridView dgvSchedulers;
        private System.Windows.Forms.ToolStripButton tbtnReloadScheduleGroupData;
        private System.Windows.Forms.Button btnClearLog;
    }
}

