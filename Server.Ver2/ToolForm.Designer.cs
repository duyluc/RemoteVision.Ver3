
namespace Server.Ver2
{
    partial class ToolForm
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
            this.CogToolBlockEditer = new Cognex.VisionPro.ToolBlock.CogToolBlockEditV2();
            ((System.ComponentModel.ISupportInitialize)(this.CogToolBlockEditer)).BeginInit();
            this.SuspendLayout();
            // 
            // CogToolBlockEditer
            // 
            this.CogToolBlockEditer.AllowDrop = true;
            this.CogToolBlockEditer.ContextMenuCustomizer = null;
            this.CogToolBlockEditer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CogToolBlockEditer.Location = new System.Drawing.Point(0, 0);
            this.CogToolBlockEditer.MinimumSize = new System.Drawing.Size(489, 0);
            this.CogToolBlockEditer.Name = "CogToolBlockEditer";
            this.CogToolBlockEditer.ShowNodeToolTips = true;
            this.CogToolBlockEditer.Size = new System.Drawing.Size(967, 652);
            this.CogToolBlockEditer.SuspendElectricRuns = false;
            this.CogToolBlockEditer.TabIndex = 0;
            // 
            // ToolForm
            // 
            this.ClientSize = new System.Drawing.Size(967, 652);
            this.Controls.Add(this.CogToolBlockEditer);
            this.Name = "ToolForm";
            ((System.ComponentModel.ISupportInitialize)(this.CogToolBlockEditer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Cognex.VisionPro.ToolBlock.CogToolBlockEditV2 CogToolBlockEditer;
    }
}