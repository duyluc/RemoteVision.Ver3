
namespace Client.Ver2
{
    partial class ClientForm
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
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.tbxIp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxMessage = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.Display4 = new System.Windows.Forms.PictureBox();
            this.Display3 = new System.Windows.Forms.PictureBox();
            this.Display2 = new System.Windows.Forms.PictureBox();
            this.Display1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Display4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Display3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Display2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Display1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.17283F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.82717F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1001, 644);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbxMessage, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 251F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(266, 638);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSelectImage);
            this.panel1.Controls.Add(this.btnSend);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbxPort);
            this.panel1.Controls.Add(this.tbxIp);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 65);
            this.panel1.TabIndex = 0;
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(132, 36);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(59, 23);
            this.btnSelectImage.TabIndex = 5;
            this.btnSelectImage.Text = "Search";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(197, 36);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(59, 23);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = ":";
            // 
            // tbxPort
            // 
            this.tbxPort.Location = new System.Drawing.Point(177, 10);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(79, 20);
            this.tbxPort.TabIndex = 2;
            this.tbxPort.Text = "9999";
            this.tbxPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxIp
            // 
            this.tbxIp.Location = new System.Drawing.Point(52, 10);
            this.tbxIp.Name = "tbxIp";
            this.tbxIp.Size = new System.Drawing.Size(111, 20);
            this.tbxIp.TabIndex = 1;
            this.tbxIp.Text = "127.0.0.1";
            this.tbxIp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Address:";
            // 
            // tbxMessage
            // 
            this.tbxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxMessage.Location = new System.Drawing.Point(3, 325);
            this.tbxMessage.Multiline = true;
            this.tbxMessage.Name = "tbxMessage";
            this.tbxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxMessage.Size = new System.Drawing.Size(260, 310);
            this.tbxMessage.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.Display4, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.Display3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.Display2, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.Display1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(275, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(723, 638);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // Display4
            // 
            this.Display4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Display4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Display4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Display4.Location = new System.Drawing.Point(364, 322);
            this.Display4.Name = "Display4";
            this.Display4.Size = new System.Drawing.Size(356, 313);
            this.Display4.TabIndex = 3;
            this.Display4.TabStop = false;
            // 
            // Display3
            // 
            this.Display3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Display3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Display3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Display3.Location = new System.Drawing.Point(3, 322);
            this.Display3.Name = "Display3";
            this.Display3.Size = new System.Drawing.Size(355, 313);
            this.Display3.TabIndex = 2;
            this.Display3.TabStop = false;
            // 
            // Display2
            // 
            this.Display2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Display2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Display2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Display2.Location = new System.Drawing.Point(364, 3);
            this.Display2.Name = "Display2";
            this.Display2.Size = new System.Drawing.Size(356, 313);
            this.Display2.TabIndex = 1;
            this.Display2.TabStop = false;
            // 
            // Display1
            // 
            this.Display1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Display1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Display1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Display1.Location = new System.Drawing.Point(3, 3);
            this.Display1.Name = "Display1";
            this.Display1.Size = new System.Drawing.Size(355, 313);
            this.Display1.TabIndex = 0;
            this.Display1.TabStop = false;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 644);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ClientForm";
            this.Text = "CLIENT";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Display4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Display3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Display2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Display1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxPort;
        private System.Windows.Forms.TextBox tbxIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbxMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.PictureBox Display4;
        private System.Windows.Forms.PictureBox Display3;
        private System.Windows.Forms.PictureBox Display2;
        private System.Windows.Forms.PictureBox Display1;
        private System.Windows.Forms.Button btnSelectImage;
    }
}

