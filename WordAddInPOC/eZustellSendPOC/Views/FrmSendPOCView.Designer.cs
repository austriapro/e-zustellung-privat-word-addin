namespace eZustellSendPOC.Views
{
    partial class FrmSendPOCView
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param Name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.öffnenToolStripMenuItem = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.sendPOCControllerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.beendenToolStripMenuItem = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.zertifikatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCommandMenuItem1 = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.toolStripCommandMenuItem2 = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.erstellenToolStripMenuItem = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.überToolStripMenuItem = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.cBxReceiver = new WinFormsMvvm.MultiColumnComboBox();
            this.sendPOCViewmodelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.tBxSubject = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tBxReference = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cBxDocumentClass = new System.Windows.Forms.ComboBox();
            this.documentClassListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.cBxDeliveryQuality = new System.Windows.Forms.ComboBox();
            this.deliverQualityListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.tBxMessage = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.cBxMailConfirm = new System.Windows.Forms.ComboBox();
            this.confirmationEMailListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.tBxCallbackUrl = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.commandButton1 = new WinFormsMvvm.Controls.CommandButton();
            this.commandButton2 = new WinFormsMvvm.Controls.CommandButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sendPOCControllerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sendPOCViewmodelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentClassListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deliverQualityListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.confirmationEMailListBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.zertifikatToolStripMenuItem,
            this.überToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(625, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.öffnenToolStripMenuItem,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.dateiToolStripMenuItem.Text = "Adressbuch";
            // 
            // öffnenToolStripMenuItem
            // 
            this.öffnenToolStripMenuItem.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.sendPOCControllerBindingSource, "OpenAddresBookCommand", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.öffnenToolStripMenuItem.Name = "öffnenToolStripMenuItem";
            this.öffnenToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.öffnenToolStripMenuItem.Text = "Öffnen";
            // 
            // sendPOCControllerBindingSource
            // 
            this.sendPOCControllerBindingSource.DataSource = typeof(eZustellSendPOC.ViewModels.SendPOCController);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.beendenToolStripMenuItem.Text = "Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // zertifikatToolStripMenuItem
            // 
            this.zertifikatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripCommandMenuItem1,
            this.toolStripCommandMenuItem2,
            this.erstellenToolStripMenuItem});
            this.zertifikatToolStripMenuItem.Name = "zertifikatToolStripMenuItem";
            this.zertifikatToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.zertifikatToolStripMenuItem.Text = "SW-Zertifikat";
            // 
            // toolStripCommandMenuItem1
            // 
            this.toolStripCommandMenuItem1.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.sendPOCControllerBindingSource, "LoadCertificateCommand", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.toolStripCommandMenuItem1.Name = "toolStripCommandMenuItem1";
            this.toolStripCommandMenuItem1.Size = new System.Drawing.Size(126, 22);
            this.toolStripCommandMenuItem1.Text = "Laden";
            // 
            // toolStripCommandMenuItem2
            // 
            this.toolStripCommandMenuItem2.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.sendPOCControllerBindingSource, "SaveCertificateCommand", true));
            this.toolStripCommandMenuItem2.Name = "toolStripCommandMenuItem2";
            this.toolStripCommandMenuItem2.Size = new System.Drawing.Size(126, 22);
            this.toolStripCommandMenuItem2.Text = "Speichern";
            this.toolStripCommandMenuItem2.ToolTipText = "Speichert das Zertifikat";
            // 
            // erstellenToolStripMenuItem
            // 
            this.erstellenToolStripMenuItem.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.sendPOCControllerBindingSource, "MakeCertificateCommand", true));
            this.erstellenToolStripMenuItem.Name = "erstellenToolStripMenuItem";
            this.erstellenToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.erstellenToolStripMenuItem.Text = "Erstellen";
            // 
            // überToolStripMenuItem
            // 
            this.überToolStripMenuItem.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.sendPOCControllerBindingSource, "ShowAboutCommand", true));
            this.überToolStripMenuItem.Name = "überToolStripMenuItem";
            this.überToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.überToolStripMenuItem.Text = "Über";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Empfänger";
            // 
            // cBxReceiver
            // 
            this.cBxReceiver.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.sendPOCViewmodelBindingSource, "SelectedReceiver", true));
            this.cBxReceiver.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cBxReceiver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBxReceiver.FormattingEnabled = true;
            this.cBxReceiver.Location = new System.Drawing.Point(116, 42);
            this.cBxReceiver.Name = "cBxReceiver";
            this.cBxReceiver.Size = new System.Drawing.Size(497, 21);
            this.cBxReceiver.TabIndex = 2;
            // 
            // sendPOCViewmodelBindingSource
            // 
            this.sendPOCViewmodelBindingSource.DataSource = typeof(eZustellSendPOC.ViewModels.SendPOCViewmodel);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Betreff";
            // 
            // tBxSubject
            // 
            this.tBxSubject.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sendPOCViewmodelBindingSource, "Subject", true));
            this.tBxSubject.Location = new System.Drawing.Point(116, 69);
            this.tBxSubject.Name = "tBxSubject";
            this.tBxSubject.Size = new System.Drawing.Size(497, 20);
            this.tBxSubject.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Referenz";
            // 
            // tBxReference
            // 
            this.tBxReference.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sendPOCViewmodelBindingSource, "Reference", true));
            this.tBxReference.Location = new System.Drawing.Point(116, 100);
            this.tBxReference.Name = "tBxReference";
            this.tBxReference.Size = new System.Drawing.Size(497, 20);
            this.tBxReference.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Dokumentenklasse";
            // 
            // cBxDocumentClass
            // 
            this.cBxDocumentClass.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.sendPOCViewmodelBindingSource, "SelectedDocumentClass", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cBxDocumentClass.DataSource = this.documentClassListBindingSource;
            this.cBxDocumentClass.DisplayMember = "Description";
            this.cBxDocumentClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBxDocumentClass.FormattingEnabled = true;
            this.cBxDocumentClass.Location = new System.Drawing.Point(116, 127);
            this.cBxDocumentClass.Name = "cBxDocumentClass";
            this.cBxDocumentClass.Size = new System.Drawing.Size(497, 21);
            this.cBxDocumentClass.TabIndex = 8;
            // 
            // documentClassListBindingSource
            // 
            this.documentClassListBindingSource.DataMember = "DocClasses";
            this.documentClassListBindingSource.DataSource = this.sendPOCViewmodelBindingSource;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Zustellqualität";
            // 
            // cBxDeliveryQuality
            // 
            this.cBxDeliveryQuality.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.sendPOCViewmodelBindingSource, "selectedDeliveryQuality", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cBxDeliveryQuality.DataSource = this.deliverQualityListBindingSource;
            this.cBxDeliveryQuality.DisplayMember = "Description";
            this.cBxDeliveryQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBxDeliveryQuality.FormattingEnabled = true;
            this.cBxDeliveryQuality.Location = new System.Drawing.Point(116, 156);
            this.cBxDeliveryQuality.Name = "cBxDeliveryQuality";
            this.cBxDeliveryQuality.Size = new System.Drawing.Size(497, 21);
            this.cBxDeliveryQuality.TabIndex = 10;
            // 
            // deliverQualityListBindingSource
            // 
            this.deliverQualityListBindingSource.DataMember = "DeliverQualityList";
            this.deliverQualityListBindingSource.DataSource = this.sendPOCViewmodelBindingSource;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Kurznachricht";
            // 
            // tBxMessage
            // 
            this.tBxMessage.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sendPOCViewmodelBindingSource, "MessageText", true));
            this.tBxMessage.Location = new System.Drawing.Point(116, 188);
            this.tBxMessage.Multiline = true;
            this.tBxMessage.Name = "tBxMessage";
            this.tBxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tBxMessage.Size = new System.Drawing.Size(497, 186);
            this.tBxMessage.TabIndex = 12;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.sendPOCViewmodelBindingSource, "LockDeliveryUntil", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox1.Location = new System.Drawing.Point(15, 387);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(129, 17);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Zustellung sperren bis";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd.MM.yyyy hh:mm";
            this.dateTimePicker1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.sendPOCViewmodelBindingSource, "LockedUntilDateTime", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "d"));
            this.dateTimePicker1.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.sendPOCViewmodelBindingSource, "LockDeliveryUntil", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(149, 385);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(97, 20);
            this.dateTimePicker1.TabIndex = 14;
            // 
            // textBox4
            // 
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sendPOCViewmodelBindingSource, "AttachmentFileName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox4.Location = new System.Drawing.Point(116, 416);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(497, 20);
            this.textBox4.TabIndex = 16;
            // 
            // cBxMailConfirm
            // 
            this.cBxMailConfirm.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.sendPOCViewmodelBindingSource, "ConfirmByEMail", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cBxMailConfirm.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.sendPOCViewmodelBindingSource, "SelectedEmailToConfirm", true));
            this.cBxMailConfirm.DataSource = this.confirmationEMailListBindingSource;
            this.cBxMailConfirm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBxMailConfirm.FormattingEnabled = true;
            this.cBxMailConfirm.Location = new System.Drawing.Point(167, 26);
            this.cBxMailConfirm.Name = "cBxMailConfirm";
            this.cBxMailConfirm.Size = new System.Drawing.Size(425, 21);
            this.cBxMailConfirm.TabIndex = 19;
            // 
            // confirmationEMailListBindingSource
            // 
            this.confirmationEMailListBindingSource.DataMember = "ConfirmationEMailList";
            this.confirmationEMailListBindingSource.DataSource = this.sendPOCViewmodelBindingSource;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.sendPOCViewmodelBindingSource, "ConfirmByEMail", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.radioButton1.Location = new System.Drawing.Point(6, 27);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(138, 17);
            this.radioButton1.TabIndex = 20;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "an diese E-Mail Adresse";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.sendPOCViewmodelBindingSource, "ConfirmByWebServiceUrl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.radioButton2.Location = new System.Drawing.Point(6, 56);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(152, 17);
            this.radioButton2.TabIndex = 21;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "an diese WebService URL";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // tBxCallbackUrl
            // 
            this.tBxCallbackUrl.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.sendPOCViewmodelBindingSource, "ConfirmByWebServiceUrl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tBxCallbackUrl.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sendPOCViewmodelBindingSource, "ConfirmationWebServiceUrl", true));
            this.tBxCallbackUrl.Location = new System.Drawing.Point(167, 55);
            this.tBxCallbackUrl.Name = "tBxCallbackUrl";
            this.tBxCallbackUrl.Size = new System.Drawing.Size(425, 20);
            this.tBxCallbackUrl.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 419);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Anlage";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.sendPOCViewmodelBindingSource, "LockedUtilTime", true));
            this.dateTimePicker2.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.sendPOCViewmodelBindingSource, "LockDeliveryUntil", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker2.Location = new System.Drawing.Point(252, 385);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.ShowUpDown = true;
            this.dateTimePicker2.Size = new System.Drawing.Size(69, 20);
            this.dateTimePicker2.TabIndex = 25;
            // 
            // commandButton1
            // 
            this.commandButton1.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.sendPOCControllerBindingSource, "DeliverCommand", true));
            this.commandButton1.Location = new System.Drawing.Point(510, 601);
            this.commandButton1.Name = "commandButton1";
            this.commandButton1.Size = new System.Drawing.Size(103, 23);
            this.commandButton1.TabIndex = 26;
            this.commandButton1.Text = "Zustellung senden";
            this.commandButton1.UseVisualStyleBackColor = true;
            // 
            // commandButton2
            // 
            this.commandButton2.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.sendPOCControllerBindingSource, "AddAttachmentCommand", true));
            this.commandButton2.Location = new System.Drawing.Point(392, 601);
            this.commandButton2.Name = "commandButton2";
            this.commandButton2.Size = new System.Drawing.Size(112, 23);
            this.commandButton2.TabIndex = 27;
            this.commandButton2.Text = "Anlage hinzufügen";
            this.commandButton2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tBxCallbackUrl);
            this.groupBox1.Controls.Add(this.cBxMailConfirm);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Location = new System.Drawing.Point(15, 451);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(598, 95);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zustellbestätigung senden an";
            // 
            // FrmSendPOCView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 636);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.commandButton2);
            this.Controls.Add(this.commandButton1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.tBxMessage);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cBxDeliveryQuality);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cBxDocumentClass);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tBxReference);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tBxSubject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cBxReceiver);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmSendPOCView";
            this.Text = "eZustellung senden";
            this.Shown += new System.EventHandler(this.FrmSendPOCView_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sendPOCControllerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sendPOCViewmodelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentClassListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deliverQualityListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.confirmationEMailListBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem öffnenToolStripMenuItem;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zertifikatToolStripMenuItem;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem erstellenToolStripMenuItem;
        private System.Windows.Forms.BindingSource sendPOCControllerBindingSource;
        private System.Windows.Forms.Label label1;
        private WinFormsMvvm.MultiColumnComboBox cBxReceiver;
        private System.Windows.Forms.BindingSource sendPOCViewmodelBindingSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tBxSubject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tBxReference;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cBxDocumentClass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cBxDeliveryQuality;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tBxMessage;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.ComboBox cBxMailConfirm;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.TextBox tBxCallbackUrl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.BindingSource documentClassListBindingSource;
        private System.Windows.Forms.BindingSource deliverQualityListBindingSource;
        private System.Windows.Forms.BindingSource confirmationEMailListBindingSource;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private WinFormsMvvm.Controls.CommandButton commandButton1;
        private WinFormsMvvm.Controls.CommandButton commandButton2;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem toolStripCommandMenuItem1;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem toolStripCommandMenuItem2;
        private System.Windows.Forms.GroupBox groupBox1;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem überToolStripMenuItem;
    }
}

