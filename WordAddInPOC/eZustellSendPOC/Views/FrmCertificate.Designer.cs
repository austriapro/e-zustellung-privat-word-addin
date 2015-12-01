namespace eZustellSendPOC.Views
{
    partial class FrmCertificate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param Name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.certificateViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.commandButton1 = new WinFormsMvvm.Controls.CommandButton();
            this.certificateControllerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.commandButton2 = new WinFormsMvvm.Controls.CommandButton();
            ((System.ComponentModel.ISupportInitialize)(this.certificateViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.certificateControllerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Meine EdID";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.certificateViewModelBindingSource, "EdId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox1.Location = new System.Drawing.Point(83, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(548, 20);
            this.textBox1.TabIndex = 1;
            // 
            // certificateViewModelBindingSource
            // 
            this.certificateViewModelBindingSource.DataSource = typeof(eZustellSendPOC.ViewModels.CertificateViewModel);
            // 
            // commandButton1
            // 
            this.commandButton1.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.certificateControllerBindingSource, "CancelCommand", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.commandButton1.Location = new System.Drawing.Point(555, 37);
            this.commandButton1.Name = "commandButton1";
            this.commandButton1.Size = new System.Drawing.Size(75, 23);
            this.commandButton1.TabIndex = 2;
            this.commandButton1.Text = "Abbrechen";
            this.commandButton1.UseVisualStyleBackColor = true;
            // 
            // certificateControllerBindingSource
            // 
            this.certificateControllerBindingSource.DataSource = typeof(eZustellSendPOC.ViewModels.CertificateController);
            // 
            // commandButton2
            // 
            this.commandButton2.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.certificateControllerBindingSource, "CreateCertificateCommand", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.commandButton2.Location = new System.Drawing.Point(474, 37);
            this.commandButton2.Name = "commandButton2";
            this.commandButton2.Size = new System.Drawing.Size(75, 23);
            this.commandButton2.TabIndex = 3;
            this.commandButton2.Text = "Erstellen";
            this.commandButton2.UseVisualStyleBackColor = true;
            // 
            // FrmCertificate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 78);
            this.Controls.Add(this.commandButton2);
            this.Controls.Add(this.commandButton1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "FrmCertificate";
            this.Text = "SW-Zertifikat erstellen";
            ((System.ComponentModel.ISupportInitialize)(this.certificateViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.certificateControllerBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private WinFormsMvvm.Controls.CommandButton commandButton1;
        private WinFormsMvvm.Controls.CommandButton commandButton2;
        private System.Windows.Forms.BindingSource certificateControllerBindingSource;
        private System.Windows.Forms.BindingSource certificateViewModelBindingSource;
    }
}