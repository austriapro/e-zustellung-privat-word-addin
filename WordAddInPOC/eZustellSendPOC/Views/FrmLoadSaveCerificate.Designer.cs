namespace eZustellSendPOC.Views
{
    partial class FrmLoadSaveCertificate
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.loadSaveCertificateViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.commandButton1 = new WinFormsMvvm.Controls.CommandButton();
            this.loadSaveCertificateControllerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.commandButton2 = new WinFormsMvvm.Controls.CommandButton();
            this.commandButton3 = new WinFormsMvvm.Controls.CommandButton();
            this.commandButton4 = new WinFormsMvvm.Controls.CommandButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.loadSaveCertificateViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadSaveCertificateControllerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dateiname";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.loadSaveCertificateViewModelBindingSource, "FnSwCertificate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox1.Location = new System.Drawing.Point(15, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(746, 20);
            this.textBox1.TabIndex = 1;
            // 
            // loadSaveCertificateViewModelBindingSource
            // 
            this.loadSaveCertificateViewModelBindingSource.DataSource = typeof(eZustellSendPOC.ViewModels.LoadSaveCertificateViewModel);
            // 
            // commandButton1
            // 
            this.commandButton1.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.loadSaveCertificateControllerBindingSource, "SelectCertificateFileCommand", true));
            this.commandButton1.Location = new System.Drawing.Point(767, 25);
            this.commandButton1.Name = "commandButton1";
            this.commandButton1.Size = new System.Drawing.Size(27, 23);
            this.commandButton1.TabIndex = 2;
            this.commandButton1.Text = "...";
            this.commandButton1.UseVisualStyleBackColor = true;
            // 
            // loadSaveCertificateControllerBindingSource
            // 
            this.loadSaveCertificateControllerBindingSource.DataSource = typeof(eZustellSendPOC.ViewModels.LoadSaveCertificateController);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Passwort";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.loadSaveCertificateViewModelBindingSource, "ShowConfirmPasswort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label3.Location = new System.Drawing.Point(411, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Passwort bestätigen";
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.loadSaveCertificateViewModelBindingSource, "Passwort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox2.Location = new System.Drawing.Point(15, 78);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(379, 20);
            this.textBox2.TabIndex = 5;
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.loadSaveCertificateViewModelBindingSource, "ConfirmPasswort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.loadSaveCertificateViewModelBindingSource, "ShowConfirmPasswort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox3.Location = new System.Drawing.Point(414, 78);
            this.textBox3.Name = "textBox3";
            this.textBox3.PasswordChar = '*';
            this.textBox3.Size = new System.Drawing.Size(379, 20);
            this.textBox3.TabIndex = 6;
            // 
            // commandButton2
            // 
            this.commandButton2.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.loadSaveCertificateControllerBindingSource, "CancelCommand", true));
            this.commandButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.commandButton2.Location = new System.Drawing.Point(719, 104);
            this.commandButton2.Name = "commandButton2";
            this.commandButton2.Size = new System.Drawing.Size(75, 23);
            this.commandButton2.TabIndex = 7;
            this.commandButton2.Text = "Abbrechen";
            this.commandButton2.UseVisualStyleBackColor = true;
            // 
            // commandButton3
            // 
            this.commandButton3.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.loadSaveCertificateControllerBindingSource, "LoadSaveButtonCommand", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.commandButton3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.loadSaveCertificateViewModelBindingSource, "LoadSaveButtonText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.commandButton3.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.loadSaveCertificateViewModelBindingSource, "LoadSaveEnabled", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.commandButton3.Location = new System.Drawing.Point(638, 104);
            this.commandButton3.Name = "commandButton3";
            this.commandButton3.Size = new System.Drawing.Size(75, 23);
            this.commandButton3.TabIndex = 8;
            this.commandButton3.Text = "Ladem";
            this.commandButton3.UseVisualStyleBackColor = true;
            // 
            // commandButton4
            // 
            this.commandButton4.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.loadSaveCertificateControllerBindingSource, "NewCertificateCommand", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.commandButton4.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.loadSaveCertificateViewModelBindingSource, "ShowNewButton", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.commandButton4.Location = new System.Drawing.Point(557, 103);
            this.commandButton4.Name = "commandButton4";
            this.commandButton4.Size = new System.Drawing.Size(75, 23);
            this.commandButton4.TabIndex = 9;
            this.commandButton4.Text = "Neu";
            this.commandButton4.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.loadSaveCertificateViewModelBindingSource, "SaveAsCert", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.loadSaveCertificateViewModelBindingSource, "IsSaveAsCertVisible", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox1.Location = new System.Drawing.Point(15, 109);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(263, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Zusätzlich als Verschlüsselungszertifikat speichern";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // FrmLoadSaveCertificate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.commandButton2;
            this.ClientSize = new System.Drawing.Size(805, 137);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.commandButton4);
            this.Controls.Add(this.commandButton3);
            this.Controls.Add(this.commandButton2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.commandButton1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.loadSaveCertificateViewModelBindingSource, "FormTitle", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.Name = "FrmLoadSaveCertificate";
            this.Text = "SW-Zertifikat laden";
            ((System.ComponentModel.ISupportInitialize)(this.loadSaveCertificateViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadSaveCertificateControllerBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private WinFormsMvvm.Controls.CommandButton commandButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private WinFormsMvvm.Controls.CommandButton commandButton2;
        private WinFormsMvvm.Controls.CommandButton commandButton3;
        private System.Windows.Forms.BindingSource loadSaveCertificateControllerBindingSource;
        private System.Windows.Forms.BindingSource loadSaveCertificateViewModelBindingSource;
        private WinFormsMvvm.Controls.CommandButton commandButton4;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}