
namespace Server.Ver2
{
    partial class ServerForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnToolBlock = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tbIp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxMessage = new System.Windows.Forms.TextBox();
            this.dvAcceptableIp = new IpAddressTable.IpAddressTable();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.cbxShowClient = new System.Windows.Forms.ComboBox();
            this.MainDisplay = new Cognex.VisionPro.CogToolDisplay();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 330F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1453, 798);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbxMessage, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.dvAcceptableIp, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 294F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 167F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(322, 790);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnToolBlock);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbPort);
            this.panel1.Controls.Add(this.tbIp);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(311, 87);
            this.panel1.TabIndex = 0;
            // 
            // btnToolBlock
            // 
            this.btnToolBlock.Location = new System.Drawing.Point(44, 50);
            this.btnToolBlock.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnToolBlock.Name = "btnToolBlock";
            this.btnToolBlock.Size = new System.Drawing.Size(79, 28);
            this.btnToolBlock.TabIndex = 6;
            this.btnToolBlock.Text = "Tool";
            this.btnToolBlock.UseVisualStyleBackColor = true;
            this.btnToolBlock.Click += new System.EventHandler(this.btnToolBlock_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(217, 50);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(79, 28);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(131, 50);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(79, 28);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = ":";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(207, 11);
            this.tbPort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(76, 22);
            this.tbPort.TabIndex = 2;
            this.tbPort.Text = "9999";
            this.tbPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbIp
            // 
            this.tbIp.Location = new System.Drawing.Point(77, 11);
            this.tbIp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbIp.Name = "tbIp";
            this.tbIp.Size = new System.Drawing.Size(116, 22);
            this.tbIp.TabIndex = 1;
            this.tbIp.Text = "127.0.0.1";
            this.tbIp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Address:";
            // 
            // tbxMessage
            // 
            this.tbxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxMessage.Location = new System.Drawing.Point(4, 393);
            this.tbxMessage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxMessage.Multiline = true;
            this.tbxMessage.Name = "tbxMessage";
            this.tbxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxMessage.Size = new System.Drawing.Size(311, 393);
            this.tbxMessage.TabIndex = 1;
            // 
            // dvAcceptableIp
            // 
            this.dvAcceptableIp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dvAcceptableIp.DbProvider = null;
            this.dvAcceptableIp.Location = new System.Drawing.Point(5, 100);
            this.dvAcceptableIp.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.dvAcceptableIp.Name = "dvAcceptableIp";
            this.dvAcceptableIp.Size = new System.Drawing.Size(308, 283);
            this.dvAcceptableIp.TabIndex = 2;
            this.dvAcceptableIp.TableChanged += new System.EventHandler(this.AcceptTable_TableChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.cbxShowClient, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.MainDisplay, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(334, 4);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1115, 790);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // cbxShowClient
            // 
            this.cbxShowClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxShowClient.FormattingEnabled = true;
            this.cbxShowClient.Location = new System.Drawing.Point(4, 4);
            this.cbxShowClient.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxShowClient.Name = "cbxShowClient";
            this.cbxShowClient.Size = new System.Drawing.Size(1107, 24);
            this.cbxShowClient.TabIndex = 1;
            // 
            // MainDisplay
            // 
            this.MainDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainDisplay.Location = new System.Drawing.Point(3, 34);
            this.MainDisplay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MainDisplay.Name = "MainDisplay";
            this.MainDisplay.SelectedRecordKey = null;
            this.MainDisplay.ShowRecordsDropDown = true;
            this.MainDisplay.Size = new System.Drawing.Size(1109, 754);
            this.MainDisplay.TabIndex = 2;
            this.MainDisplay.Tool = null;
            this.MainDisplay.ToolSyncObject = null;
            this.MainDisplay.UserRecord = null;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1453, 798);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ServerForm";
            this.Text = "SERVER";
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.Shown += new System.EventHandler(this.ServerForm_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.TextBox tbIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbxMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnToolBlock;
        private System.Windows.Forms.ComboBox cbxShowClient;
        private IpAddressTable.IpAddressTable dvAcceptableIp;
        private Cognex.VisionPro.CogToolDisplay MainDisplay;
    }
}

