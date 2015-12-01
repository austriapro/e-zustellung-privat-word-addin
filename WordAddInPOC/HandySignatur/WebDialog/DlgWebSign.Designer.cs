namespace HandySignatur
{
    partial class DlgWebSign
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tSProgrBar4Web = new System.Windows.Forms.ToolStripProgressBar();
            this.tStripLabelReady = new System.Windows.Forms.ToolStripStatusLabel();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.wbBrHandySig = new System.Windows.Forms.WebBrowser();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSProgrBar4Web,
            this.tStripLabelReady});
            this.statusStrip1.Location = new System.Drawing.Point(0, 388);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(511, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tSProgrBar4Web
            // 
            this.tSProgrBar4Web.Name = "tSProgrBar4Web";
            this.tSProgrBar4Web.Size = new System.Drawing.Size(100, 16);
            this.tSProgrBar4Web.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // tStripLabelReady
            // 
            this.tStripLabelReady.Name = "tStripLabelReady";
            this.tStripLabelReady.Size = new System.Drawing.Size(183, 17);
            this.tStripLabelReady.Text = "Warte auf Handy Signatur Server ...";
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(303, 291);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 19);
            this.BtnCancel.TabIndex = 3;
            this.BtnCancel.Text = "Abbruch";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Visible = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // wbBrHandySig
            // 
            this.wbBrHandySig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbBrHandySig.Location = new System.Drawing.Point(0, 0);
            this.wbBrHandySig.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbBrHandySig.Name = "wbBrHandySig";
            this.wbBrHandySig.Size = new System.Drawing.Size(511, 388);
            this.wbBrHandySig.TabIndex = 0;
            this.wbBrHandySig.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbBrHandySig_DocumentCompleted);
            this.wbBrHandySig.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.wbBrHandySig_Navigated);
            this.wbBrHandySig.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.wbBrHandySig_Navigating);
            // 
            // DlgWebSign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(511, 410);
            this.Controls.Add(this.wbBrHandySig);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.statusStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgWebSign";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Handy Signatur";
            this.Load += new System.EventHandler(this.DlgWebSign_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tStripLabelReady;
        private System.Windows.Forms.ToolStripProgressBar tSProgrBar4Web;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.WebBrowser wbBrHandySig;
    }
}